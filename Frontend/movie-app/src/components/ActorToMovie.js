import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import facade from "./api/MovieFacade";
import Select from "react-select";

export default function ActorToMovie() {
  const [actorOptions, setActorOptions] = useState(null);
  const [movieOptions, setMovieOptions] = useState(null);
  const [pickedActor, setPickedActor] = useState("Actor..");
  const [pickedMovie, setPickedMovie] = useState("Movie..");

  useEffect(() => {
    if (!actorOptions) {
      const actors = facade.getActors();
      actors.then((arr) =>
        setActorOptions(
          arr.map((actor) => ({
            value: actor.Name,
            label: actor.Name,
          }))
        )
      );
    }
    if (!movieOptions) {
      const movies = facade.getMovies();
      movies.then((arr) =>
        setMovieOptions(arr.map((e) => ({ value: e.Title, label: e.Title })))
      );
    }
  }, [actorOptions]);

  const addActorToMovie = (e) => {
    e.preventDefault();
    facade.addActorToMovie(pickedActor, pickedMovie);
  };

  return (
    <div>
      <div className="d-flex justify-content-evenly">
        <Select
          placeholder={pickedActor}
          options={actorOptions}
          form={"true"}
          onChange={(actor) => setPickedActor(actor.value)}
        />
        <Select
          placeholder={pickedMovie}
          options={movieOptions}
          form={"true"}
          onChange={(actor) => setPickedMovie(actor.value)}
        />
      </div>
      <Button
        className="mt-2"
        variant="primary"
        type="submit"
        onClick={addActorToMovie}
      >
        Submit
      </Button>
    </div>
  );
}
