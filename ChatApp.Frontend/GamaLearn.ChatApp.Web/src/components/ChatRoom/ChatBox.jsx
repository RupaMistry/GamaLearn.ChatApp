import { useEffect, useState } from 'react';
import { useAuth } from '../Authentication/AuthContext';
import Connector from '../../utilHelpers/SignalRConnector';
import MessageList from './MessageList';
import MessageBox from './MessageBox';
import { Card, Button } from 'react-bootstrap';

const ChatBox = () => {
    const [messageList, setMessageList] = useState([]);
    const { authToken, userDetails } = useAuth();

    const { newMessage, messageReceivedEvent, pendingMessagesReceivedEvent, newMessageToGroup } = Connector(authToken, userDetails);

    useEffect(() => {

        // Update the messageList whenever a new message is received.
        messageReceivedEvent((sender, messageDate, message) => {
            setMessageList([...messageList, { sender: sender, messageDate: messageDate, message: message }]);
        });

        // Sets pending messages list to messageList
        pendingMessagesReceivedEvent((pendingMessages) => {
            setMessageList(pendingMessages);
        });
    });

    /**  Sends a new message to given recipient */
    const sendMessage = async (userMessage) => {
        newMessage(userMessage);
    }

    /**  Sends a new message to entire chatroom. */
    const sendMessageToGroup = async (userMessage) => {
        newMessageToGroup(userMessage);
    }

    return (
        <>
            <Card id="chat1" style={{ borderRadius: "15px" }}>
                <Card.Header
                    className="d-flex justify-content-between align-items-center p-3 bgmidnightblue text-white border-bottom-0"
                    style={{ borderTopLeftRadius: "15px", borderTopRightRadius: "15px", }}>
                    <Button variant="light">
                        <i className="fas fa-angle-left"></i>
                    </Button>
                    <h2 className="mb-0 fw-bold">Live chat</h2>
                    <Button variant="light">
                        <i className="fas fa-times"></i>
                    </Button>
                </Card.Header>
                <Card.Body>
                    <MessageList messageList={messageList} />
                    <br />
                    <MessageBox userDetails={userDetails} sendMessage={sendMessage} sendMessageToGroup={sendMessageToGroup} />
                </Card.Body>
            </Card>
        </>
    );
};

export default ChatBox;