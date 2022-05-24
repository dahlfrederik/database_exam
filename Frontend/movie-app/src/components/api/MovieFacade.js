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

  const getActors = () => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Movie/actor/", options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

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
    getActors,
  };
}
const facade = movieFacade();
export default facade;
