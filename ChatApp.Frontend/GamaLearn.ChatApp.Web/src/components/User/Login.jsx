
import { useState } from 'react'
import { Container, Row, Col, Card, Form, Button, DropdownButton } from "react-bootstrap";
import { UserApiService } from "../../apiservices/UserApiService";
import { AlertMessages, DisplayLabels } from "../../utilHelpers/Constants";
import { Link } from 'react-router-dom'
import { useAuth } from '../Authentication/AuthContext';
import ChatRecipient from './ChatRecipient';

function Login() {

  const { loginUser } = useAuth();
  const [isDisabled, setDisabled] = useState(false);
  const [isChatRoom, setIsChatRoom] = useState(false);
  const [chatRecipient, setChatRecipient] = useState(DisplayLabels.ChatRecipient);

  /** Handles the dropdown selection for chat recipient */
  const handleSelect = (selectedKey) => {
    setIsChatRoom(selectedKey.startsWith(DisplayLabels.ChatRoom));
    setChatRecipient(selectedKey);
  };

  /** Handles the user login submit event.  */
  const onLoginSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData(e.target);
    const loginModel = Object.fromEntries(formData.entries());

    // Validate that ChatRecipient is selected
    if (chatRecipient === DisplayLabels.ChatRecipient) {
      alert(AlertMessages.ChatRecipient);
      return;
    }
    // Validate that ChatRecipient selection is not their own username
    else if (chatRecipient === loginModel.UserName) {
      alert(AlertMessages.SameUserError);
      return;
    }

    setDisabled(true);

    try {

      let loginDetails = {
        UserName: loginModel.UserName,
        Password: loginModel.Password,
        ChatRecipient: chatRecipient,
        IsChatRoom: isChatRoom
      };

      var apiResponse = await new UserApiService().loginUser(loginDetails);

      if (apiResponse.IsSuccess && apiResponse.Message !== '') {
        var authToken = apiResponse.Message;
        loginDetails.Password = null;
        // Once logged in, set authToken
        loginUser(authToken, loginDetails);
      }
      else {
        setDisabled(false);
        alert(apiResponse.Message);
      }
    } catch (error) {
      console.error(error);
      alert(AlertMessages.SystemError);
    }
  }

  return (
    <Container className="mt-5">
      <Row className="justify-content-center">
        <Col md={6}>
          <Card>
            <Card.Body>
              <h2 className="text-center mb-4 midnightblue">Login to GamaLearn Chatting App!</h2>
              <Form onSubmit={onLoginSubmit}>
                <Form.Group controlId="formUserName" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '260px' }}>User Name</Form.Label>
                  <Form.Control type="text" placeholder="Enter User Name" required name="UserName" minLength="6" />
                </Form.Group>

                <Form.Group controlId="formPassword" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '260px' }}>Password</Form.Label>
                  <Form.Control
                    type="password" placeholder="Enter Password" required name="Password" minLength="6" />
                </Form.Group>

                <Form.Group controlId="formChatRoom" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '181px' }}> Users OR ChatRooms</Form.Label>
                  <DropdownButton className='align-items-center' variant="primary" id="dropdown-basic-button"
                    name="ChatRecipient" title={chatRecipient} onSelect={handleSelect}>
                    <ChatRecipient />
                  </DropdownButton>
                </Form.Group>

                <Button variant="primary" type="submit" className='midnightblue' disabled={isDisabled} >
                  Login
                </Button>
              </Form>
              <p>Dont have an account? Register <Link to="/register">here</Link></p>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default Login;