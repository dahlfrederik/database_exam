import React, { useEffect, useState } from "react";
import {
  Button,
  Col,
  Container,
  Form,
  Modal,
  Row,
  Table,
} from "react-bootstrap";
import Select from "react-select";
import facade from "../api/MovieFacade";

export default function AdminModal({
  adminVisable,
  handleAdminClose,
  movieList,
}) {
  const [users, setUsers] = useState([]);
  const [actorOptions, setActorOptions] = useState(null);
  const [movieOptions, setMovieOptions] = useState(null);
  const [pickedActor, setPickedActor] = useState("Actor..");
  const [pickedMovie, setPickedMovie] = useState("Movie..");

  useEffect(() => {
    if (!actorOptions) {
      const actors = facade.getActors();
      actors.then((arr) =>
        setActorOptions(
          arr.map((actor) => ({
            value: actor.Name,
            label: actor.Name,
          }))
        )
      );
    }
    if (!movieOptions) {
      const movies = facade.getMovies();
      movies.then((arr) =>
        setMovieOptions(arr.map((e) => ({ value: e.Title, label: e.Title })))
      );
    }
  }, [actorOptions]);

  const AddMovie = () => {
    return (
      <Form>
        <Form.Group className="mb-3">
          <Form.Control type="text" placeholder="Movie title" />
          <Form.Control type="text" className="mt-1" placeholder="Tagline" />
          <Form.Control
            type="number"
            className="mt-1"
            placeholder="Year of release"
          />
        </Form.Group>
        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    );
  };

  const AddNewActor = () => {
    return (
      <div>
        <div className="d-flex justify-content-evenly">
          <Select
            placeholder={pickedActor}
            options={actorOptions}
            form={"true"}
            onChange={(actor) => setPickedActor(actor.value)}
          />
          <Select
            placeholder={pickedMovie}
            options={movieOptions}
            form={"true"}
            onChange={(actor) => setPickedMovie(actor.value)}
          />
        </div>
        <Button className="mt-2" variant="primary" type="submit">
          Submit
        </Button>
      </div>
    );
  };

  const UserList = () => {
    return (
      <Table bordered hover>
        <thead>
          <tr>
            <th>ID</th>
            <th>Username</th>
            <th>Email</th>
            <th>Created</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>1</td>
            <td>Mark</td>
            <td>Otto</td>
            <td>@mdo</td>
          </tr>
          <tr>
            <td>2</td>
            <td>Jacob</td>
            <td>Thornton</td>
            <td>@fat</td>
          </tr>
          <tr>
            <td>3</td>
            <td colSpan={2}>Larry the Bird</td>
            <td>{pickedActor}</td>
            <td>
              <button className="btn btn-primary">Promote</button>
              <button className="btn btn-primary">Remove</button>
            </td>
          </tr>
        </tbody>
      </Table>
    );
  };

  return (
    <Modal centered show={adminVisable} onHide={handleAdminClose}>
      <Modal.Header closeButton>
        <Modal.Title>Admin Panel</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h3>Add a new movie</h3>
        <AddMovie />
        <h3 className="mt-5">Add actor to movie</h3>
        <AddNewActor />
        <h3 className="text-center mt-5">User list</h3>
        <UserList />
      </Modal.Body>
      <Modal.Footer></Modal.Footer>
    </Modal>
  );
}
