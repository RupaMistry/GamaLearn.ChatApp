import Login from './components/User/Login'
import Register from "./components/User/Register";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import { AuthProvider } from './components/Authentication/AuthContext'
import { PrivateRoutes } from './components/Authentication/PrivateRoutes'
import "./App.css";
import "./index.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Dashboard from './components/Home/Dashboard';

function App() {
  return (
    <div className="App">
      <Router>
        <AuthProvider>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route element={<PrivateRoutes />}>
              <Route path="/dashboard" element={<Dashboard />} />
            </Route>
          </Routes>
        </AuthProvider>
      </Router>
    </div>
  );
}
export default App;