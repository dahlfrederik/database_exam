import React, { useEffect, useState } from "react";
import { Button, Form, Modal, Table } from "react-bootstrap";
import Select from "react-select";
import ActorToMovie from "../ActorToMovie";
import AddMovie from "../AddMovie";
import facade from "../api/MovieFacade";

export default function AdminModal({ adminVisable, handleAdminClose }) {
  const [users, setUsers] = useState([]);

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
        <ActorToMovie />
        <h3 className="text-center mt-5">User list</h3>
        <UserList />
      </Modal.Body>
      <Modal.Footer></Modal.Footer>
    </Modal>
  );
}
