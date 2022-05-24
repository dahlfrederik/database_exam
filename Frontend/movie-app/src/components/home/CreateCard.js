import ReviewModal from "./ReviewModal";
import React, { useState } from "react";

export default function CreateCard({ title, tagline, released, rating }) {
  const [showReviews, setShowReviews] = useState(false);
  return (
    <div class="moviecardcontainer">
      <div class="moviecard">
        <h4>
          <b>{title}</b>
        </h4>
        <p>{tagline}</p>
        <p>{released}</p>
      </div>
      <div class="movierating">
        <p>Rating: {rating}</p>
        <p> lorem ipsum dorsum porsum bla bal bla </p>

        <div>
          <button
            type="submit"
            onClick={() => setShowReviews(true)}
            className="btn btn-primary"
          >
            See more reviews
          </button>
          <ReviewModal
            showReviews={showReviews}
            setShowReviews={setShowReviews}
            movieTitle={title}
          />
        </div>
      </div>
    </div>
  );
}
