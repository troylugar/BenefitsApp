import { useEffect, useState } from "react";
import { useParams } from "react-router";
import api from "../api/api";
import { formatter } from "../helper";

function Enrollment(props) {
  const { enrollmentId } = useParams();
  const [enrollmentData, setEnrollment] = useState(false);
  useEffect(() => {
    api.getEnrollmentById(enrollmentId).then(setEnrollment);
  }, [enrollmentId]);

  if (!enrollmentData) {
    return <div></div>;
  }
  const { enrollment, calculations } = enrollmentData;
  return (
    <div className="mt-3">
      <dd className="fw-bold">Name</dd>
      <dl>{enrollment.employee.firstName} {enrollment.employee.lastName}</dl>
      <dd className="fw-bold">Total Cost</dd>
      <dl>{formatter.format(calculations.total)}</dl>
      <dd className="fw-bold">Dependents</dd>
      <dl>
        <ul>
          {enrollment.employee.dependents.map((dependent, i) => (
            <li key={i}>{dependent.firstName} {dependent.lastName}</li>
          ))}
        </ul>
      </dl>

      <h3 className="mt-3">Line Items</h3>
      <table className="w-100">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
          {calculations.lineItems.map((item, i) => (
            <tr key={i}>
              <th>{item.name}</th>
              <td>{item.description}</td>
              <td>{formatter.format(item.amount)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default Enrollment;