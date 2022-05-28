import React, { useState } from "react";
import facade from "../components/api/UserFacade";

export default function Signup() {
  const [userInfo, setUserInfo] = useState({
    username: "",
    password: "",
    email: "",
  });

  const register = (e) => {
    e.preventDefault();
    const created = facade.addUser(
      userInfo.username,
      userInfo.password,
      userInfo.email
    );
    created.catch(console.log("failed"));
  };

  const onChange = (evt) => {
    setUserInfo({
      ...userInfo,
      [evt.target.id]: evt.target.value,
    });
  };

  return (
    <form onChange={onChange}>
      <h3>Sign Up</h3>
      <div className="mb-3">
        <label>Email address</label>
        <input
          id="email"
          type="email"
          className="form-control"
          placeholder="Enter email"
        />
      </div>
      <div className="mb-3">
        <label>Username</label>
        <input
          id="username"
          type="username"
          className="form-control"
          placeholder="Enter username"
        />
      </div>
      <div className="mb-3">
        <label>Password</label>
        <input
          id="password"
          type="password"
          className="form-control"
          placeholder="Enter password"
        />
      </div>
      <div className="d-grid">
        <button type="submit" className="btn btn-primary" onClick={register}>
          Sign Up
        </button>
      </div>
      <p className="signup text-right">
        Already registered <a href="/sign-in">sign in?</a>
      </p>
    </form>
  );
}
