import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import api from "../api/api";
import { formatter } from "../helper";

function EnrollmentsTable() {
  const [enrollmentData, setEnrollments] = useState({
    enrollments: [],
    cost: null
  });

  const enrollments = enrollmentData.enrollments;
  const cost = enrollmentData.cost;

  const removeEnrollment = id => {
    api.removeEnrollmentById(id).then(() => {
      api.getEnrollments().then(setEnrollments)
    });
  }

  useEffect(() => {
    api.getEnrollments().then(setEnrollments)
  }, []);

  return (
    <div>
      <h3>Total cost: {formatter.format(cost)}</h3>
      <table className="w-100">
        <thead>
          <tr>
            <th>Employee</th>
            <th>Benefit</th>
            <th>Discounts</th>
            <th>Cost</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {enrollments.map(enrollment => (
            <tr key={enrollment.id}>
              <td>{enrollment.employee.firstName} {enrollment.employee.lastName}</td>
              <td>{enrollment.benefit}</td>
              <td>
                <ul>
                  {enrollment.discounts.map(discount => <li key={discount}>{discount}</li>)}
                </ul>
              </td>
              <td>{formatter.format(enrollment.cost)}</td>
              <td>
                <div className="btn-group btn-group-sm" role="group">
                  <Link to={`/enrollments/${enrollment.id}`} className="btn btn-outline-primary">
                    <i className="mx-1 bi bi-info"></i>
                    Details
                  </Link>
                  <button onClick={() => removeEnrollment(enrollment.id)} className="btn btn-outline-danger">
                    <i className="mx-1 bi bi-trash"></i>
                    Remove
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default EnrollmentsTable;