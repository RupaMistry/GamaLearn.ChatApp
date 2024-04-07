import { useAuth } from '../Authentication/AuthContext';

import { Button } from 'react-bootstrap';

const Header = () => {
    const { userDetails, logoutUser } = useAuth();

    return (
        <div>
            <h2 className="text-center white">
                Welcome, {userDetails.UserName} to GamaLearn Chatting App!
            </h2>
            <h4 className="text-center mb-4 white">
                You are now connected to {userDetails.ChatRecipient}.
            </h4>
            <Button variant="danger" className="position-absolute top-0 end-0" onClick={logoutUser}>
                Logout
            </Button>
        </div>
    )
}

export default Header;