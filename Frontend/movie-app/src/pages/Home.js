import React, { useState, useEffect } from "react";
import facade from "../components/api/MovieFacade";
import CreateCard from "../components/home/CreateCard";
import AdminModal from "../components/modals/AdminModal";

export default function Home({ user }) {
  const [movieList, setMovieList] = useState(null);
  const [adminVisable, setAdminVisable] = useState(false);

  useEffect(() => {
    if (!movieList) {
      const movies = facade.getMovies();
      movies.then((e) => setMovieList(e));
    }
  }, [movieList]);

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
          {user.Role == 2 ? (
            <div>
              <button
                className="btn btn-primary m-3"
                onClick={() => setAdminVisable(true)}
              >
                Admin Modal
              </button>

              <AdminModal
                adminVisable={adminVisable}
                handleAdminClose={setAdminVisable}
                myUser={user}
              />
            </div>
          ) : null}

          <p>If you wish to add your own rating click the add rating button</p>
        </div>
      </div>
      <div>
        {movieList
          ? movieList.map((e) => (
              <CreateCard
                title={e.Title}
                tagline={e.Tagline}
                released={e.Released}
                review={10}
              />
            ))
          : null}
      </div>
    </div>
  );
}
