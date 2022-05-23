import { useEffect, useState } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import facade from "./components/api/UserFacade";
import Home from "./pages/Home";
import Login from "./pages/Login";
import NoMatch from "./pages/NoMatch";
import Signup from "./pages/Signup";

function App() {
  const [loggedIn, setLoggedIn] = useState(false);
  const [user, setUser] = useState(null);

  const login = (username, password) => {
    try {
      facade.login(username, password).then(() => setLoggedIn(true));
    } catch (error) {
      console.log(error);
    }
  };

  const logout = () => {
    facade.logout();
    setLoggedIn(false);
  };

  useEffect(() => {
    if (loggedIn) {
      const user = facade.getUser();
      setUser(JSON.parse(user));
    }
  }, [loggedIn]);

  return (
    <Router>
      <div className="App">
        {user ? (
          <Routes>
            <Route exact path="/" element={<Home user={user} />} />
            <Route path="*" element={<NoMatch />} />
          </Routes>
        ) : (
          <div className="auth-wrapper">
            <div className="auth-inner">
              <Routes>
                <Route exact path="/" element={<Login login={login} />} />
                <Route path="/sign-in" element={<Login login={login} />} />
                <Route path="/sign-up" element={<Signup />} />
                <Route path="*" element={<NoMatch />} />
              </Routes>
            </div>
          </div>
        )}
      </div>
    </Router>
  );
}

export default App;
