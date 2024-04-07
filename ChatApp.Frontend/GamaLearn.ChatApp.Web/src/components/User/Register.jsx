
import { useState } from 'react'
import { Container, Row, Col, Card, Form, Button } from "react-bootstrap";
import { UserApiService } from "../../apiservices/UserApiService";
import { AlertMessages } from "../../utilHelpers/Constants";
import { Link } from 'react-router-dom'

function Register() {

  const [isDisabled, setDisabled] = useState(false);

  /** Handles the user registration event  */
  const onRegisterSubmit = async (e) => {
    setDisabled(true);
    e.preventDefault();

    const formData = new FormData(e.target);
    const registerModel = Object.fromEntries(formData.entries());

    if (registerModel.Password !== registerModel.ConfirmPassword) {
      alert(AlertMessages.PasswordMismatch);
      setDisabled(false);
      return;
    }

    console.log(registerModel);

    try {
      var apiResponse = await new UserApiService().registerUser(registerModel.UserName, registerModel.Password, registerModel.Email);

      alert(apiResponse.Message);
      if (apiResponse.IsSuccess) {
        window.location.href = '/login';
      }
      else {
        setDisabled(false);
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
              <h2 className="text-center mb-4 midnightblue">Signup to GamaLearn Chatting App!</h2>
              <Form onSubmit={onRegisterSubmit}>
                <Form.Group controlId="formUserName" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '260px' }}>User Name</Form.Label>
                  <Form.Control type="text" placeholder="Enter UserName" required name="UserName" minLength="6" />
                </Form.Group>

                <Form.Group controlId="formEmail" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '260px' }}>Email</Form.Label>
                  <Form.Control
                    type="email" placeholder="Enter email" required name="Email" />
                </Form.Group>

                <Form.Group controlId="formPassword" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '260px' }}>Password</Form.Label>
                  <Form.Control
                    type="password" placeholder="Enter Password" required name="Password" minLength="6" />
                </Form.Group>

                <Form.Group controlId="formConfirmPassword" className="mb-3 d-flex flex-row align-items-center">
                  <Form.Label className="text-start mb-0 midnightblue" style={{ width: '260px' }}>Confirm Password</Form.Label>
                  <Form.Control
                    type="password" placeholder="Enter Confirm Password" required name="ConfirmPassword" minLength="6" />
                </Form.Group>

                <Button variant="primary" type="submit" className='midnightblue' disabled={isDisabled} >
                  Register
                </Button>
              </Form>
              <p>Already have an account? Login <Link to="/login">here</Link></p>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default Register;