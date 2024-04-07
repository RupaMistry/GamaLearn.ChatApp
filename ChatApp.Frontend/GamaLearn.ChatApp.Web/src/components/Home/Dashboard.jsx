import { Container, Row, Col } from 'react-bootstrap';
import Header from './Header';
import ChatBox from '../ChatRoom/ChatBox'

const Dashboard = () => {
    return (
        <Container className="py-5">
            <Row className="justify-content-center">
                <Col md={8} >
                    <Header />
                </Col>
            </Row>
            <Row className="d-flex justify-content-center">
                <Col md="8" lg="8" xl="8">
                    <ChatBox />
                </Col>
            </Row>
        </Container>
    );
};

export default Dashboard;