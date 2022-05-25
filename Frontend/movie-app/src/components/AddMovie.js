import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import facade from "./api/MovieFacade";

export default function AddMovie() {
  const [movieInfo, setMovieInfo] = useState({
    title: "",
    tagline: "",
    released: 0,
  });

  const onChangeMovieForm = (evt) => {
    setMovieInfo({
      ...movieInfo,
      [evt.target.id]: evt.target.value,
    });
  };

  const addMovie = (e) => {
    e.preventDefault();
    facade.addMovie(movieInfo.title, movieInfo.tagline, movieInfo.released);
  };

  return (
    <Form onChange={onChangeMovieForm}>
      <Form.Group className="mb-3">
        <Form.Control id="title" type="text" placeholder="Movie title" />
        <Form.Control
          id="tagline"
          type="text"
          className="mt-1"
          placeholder="Tagline"
        />
        <Form.Control
          id="released"
          type="number"
          className="mt-1"
          placeholder="Year of release"
        />
      </Form.Group>
      <Button variant="primary" type="submit" onClick={addMovie}>
        Submit
      </Button>
    </Form>
  );
}
