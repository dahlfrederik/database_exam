import ReviewModal from "./ReviewModal";
import MovieModal from "./MovieModal";
import ActorModal from "./ActorModal";
import React, { useState, useEffect } from "react";
import facade from "../api/ReviewFacade";

export default function CreateCard({
  title,
  tagline,
  released,
  movieId,
  myUser,
}) {
  const [showReviews, setShowReviews] = useState(false);
  const [showMovie, setShowMovie] = useState(false);
  const [showActor, setShowActor] = useState(false);
  const [actorName, setActorName] = useState("");
  const [movieTitle, setMovieTitle] = useState("");
  const [rating, setRating] = useState(null);

  function handleRating() {
    facade.getRating(movieId).then((e) => setRating(e));
  }

  useEffect(() => {
    if (!rating) {
      handleRating(movieId);
    }
  }, [rating]);

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
            onClick={() => {
              setMovieTitle(title);
              setShowMovie(true);
            }}
            className="btn btn-light"
          >
            See actors
          </button>
          <MovieModal
            showMovie={showMovie}
            setShowMovie={setShowMovie}
            movieTitle={movieTitle}
            setMovieTitle={setMovieTitle}
            setActorName={setActorName}
            setShowActor={setShowActor}
            setShowReviews={setShowReviews}
          />
          <ActorModal
            showActor={showActor}
            setShowActor={setShowActor}
            actorName={actorName}
            setMovieTitle={setMovieTitle}
            setShowMovie={setShowMovie}
          />
        </div>
      </div>
      <div class="movierating">
        <p>Rating: {rating}</p>
        <p> lorem ipsum dorsum porsum bla bal bla </p>
        <div>
          <button
            type="submit"
            onClick={() => {
              setMovieTitle(title);
              setShowReviews(true);
            }}
            className="btn btn-primary"
          >
            See more reviews
          </button>
          <ReviewModal
            showReviews={showReviews}
            setShowReviews={setShowReviews}
            movieTitle={movieTitle}
            movieId={movieId}
            myUser={myUser}
            rating={rating}
          />
        </div>
      </div>
    </div>
  );
}
