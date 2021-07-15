import { Switch, Route, useRouteMatch } from "react-router";
import Employee from "../components/Employee";
import EmployeesTable from "../components/EmployeesTable";
import CreateEmployee from "../components/CreateEmployee";
import EditEmployee from "../components/EditEmployee";
import EditDependent from "../components/EditDependent";
import CreateDependent from "../components/CreateDependent";

function EmployeesPage() {
  const match = useRouteMatch();

  return (
    <div>
      <Switch>
        <Route path={`${match.path}/create`}>
          <CreateEmployee />
        </Route>
        <Route path={`${match.path}/:employeeId/edit`}>
          <EditEmployee />
        </Route>
        <Route path={`${match.path}/:employeeId/dependents/create`}>
          <CreateDependent />
        </Route>
        <Route path={`${match.path}/:employeeId/dependents/:dependentId/edit`}>
          <EditDependent />
        </Route>
        <Route path={`${match.path}/:employeeId`}>
          <Employee />
        </Route>
        <Route path={match.path}>
          <EmployeesTable />
        </Route>
      </Switch>
    </div>
  );
}

export default EmployeesPage;