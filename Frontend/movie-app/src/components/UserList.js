import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import facade from "./api/UserFacade";

export default function UserList() {
  const [userList, setUserList] = useState(null);

  useEffect(() => {
    if (!userList) {
      const users = facade.getUsers();
      users.then((e) => setUserList(e));
    }
  }, [userList]);

  return (
    <div>
      <Table bordered hover responsive>
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
              <td>{user.UsernameF}</td>
              <td>{user.Email}</td>
              <td>{user.Timestamp}</td>
              <td>{user.Role}</td>
              <td>
                <button className="btn btn-primary">Promote</button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
}
