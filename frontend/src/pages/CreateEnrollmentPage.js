import { useEffect, useState } from "react";
import { Redirect } from "react-router-dom";
import api from "../api/api";
import { handleChange } from "../helper";

function CreateEnrollmentPage(props) {
  const [redirect, setRedirect] = useState(false);
  const [discountTypes, setDiscountTypes] = useState([]);
  const [benefitTypes, setBenefitTypes] = useState([]);
  const [employees, setEmployees] = useState([]);
  useEffect(() => {
    api.getDiscounts().then(data => setDiscountTypes(data));
    api.getBenefits().then(data => setBenefitTypes(data));
    api.getEmployees().then(data => setEmployees(data));
  }, []);

  const [selectedDiscounts, setSelectedDiscounts] = useState([]);
  const [selectedBenefit, setSelectedBenefit] = useState('');
  const [selectedEmployee, setSelectedEmployee] = useState('');

  const getMultipleSelected = (hook) => (event) => {
    const values = [...event.target.children]
      .filter(x => x.selected)
      .map(x => x.value)
    hook(values);
  }

  const onSubmit = (event) => {
    event.preventDefault();
    api.createEnrollment(selectedEmployee, selectedBenefit, selectedDiscounts)
      .then(() => {
        setRedirect(true);
      });
  };


  const showRedirect = () => {
    if (redirect) {
      return <Redirect to="/Enrollments" />;
    }
  }

  return (
    <div>
      { showRedirect() }
      <h1>Create Enrollment</h1>
      <form onSubmit={onSubmit}>
          <label className="mt-3" htmlFor="employeeSelect">Employee</label>
          <select id="employeeSelect" className="form-select" defaultValue="Unknown" onChange={handleChange(setSelectedEmployee)}>
            <option value="Unknown" disabled>Employee</option>
            {
              (employees || []).map(employee =>
                <option key={employee.id} value={employee.id}>{employee.id}: {employee.firstName} {employee.lastName}</option>)
            }
          </select>
          <label className="mt-3" htmlFor="benefitSelect">Benefit</label>
          <select id="benefitSelect" className="form-select" defaultValue="Unknown" onChange={handleChange(setSelectedBenefit)}>
            <option value="Unknown" disabled>Benefit</option>
            {
              (benefitTypes || []).map(benefit =>
                <option key={benefit.type} value={benefit.type}>{benefit.type}: {benefit.description}</option>)
            }
          </select>
          <label className="mt-3" htmlFor="discountSelect">Discounts</label>
          <select id="discountSelect" className="form-select" multiple onChange={getMultipleSelected(setSelectedDiscounts)}>
          {
              (discountTypes || []).map(discount =>
                <option key={discount.type} value={discount.type}>{discount.type}: {discount.description}</option>)
            }
          </select>
          <input type="submit" className="mt-3 btn btn-primary" value="Enroll" />
      </form>
    </div>
  );
}

export default CreateEnrollmentPage;