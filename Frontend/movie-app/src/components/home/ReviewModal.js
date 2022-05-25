import React, { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import facade from "../api/ReviewFacade";
import "./styles/styles.css";

export default function ReviewModal({
  showReviews,
  setShowReviews,
  movieTitle,
  movieId,
  myUser,
  rating,
}) {
  const [reviews, setReviews] = useState([]);
  const [newReview, setNewReview] = useState({
    MovieId: movieId,
    UserId: myUser.Id,
    UserName: myUser.Username,
    Desc: "",
    Rating: 0,
  });

  function handleReviews() {
    facade.getLatestReviews(movieId).then((e) => setReviews(e));
  }
  const onChangeReviewForm = (evt) => {
    setNewReview({
      ...newReview,
      [evt.target.id]: evt.target.value,
    });
  };

  const reviewBody =
    reviews.length > 0
      ? reviews.map((review) => (
          <div class="reviewContainer">
            <div class="review">
              <p>Rating: {review.Rating}</p>
              <p>Description: {review.Desc}</p>
              <p>By: {review.Username}</p>
            </div>
          </div>
        ))
      : "No reviews yet... Be the first!";

  const addReview = (e) => {
    e.preventDefault();
    facade.addReview(
      newReview.MovieId,
      newReview.UserId,
      newReview.UserName,
      newReview.Desc,
      newReview.Rating
    );
  };
  console.log(newReview);

  useEffect(() => {
    if (showReviews) {
      handleReviews(movieTitle);
    }
  }, [showReviews]);

  return (
    <Modal
      class="modal-dialog"
      show={showReviews}
      onHide={() => setShowReviews(false)}
    >
      <Modal.Header closeButton>
        <Modal.Title>
          {movieTitle} score: {rating}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body class="modal-body">{reviewBody}</Modal.Body>
      <Modal.Footer>
        <Form style={{ width: "100%" }} onChange={onChangeReviewForm}>
          <Form.Group className="mb-3">
            <Form.Control
              id="Desc"
              type="text"
              className="mt-1"
              placeholder="Review"
            />
            <Form.Control
              id="Rating"
              type="number"
              min="0"
              max="10"
              className="mt-1"
              placeholder="Rating"
            />
          </Form.Group>
          <Button
            variant="primary"
            onClick={(e) => {
              setShowReviews(false);
              addReview(e);
            }}
          >
            Add my review
          </Button>
        </Form>
      </Modal.Footer>
    </Modal>
  );
}
