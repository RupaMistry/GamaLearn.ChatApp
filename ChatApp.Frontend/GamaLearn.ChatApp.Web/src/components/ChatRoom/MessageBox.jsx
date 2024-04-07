import { Form, Button } from 'react-bootstrap';
import { useState } from 'react';
import { AlertMessages } from '../../utilHelpers/Constants';

const MessageBox = ({ userDetails, sendMessage, sendMessageToGroup }) => {

    const [message, setMessage] = useState("");

    const onSentSubmit = async (e) => {
        e.preventDefault();

        try {
            const userMessage = {
                Sender: userDetails.UserName,
                Message: message
            };
            // If user is connected to a chat room, send message to group
            if (userDetails.IsChatRoom) {
                userMessage.ChatRoomName = userDetails.ChatRecipient;
                sendMessageToGroup(userMessage);
            }
            // else to the selected user recipient
            else {
                userMessage.Recipient = userDetails.ChatRecipient;
                sendMessage(userMessage);
            }

            setMessage('');
        }
        catch (error) {
            console.error(error);
            alert(AlertMessages.SystemError);
        }
    }
    return (
        <>
            <Form onSubmit={onSentSubmit}>
                <Form.Group controlId="messageInput">
                    <Form.Control
                        type="textarea"
                        required name="Message" maxLength="100"
                        value={message} rows={4} onChange={(e) => setMessage(e.target.value)}
                        placeholder={"Type and send your message to " + userDetails.ChatRecipient} />
                </Form.Group>
                <br />
                <Button variant="primary" type="submit" className='midnightblue'>Send</Button>
            </Form>
        </>
    );
};

export default MessageBox;