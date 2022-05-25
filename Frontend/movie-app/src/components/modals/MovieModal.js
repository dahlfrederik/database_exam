import React, { useState, useEffect } from "react";
import { Modal, Button } from "react-bootstrap";
import facade from "../api/MovieFacade"

export default function MovieModal({
  showMovie,
  setShowMovie,
  movieTitle,
  setMovieTitle,
  setActorName,
  setShowActor,
  setShowReviews,
}) {
  const [movieInfo, setMovieInfo] = useState([])
  function handleMovieInfo(){
    facade.getMovie(movieTitle).then((e) => setMovieInfo(e));
  }
  const toShow = movieInfo && movieInfo.Actors ? (
    <div>
      <p>Released: {movieInfo.Released}</p>
      <p>Tagline: {movieInfo.Tagline}</p>
      <h3>Actors</h3>
      {movieInfo.Actors.length > 0 ? movieInfo.Actors.map((a, index) => (
        <div key={a.Id}>
          <button
              type="submit"
              onClick={() => {setActorName(a.Name); setShowActor(true); setShowMovie(false)}}
              className="btn btn-secondary"
          >{a.Name}</button>
          </div>
      )) : "Loading"}
    </div>
    ) : ("Loading...")
  useEffect(() =>{
    if(showMovie){
      handleMovieInfo(movieTitle)
    }
  }, [showMovie])
  return (
    <Modal show={showMovie} onHide={() => setShowMovie(false)}>
      <Modal.Header closeButton>
        <Modal.Title>Movie: {movieTitle}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {toShow}
        </Modal.Body>
      <Modal.Footer>
      <Button variant="primary" onClick={() => {setMovieTitle(movieTitle); setShowReviews(true); setShowMovie(false);}}>
          Show Reviews
        </Button>
      </Modal.Footer>
    </Modal>
  );
}