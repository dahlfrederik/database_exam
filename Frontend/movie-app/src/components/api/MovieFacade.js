import { SERVER_URL } from "../../util/Settings";

function handleHttpErrors(res) {
  if (!res.ok) {
    return Promise.reject({ status: res.status, fullError: res.json() });
  }
  return res.json();
}

function movieFacade() {
  const getMovies = () => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Movie/movie/", options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  const getMovie = (movietitle) => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Movie/movie/" + movietitle, options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  const getActors = () => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Movie/actor/", options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  const getTopFive = () => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Movie/movie/topfive/", options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  const getActor = (actorname) => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Movie/actor/" + actorname, options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  function addMovie(title, tagline, released) {
    const options = makeOptions("POST", {
      Title: title,
      Tagline: tagline,
      Released: released,
    });
    return fetch(SERVER_URL + "Movie/movie", options).then(handleHttpErrors);
  }

  function addActorToMovie(actorName, movieTitle) {
    const options = makeOptions("POST");
    return fetch(
      SERVER_URL + `Movie/movie/${actorName}/${movieTitle}`,
      options
    ).then(handleHttpErrors);
  }

  function addNewActorToMovie(actor, movieTitle) {
    const options = makeOptions("POST", {
      Name: actor.name,
      Born: actor.born,
    });
    return fetch(SERVER_URL + `Movie/movie/${movieTitle}`, options).then(
      handleHttpErrors
    );
  }

  const makeOptions = (method, body) => {
    var opts = {
      method: method,
      headers: {
        "Content-type": "application/json",
        Accept: "application/json",
      },
    };
    if (body) {
      opts.body = JSON.stringify(body);
    }
    return opts;
  };

  return {
    makeOptions,
    getMovies,
    getMovie,
    getActor,
    getActors,
    getTopFive,
    addMovie,
    addActorToMovie,
    addNewActorToMovie,
  };
}
const facade = movieFacade();
export default facade;
