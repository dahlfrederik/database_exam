import React, { useEffect, useState } from "react";
import { Button, Form, Modal, Table } from "react-bootstrap";
import facade from "../api/MovieFacade";

export default function TopFiveModal({ topFiveVisible, handleTopFiveClose }) {
  const [topFiveList, setTopFiveList] = useState(null);
  const [timer, setTimer] = useState(0);

  useEffect(() => {
    var t0 = performance.now();
    if (!topFiveList) {
      const movies = facade.getTopFive();
      movies
        .then((e) => setTopFiveList(e))
        .then(() => setTimer(performance.now() - t0));
    }
  }, [topFiveList]);

  const MovieList = () => {
    var count = 1;
    return (
      <Table bordered hover>
        <thead>
          <tr>
            <th>Top Five Movies</th>
          </tr>
        </thead>
        <tbody>
          {topFiveList.map((movie) => (
            <tr>
              <td>
                {count++}. {movie}
              </td>
            </tr>
          ))}
        </tbody>
        <p>Fetch timer: {timer}</p>
      </Table>
    );
  };

  return (
    <Modal size="lg" centered show={topFiveVisible} onHide={handleTopFiveClose}>
      <Modal.Header closeButton>
        <Modal.Title>Top Five Movies</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <MovieList />
      </Modal.Body>
      <Modal.Footer></Modal.Footer>
    </Modal>
  );
}
