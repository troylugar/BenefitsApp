import { useEffect, useState } from "react";
import { useRouteMatch, Link } from "react-router-dom";
import { useParams } from "react-router";
import api from "../api/api";
import { formatter } from "../helper";

function Employee(props) {
  const match = useRouteMatch();
  const { employeeId } = useParams();
  const [employee, setEmployee] = useState(false);
  useEffect(() => {
    api.getEmployeeById(employeeId).then(setEmployee);
  }, [employeeId]);
  if (!employee) return <div></div>;

  return (
    <div>
      <h1>{employee.firstName} {employee.lastName}</h1>
      <dd className="fw-bold">Start Date</dd>
      <dl>{(new Date(employee.startDate)).toDateString()}</dl>
      <dd className="fw-bold">Salary</dd>
      <dl>{formatter.format(employee.salary)}</dl>
      <dd className="fw-bold">Cost of benefits</dd>
      <dl>{formatter.format(employee.costOfBenefits)}</dl>
      <dd className="fw-bold">Enrollments</dd>
      <dl>
        <ul>
          { employee.enrollments.map(enrollment => (
            <li key={enrollment.benefit}>{enrollment.benefit}</li>
          )) }
        </ul>
      </dl>
      <dd className="fw-bold">Dependents</dd>
      <dl>
        <ul>
          {employee.dependents.map(dependent =>
            <li key={dependent.id}>
              {dependent.firstName} {dependent.lastName}
            </li>)
          }
        </ul>
      </dl>
      <Link className="btn btn-primary" to={`${match.url}/edit`}>Edit</Link>
    </div>
  );
}

export default Employee;