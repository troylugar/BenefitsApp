import { Switch, Route, useRouteMatch } from "react-router";
import { Link } from "react-router-dom";
import EnrollmentsTable from "../components/EnrollmentsTable";
import Enrollment from "../components/Enrollment";
import CreateEnrollmentPage from "./CreateEnrollmentPage";

function EnrollmentsPage() {
  const match = useRouteMatch();
  
  return (
    <div>
      <Switch>
        <Route path={`${match.path}/create`}>
          <CreateEnrollmentPage />
        </Route>
        <Route path={`${match.path}/:enrollmentId`}>
          <Enrollment />
        </Route>
        <Route path={match.path}>
          <h1 className="my-3">Enrollments Page</h1>
          <EnrollmentsTable />
          <Link to={`${match.path}/create`} className="mt-3 btn btn-primary">
            New Enrollment
          </Link>
        </Route>
      </Switch>
    </div>
  );
}

export default EnrollmentsPage;