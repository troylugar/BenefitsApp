import { useState } from "react";
import { Redirect, useParams } from "react-router-dom";
import api from "../api/api";
import { handleChange } from "../helper";

function CreateDependent(props) {
  const { employeeId } = useParams();

  const [redirect, setRedirect] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');

  const onSubmit = (event) => {
    api.createDependent(employeeId, {
      firstName,
      lastName
    });
    setRedirect(true);
    event.preventDefault();
  };

  const showRedirect = () => {
    if (redirect) {
      return <Redirect to={`/Employees/${employeeId}`} />
    }
  };

  return (
    <form onSubmit={ onSubmit } >
      { showRedirect() }
      <h1 className="my-3">Create Dependent</h1>
      <div className="row">
        <div className="col-6 row">
          <label for="firstName" className="col-2 col-form-label">First Name</label>
          <div className="col-10">
            <input id="firstName" type="text" className="form-control" onChange={handleChange(setFirstName)} value={firstName} />
          </div>
        </div>
        <div className="col-6 row">
          <label for="lastName" className="col-2 col-form-label">Last Name</label>
          <div className="col-10">
            <input id="lastName" type="text" className="form-control" onChange={handleChange(setLastName)} value={lastName} />
          </div>
        </div>
      </div>
      <input type="submit" className="my-3 btn btn-primary" value="Create Dependent" />
    </form >
  );
}

export default CreateDependent;