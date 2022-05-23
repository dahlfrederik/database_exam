import React, { useState, useEffect } from "react";
import facade from "../components/api/MovieFacade";

export default function Home({ user }) {
  const [movieList, setMovieList] = useState(null);

  useEffect(() => {
    if (!movieList) {
      const movies = facade.getMovies();
      movies.then((e) => setMovieList(e));
    }
  }, [movieList]);

  function createCard(title, tagline, released, rating) {
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
        </div>
      </div>
    );
  }

  return (
    <div>
      <div>
        <div className="mx-10 text-center">
          <h1>MOVIE MAFIA</h1>
          <h3>
            Welcome back <b>{user.Username}</b>
          </h3>
          <p>
            Below is the list of all our <b>Movies</b> and <b>Series</b> and
            their ratings.
          </p>
          <p>If you wish to add your own rating click the add rating button</p>
        </div>
      </div>
      <div>
        {movieList
          ? movieList.map((e) => createCard(e.Title, e.Tagline, e.Released, 10))
          : null}
      </div>
    </div>
  );
}
