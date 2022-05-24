import React, { useState, useEffect } from "react";
import { Modal, Button } from "react-bootstrap";
import facade from "../api/MovieFacade"

export default function ActorModal({
  showActor,
  setShowActor,
  actorName,
  setMovieTitle,
  setShowMovie,
}) {
  const [actorInfo, setActorInfo] = useState([])
  function handleActorInfo(){
    facade.getActor(actorName).then((e) => setActorInfo(e));
  }
  const toShow = actorInfo && actorInfo.ActedIn ? (
    <div>
      <p>Born: {actorInfo.Born}</p>
      <h3>Acted In</h3>
      {actorInfo.ActedIn.length > 0 ? actorInfo.ActedIn.map((m, index) => (
        <div key={m.Id}>
          <button
              type="submit"
              onClick={() => {setMovieTitle(m.Title); setShowMovie(true); setShowActor(false)}}
              className="btn btn-secondary"
          >{m.Title}</button>
          </div>
      )) : "Loading"}
    </div>
    ) : ("Loading...")
    useEffect(() =>{
      if(showActor){
        handleActorInfo(actorName)
      }
    }, [showActor])
  
  return (
    <Modal show={showActor} onHide={() => setShowActor(false)}>
      <Modal.Header closeButton>
        <Modal.Title>{actorName}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {toShow}
      </Modal.Body>
      <Modal.Footer>
      </Modal.Footer>
    </Modal>
  );
}
