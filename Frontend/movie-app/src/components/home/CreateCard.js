import ReviewModal from "./ReviewModal";
import MovieModal from "./MovieModal";
import React, { useState } from "react";

export default function CreateCard({ title, tagline, released, rating }) {
  const [showReviews, setShowReviews] = useState(false);
  const [showMovie, setShowMovie] = useState(false);
  return (
    <div class="moviecardcontainer">
      <div class="moviecard">
        <h4>
          <b>{title}</b>
        </h4>
        <p>{tagline}</p>
        <p>{released}</p>
        <div>
          <button
            type="submit"
            onClick={() => setShowMovie(true)}
            className="btn btn-light"
          >
            See actors
          </button>
          <MovieModal
            showMovie={showMovie}
            setShowMovie={setShowMovie}
            movieTitle={title}
          />
        </div>
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
