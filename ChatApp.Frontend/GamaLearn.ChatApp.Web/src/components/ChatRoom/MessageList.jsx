import { useAuth } from '../Authentication/AuthContext';

const MessageList = ({ messageList }) => {

    const { userDetails } = useAuth();

    return (
        <>
            <div style={{ overflowY: "scroll", height: "400px" }}>
                {messageList?.length > 0 && messageList.map((msg, index) => (
                    <div key={index}>
                        <div className={`${msg.sender === userDetails.UserName ? 'd-flex flex-row  justify-content-end' :
                            'd-flex flex-row justify-content-start'}`}>
                            <h6 style={{ height: "100%" }}>{msg.sender} at<small> {msg.messageDate}</small></h6>
                        </div>
                        <div className={`${msg.sender === userDetails.UserName ? 'd-flex flex-row  justify-content-end' :
                            'd-flex flex-row justify-content-start'}`}>
                            <div className="p-2 ms-2"
                                style={{
                                    borderRadius: "15px", backgroundColor: `${msg.sender === userDetails.UserName ?
                                        'cornflowerblue' : 'lightgreen'}`,
                                }}>
                                <p className="small mb-0">
                                    {msg.message}
                                </p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </>
    );
};

export default MessageList;