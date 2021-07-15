import { useEffect, useState } from "react";
import { Redirect, useParams, Link } from "react-router-dom";
import api from "../api/api";
import { handleChange } from "../helper";

function EditEmployee(props) {
  const { employeeId } = useParams();
  const [employee, setEmployee] = useState(false);

  const [redirect, setRedirect] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [salary, setSalary] = useState(0);
  const [startDate, setStartDate] = useState(new Date());

  useEffect(() => {
    api.getEmployeeById(employeeId).then(data => {
      setEmployee(data);
      setFirstName(data.firstName);
      setLastName(data.lastName);
      setSalary(data.salary);
      const isoDate = data.startDate.substring(0,10);
      setStartDate(isoDate);
    });
  }, [employeeId]);

  const onSubmit = (event) => {
    const d = new Date(startDate);
    api.modifyEmployee(employeeId, {
      firstName,
      lastName,
      salary,
      startDate: d.toISOString()
    });
    setRedirect(true);
    event.preventDefault();
  };

  const deleteDependent = (employeeId, dependentId) => () => {
    api.removeDependent(employeeId, dependentId).then(() => {
      setRedirect(true);
    });
  };

  const showRedirect = () => {
    if (redirect) {
      return <Redirect to={`/Employees/${employeeId}`} />
    }
  };

  if (!employee) {
    return <div></div>;
  }

  return (
    <div>
      <form onSubmit={onSubmit} >
        {showRedirect()}
        <h1 className="my-3">Edit Employee</h1>
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
        <div className="row">
          <div className="col-6 row">
            <label for="salary" className="col-2 col-form-label">Salary</label>
            <div className="col-10">
              <input id="salary" type="number" className="form-control" onChange={handleChange(setSalary)} value={salary} />
            </div>
          </div>
          <div className="col-6 row">
            <label for="start-date" className="col-2 col-form-label">Salary</label>
            <div className="col-10">
              <input id="start-date" type="date" className="form-control" onChange={handleChange(setStartDate)} value={startDate} />
            </div>
          </div>
        </div>
        <input type="submit" className="my-3 btn btn-primary" value="Save Employee" />
      </form>
      <table>
        <thead>
          <tr>
            <th>Dependent</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employee.dependents.map(dependent => (
            <tr>
              <td>{dependent.firstName} {dependent.lastName}</td>
              <td>
                <div className="btn-group btn-group-sm" role="group">
                  <Link to={`/employees/${employee.id}/dependents/${dependent.id}/edit`} className="btn btn-outline-warning">
                    <i className="my-1 bi bi-pencil"></i>
                    Edit
                  </Link>
                  <button onClick={deleteDependent(employee.id, dependent.id)} className='btn btn-outline-danger'>
                    <i className="my-1 bi bi-trash"></i>
                    Delete
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to={`/employees/${employee.id}/dependents/create`} className="btn btn-primary">Create a Dependent</Link>
    </div>
  );
}

export default EditEmployee;