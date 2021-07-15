import { useRouteMatch } from "react-router";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import api from "../api/api";

function EmployeesTable(props) {
  const match = useRouteMatch();
  const [employees, setEmployees] = useState([]);

  const getEmployees = () => api.getEmployees().then(setEmployees);
  useEffect(() => {
    getEmployees();
  }, []);

  const deleteEmployee = (id) => () => {
    api.removeEmployee(id).then(() => {
      getEmployees();
    });
  };

  return (
    <div>
      <h1 className="my-3">Employees</h1>
      <table className='w-100'>
        <thead>
          <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map(employee => (
            <tr key={employee.id}>
              <td>{employee.firstName}</td>
              <td>{employee.lastName}</td>
              <td>
                <div className="btn-group btn-group-sm" role="group">
                  <Link to={`${match.path}/${employee.id}`} className='btn btn-outline-primary'>
                    <i className="my-1 bi bi-info"></i>
                    View
                  </Link>
                  <Link to={`${match.path}/${employee.id}/edit`} className='btn btn-outline-warning'>
                    <i className="my-1 bi bi-pencil"></i>
                    Edit
                  </Link>
                  <button onClick={deleteEmployee(employee.id)} className='btn btn-outline-danger'>
                    <i className="my-1 bi bi-trash"></i>
                    Delete
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to={`${match.path}/create`} className="btn btn-primary">
        Add Employee
      </Link>
    </div>
  );
}

export default EmployeesTable;