import React, { useState, useEffect } from "react";
import { Modal, Button } from "react-bootstrap";
import facade from "../api/MovieFacade"

export default function MovieModal({
  showMovie,
  setShowMovie,
  movieTitle,
}) {
  const [movieInfo, setMovieInfo] = useState([])
  function handleMovieInfo(){
    facade.getMovie(movieTitle).then((e) => setMovieInfo(e));
  }
  const toShow = movieInfo && movieInfo.Actors ? (
    movieInfo.Actors.length > 0 ? movieInfo.Actors.map((a, index) => (
      <div key={a.Id}>{a.Name}, {a.Born}</div>
    )) : "Loading"
    ) : ("Loading...")
  useEffect(() =>{
    if(showMovie){
      handleMovieInfo(movieTitle)
    }
  }, [showMovie])
  return (
    <Modal show={showMovie} onHide={() => setShowMovie(false)}>
      <Modal.Header closeButton>
        <Modal.Title>{movieTitle}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h2>Actors</h2>
        {toShow}
        </Modal.Body>
      <Modal.Footer>
      </Modal.Footer>
    </Modal>
  );
}