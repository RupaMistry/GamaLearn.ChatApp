import { createContext, useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router'
export const useAuth = () => useContext(AuthContext);

const AuthContext = createContext()

export default AuthContext;

export const AuthProvider = ({ children }) => {
    const [authToken, setAuthToken] = useState(localStorage.getItem('authToken'));
    const [userDetails, setUserDetails] = useState(JSON.parse(localStorage.getItem('userDetails')));

    const navigate = useNavigate();

    useEffect(() => {
        validateUserSession();
    }, []);

    /** Authenticates user login credentials.  */
    const loginUser = (authToken, userDetails) => {
        // Set JWT authToken
        localStorage.setItem('authToken', authToken);
        setAuthToken(authToken);

        // Set session user details
        localStorage.setItem('userDetails', JSON.stringify(userDetails));
        setUserDetails(userDetails);

        // Redirect to dashboard page.
        navigate('/dashboard');
    };

    /**  Validates if current user still has a valid session or not. */
    const validateUserSession = () => {
        // Check if authToken and userDetails is valid
        if (authToken && userDetails) {
            return true;
        }
        // if not, then ask user to login
        else {
            navigate('/login');
            return false;
        }
    };

    /**  Logouts user from current session */
    const logoutUser = () => {
        // Remove all data from localStorage
        localStorage.removeItem('authToken');
        localStorage.removeItem('userDetails');

        // Set authToken and userDetails to null
        setAuthToken(null);
        setUserDetails(null);

        // Redirect to login page.
        navigate('/login');

        // Refresh to disconnect user from SignalR server.
        window.location.reload();
    };

    return (
        <AuthContext.Provider value={{ authToken, userDetails, loginUser, logoutUser, validateUserSession }}>
            {children}
        </AuthContext.Provider>
    )
}
