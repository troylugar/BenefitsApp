import './App.css';
import EmployeesPage from './pages/EmployeesPage';
import EnrollmentsPage from './pages/EnrollmentsPage';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect
} from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <div className="container">
            <Link to="/" className="navbar-brand">BenefitsApp</Link>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav">
                <li className="nav-item">
                  <Link to="/employees" className="nav-link">Employees</Link>
                </li>
                <li className="nav-item">
                  <Link to="/enrollments" className="nav-link">Enrollments</Link>
                </li>
              </ul>
            </div>
          </div>
        </nav>
        <div className="container">
          <Switch>
            <Route path="/employees">
              <EmployeesPage />
            </Route>
            <Route path="/enrollments">
              <EnrollmentsPage />
            </Route>
            <Route path="/enrollments">
              <EnrollmentsPage />
            </Route>
            <Route path="/">
              <Redirect to="/employees" />
            </Route>
          </Switch>
        </div>
      </div>
    </Router>
  );
}

export default App;
