import React, { useEffect, useState } from "react";
import { Button, Form, Modal, Table } from "react-bootstrap";
import Select from "react-select";
import ActorToMovie from "../ActorToMovie";
import AddMovie from "../AddMovie";
import facade from "../api/UserFacade";

export default function AdminModal({ adminVisable, handleAdminClose, myUser }) {
  const [userList, setUserList] = useState(null);

  useEffect(() => {
    if (!userList) {
      const users = facade.getUsers();
      users.then((e) => setUserList(e));
    }
  }, [userList]);

  const UserList = () => {
    return (
      <Table bordered hover>
        <thead>
          <tr>
            <th>Username</th>
            <th>Email</th>
            <th>Created</th>
            <th>Role</th>
          </tr>
        </thead>
        <tbody>
          {userList.map((user) => (
            <tr>
              <td>{user.Username}</td>
              <td>{user.Email}</td>
              <td>{user.Timestamp}</td>
              <td>{user.Role == 2 ? "Admin" : "User"}</td>
              <td>
                <button
                  className="btn btn-primary"
                  onClick={() =>
                    facade.promoteUserToAdmin(myUser.Username, user.Username)
                  }
                >
                  Promote
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    );
  };

  return (
    <Modal size="lg" centered show={adminVisable} onHide={handleAdminClose}>
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
