import React, { useEffect, useState } from "react";
import facade from "./api/MovieFacade";
import Select from "react-select";
import { Button, Form } from "react-bootstrap";

export default function NewActorToMovie() {
  const [movieOptions, setMovieOptions] = useState(null);
  const [pickedMovie, setPickedMovie] = useState("Movie..");
  const [actor, setActor] = useState({
    name: "",
    born: 0,
  });

  useEffect(() => {
    if (!movieOptions) {
      const movies = facade.getMovies();
      movies.then((arr) =>
        setMovieOptions(arr.map((e) => ({ value: e.Title, label: e.Title })))
      );
    }
  }, [movieOptions]);

  const onChangeActorForm = (evt) => {
    setActor({
      ...actor,
      [evt.target.id]: evt.target.value,
    });
  };

  const addNewActorToMovie = (e) => {
    e.preventDefault();
    facade.addNewActorToMovie(actor, pickedMovie);
  };

  return (
    <div>
      <div className="d-flex justify-content-center">
        <Form onChange={onChangeActorForm}>
          <Form.Group className="mb-3">
            <Form.Control
              id="name"
              type="text"
              className="mt-1"
              placeholder="Name"
            />
            <Form.Control
              id="born"
              type="number"
              className="mt-1"
              placeholder="Born"
            />
            <Select
              placeholder={pickedMovie}
              options={movieOptions}
              form={"true"}
              onChange={(movie) => setPickedMovie(movie.value)}
            />
          </Form.Group>
        </Form>
      </div>
      <Button
        className="mt-2"
        variant="primary"
        type="submit"
        onClick={(e) => addNewActorToMovie(e)}
      >
        Submit
      </Button>
    </div>
  );
}
