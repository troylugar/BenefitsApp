import { useState } from "react";
import { Redirect } from 'react-router-dom';
import api from "../api/api";
import { handleChange } from "../helper";

function CreateEmployee(props) {
  const [redirect, setRedirect] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');

  const onSubmit = (event) => {
    event.preventDefault();
    return api.createEmployee({
      firstName,
      lastName
    }).then(() => {
      setRedirect(true);
    });
  };

  const showRedirect = () => {
    if (redirect) {
      return <Redirect to="/Employees" />;
    }
  }

  return (
    <form onSubmit={ onSubmit } >
      { showRedirect() }
      <h1 className="my-3">Create Employee</h1>
      <div className="row">
        <div className="col-6 row">
          <label for="firstName" className="col-2 col-form-label">First Name</label>
          <div className="col-10">
            <input id="firstName" type="text" className="form-control" onChange={handleChange(setFirstName)} />
          </div>
        </div>
        <div className="col-6 row">
          <label for="lastName" className="col-2 col-form-label">Last Name</label>
          <div className="col-10">
            <input id="lastName" type="text" className="form-control" onChange={handleChange(setLastName)} />
          </div>
        </div>
      </div>
      <input type="submit" className="my-3 btn btn-primary" value="Create Employee" />
    </form >
  );
}

export default CreateEmployee;