import { useState } from "react";
import { Redirect } from 'react-router-dom';
import api from "../api/api";
import { handleChange } from "../helper";

function CreateEmployee(props) {
  const [redirect, setRedirect] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [salary, setSalary] = useState(0);
  const [startDate, setStartDate] = useState(new Date());

  const onSubmit = (event) => {
    event.preventDefault();
    return api.createEmployee({
      firstName,
      lastName,
      salary,
      startDate
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
    <form onSubmit={onSubmit} >
      {showRedirect()}
      <h1 className="my-3">Create Employee</h1>
      <div className="row">
        <div className="col-6 row">
          <label htmlFor="firstName" className="col-2 col-form-label">First Name</label>
          <div className="col-10">
            <input id="first-name" type="text" className="form-control" onChange={handleChange(setFirstName)} />
          </div>
        </div>
        <div className="col-6 row">
          <label htmlFor="lastName" className="col-2 col-form-label">Last Name</label>
          <div className="col-10">
            <input id="last-name" type="text" className="form-control" onChange={handleChange(setLastName)} />
          </div>
        </div>
      </div>
      <div className="row">
        <div className="col-6 row">
          <label htmlFor="salary" className="col-2 col-form-label">Salary</label>
          <div className="col-10">
            <input id="salary" type="number" className="form-control" onChange={handleChange(setSalary)} />
          </div>
        </div>
        <div className="col-6 row">
          <label htmlFor="start-date" className="col-2 col-form-label">Salary</label>
          <div className="col-10">
            <input id="start-date" type="date" className="form-control" onChange={handleChange(setStartDate)} />
          </div>
        </div>
      </div>
      <input type="submit" className="my-3 btn btn-primary" value="Create Employee" />
    </form >
  );
}

export default CreateEmployee;