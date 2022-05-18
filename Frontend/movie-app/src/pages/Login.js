import React, { useState } from "react";

export default function Login({ login }) {
  const [loginInfo, setLoginInfo] = useState({ username: "", password: "" });

  const performLogin = (e) => {
    e.preventDefault();
    login(loginInfo.username, loginInfo.password);
  };

  const onChange = (evt) => {
    setLoginInfo({
      ...loginInfo,
      [evt.target.id]: evt.target.value,
    });
  };

  return (
    <form onChange={onChange}>
      <h3>Sign In</h3>
      <div className="mb-3">
        <label>Username</label>
        <input
          type="username"
          className="form-control"
          placeholder="Enter username"
          id="username"
        />
      </div>
      <div className="mb-3">
        <label>Password</label>
        <input
          className="form-control"
          placeholder="Enter password"
          type="password"
          id="password"
        />
      </div>
      <div className="d-grid">
        <button
          type="submit"
          className="btn btn-primary"
          onClick={performLogin}
        >
          Login
        </button>
      </div>
      <p className="signup text-right">
        Don't have an account? <a href="/sign-up">sign up</a>
      </p>
    </form>
  );
}
