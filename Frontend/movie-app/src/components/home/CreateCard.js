import ReviewModal from "../modals/ReviewModal";
import MovieModal from "../modals/MovieModal";
import ActorModal from "../modals/ActorModal";
import React, { useState, useEffect } from "react";
import facade from "../api/ReviewFacade";
import ShowRating from "./starrating/ShowRating";

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
    <div class="moviecardcontainer mt-2">
      <div class="moviecard p-2">
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
        <h4>Average rating</h4>
        <ShowRating avgRating={rating} />
        <p> Click button below to see all user reviews </p>
        <p> It is even possible to add your own review </p>
        <div>
          <button
            type="submit"
            onClick={() => {
              setMovieTitle(title);
              setShowReviews(true);
            }}
            className="btn btn-primary"
          >
            See reviews
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
