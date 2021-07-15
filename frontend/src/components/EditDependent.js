import { useEffect, useState } from "react";
import { Redirect, useParams } from "react-router-dom";
import api from "../api/api";
import { handleChange } from "../helper";

function EditDependent(props) {
  const { employeeId, dependentId } = useParams();
  const [dependent, setDependent] = useState(false);

  const [redirect, setRedirect] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');

  useEffect(() => {
    api.getDependentById(employeeId, dependentId).then(data => {
      setDependent(data);
      setFirstName(data.firstName);
      setLastName(data.lastName);
    });
  }, [employeeId, dependentId]);

  const onSubmit = (event) => {
    api.modifyDependent(employeeId, dependentId, {
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

  if (!dependent) {
    return <div></div>;
  }

  return (
    <form onSubmit={ onSubmit } >
      { showRedirect() }
      <h1 className="my-3">Edit Dependent</h1>
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
      <input type="submit" className="my-3 btn btn-primary" value="Save Dependent" />
    </form >
  );
}

export default EditDependent;