import React from "react";
import { Modal, Button } from "react-bootstrap";

export default function ReviewModal({
  showReviews,
  setShowReviews,
  movieTitle,
}) {
  return (
    <Modal show={showReviews} onHide={() => setShowReviews(false)}>
      <Modal.Header closeButton>
        <Modal.Title>{movieTitle}</Modal.Title>
      </Modal.Header>
      <Modal.Body>Woohoo, you're reading all these reviews!</Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={() => setShowReviews(false)}>
          Add my review
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
