import { SERVER_URL } from "../../util/Settings";

function handleHttpErrors(res) {
  if (!res.ok) {
    return Promise.reject({ status: res.status, fullError: res.json() });
  }
  return res.json();
}

function reviewFacade() {
  const getLatestReviews = (movieId) => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Review/Movie/LatestReviews/" + movieId, options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  const getRating = (movieId) => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "Review/Movie/Rating/" + movieId, options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  const addReview = (movieId, userId, userName, desc, rating) => {
    const options = makeOptions("POST", {
      MovieId: "" + movieId,
      UserId: "" + userId,
      Username: userName,
      Desc: desc,
      Rating: parseInt(rating),
    });
    return fetch(SERVER_URL + "Review", options).then(handleHttpErrors);
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
    getLatestReviews,
    getRating,
    addReview,
  };
}

const facade = reviewFacade();
export default facade;
