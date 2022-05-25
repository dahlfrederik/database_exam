import { SERVER_URL } from "../../util/Settings";

function handleHttpErrors(res) {
  if (!res.ok) {
    return Promise.reject({ status: res.status, fullError: res.json() });
  }
  return res.json();
}

function userFacade() {
  const setUser = (user) => {
    localStorage.setItem("user", user);
  };

  const getUser = () => {
    return localStorage.getItem("user");
  };

  const loggedIn = () => {
    const loggedIn = getUser() != null;
    return loggedIn;
  };

  const logout = () => {
    localStorage.removeItem("user");
  };

  const login = (username, password) => {
    const options = makeOptions("POST", true, {
      username: username,
      password: password,
    });

    return fetch(SERVER_URL + "User/users/login", options)
      .then(handleHttpErrors)
      .then((res) => {
        setUser(JSON.stringify(res));
      });
  };

  const getUsers = () => {
    const options = makeOptions("GET");

    return fetch(SERVER_URL + "User/users/", options)
      .then(handleHttpErrors)
      .then((res) => {
        return res;
      });
  };

  function addUser(email, password, username) {
    const options = makeOptions("POST", true, {
      email: email,
      password: password,
      username: username,
    });
    return fetch(SERVER_URL, options).then(handleHttpErrors);
  }

  function promoteUserToAdmin(myUsername, username) {
    const options = makeOptions("POST");
    return fetch(
      SERVER_URL + `User/users/${myUsername}/${username}`,
      options
    ).then(handleHttpErrors);
  }

  const makeOptions = (method, addUser, body) => {
    var opts = {
      method: method,
      headers: {
        "Content-type": "application/json",
        Accept: "application/json",
      },
    };
    if (addUser && loggedIn()) {
      opts.headers["auth-user"] = getUser();
    }
    if (body) {
      opts.body = JSON.stringify(body);
    }
    return opts;
  };
  return {
    makeOptions,
    setUser,
    getUser,
    loggedIn,
    login,
    logout,
    addUser,
    getUsers,
    promoteUserToAdmin,
  };
}
const facade = userFacade();
export default facade;
