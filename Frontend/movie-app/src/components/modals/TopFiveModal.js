import React, { useEffect, useState } from "react";
import { Button, Form, Modal, Table } from "react-bootstrap";
import facade from "../api/MovieFacade";


export default function AdminModal({ topFiveVisible, handleTopFiveClose}) {
    const [movieList, setmovieList] = useState(null);
    const [timer, setTimer] = useState(0);

    useEffect(() => {
        var t0 = performance.now();
        if (!movieList) {
            const movies = facade.getTopFive();
            movies.then((e) => setmovieList(e))
            .then(() => setTimer( performance.now() - t0));
        }
        

    }, [movieList]);

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
                    {movieList.map((movie) => (
                        <tr>
                            <td>{count++}. {movie}</td>
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
