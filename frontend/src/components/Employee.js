import { useEffect, useState } from "react";
import { useParams } from "react-router";
import api from "../api/api";
import { formatter } from "../helper";

function Employee(props) {
  const { employeeId } = useParams();
  const [employee, setEmployee] = useState(false);
  useEffect(() => {
    api.getEmployeeById(employeeId).then(setEmployee);
  }, [employeeId]);
  if (!employee) return <div></div>;

  return (
    <div>
      <h1>{employee.firstName} {employee.lastName}</h1>
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
    </div>
  );
}

export default Employee;