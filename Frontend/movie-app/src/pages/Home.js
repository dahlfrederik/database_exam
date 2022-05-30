import React, { useState, useEffect } from "react";
import facade from "../components/api/MovieFacade";
import CreateCard from "../components/home/CreateCard";
import AdminModal from "../components/modals/AdminModal";
import TopFiveModal from "../components/modals/TopFiveModal";

export default function Home({ user, logout }) {
  const [movieList, setMovieList] = useState(null);
  const [adminVisable, setAdminVisable] = useState(false);
  const [topFiveVisible, setTopFiveVisible] = useState(false);

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
          <div>
            <h1>MOVIE MAFIA</h1>
            <button
              style={{
                position: "absolute",
                right: "0",
                top: "0",
              }}
              className="btn btn-danger m-4"
              onClick={() => logout()}
            >
              Logout
            </button>
          </div>
          <h3>
            Welcome back <b>{user?.Username}</b>
          </h3>

          <p>
            Below is the list of all our <b>Movies</b> and <b>Series</b> and
            their ratings.
          </p>
          {user?.Role == 2 ? (
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
          <div>
            <button
              className="btn btn-primary m-3"
              onClick={() => setTopFiveVisible(true)}
            >
              Top Five Modal
            </button>

            <TopFiveModal
              topFiveVisible={topFiveVisible}
              handleTopFiveClose={setTopFiveVisible}
            />
          </div>

          <p>
            If you wish to add your own rating click the see all reviews and
            then add a review
          </p>
        </div>
      </div>
      <div>
        {movieList
          ? movieList.map((e) => (
              <CreateCard
                title={e.Title}
                tagline={e.Tagline}
                released={e.Released}
                movieId={e.Id}
                myUser={user}
              />
            ))
          : null}
      </div>
      //{" "}
    </div>
  );
}
