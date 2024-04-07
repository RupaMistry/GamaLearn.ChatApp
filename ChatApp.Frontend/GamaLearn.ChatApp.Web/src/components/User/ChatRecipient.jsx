
import { useEffect, useState } from 'react'
import { Dropdown } from "react-bootstrap";
import { UserApiService } from "../../apiservices/UserApiService";
import { ChatRoomApiService } from '../../apiservices/ChatRoomApiService';
import { DisplayLabels } from '../../utilHelpers/Constants';

function ChatRecipient() {

    const [userList, setUserList] = useState([]);
    const [chatRoomsList, setChatRoomsList] = useState([]);

    useEffect(() => {

        /** Fetch list of all users in system */
        const fetchUsers = async () => {
            try {
                var apiResponse = await new UserApiService().getUsers();
                if (apiResponse.IsSuccess && apiResponse.Data != null) {
                    setUserList(apiResponse.Data);
                }
            } catch (error) {
                console.error('Error fetching data: ', error);
            }
        };

        /** Fetch list of all chat rooms in system.  */
        const fetchChatRooms = async () => {
            try {
                var apiResponse = await new ChatRoomApiService().getChatRooms();
                if (apiResponse.IsSuccess && apiResponse.Data != null) {
                    setChatRoomsList(apiResponse.Data);
                } else {
                    alert(apiResponse.Message);
                }
            } catch (error) {
                console.error('Error fetching data: ', error);
            }
        };
        fetchUsers();
        fetchChatRooms();
    }, []);

    return (
        <>
            <Dropdown.Header>Users</Dropdown.Header>
            {userList?.length > 0 && userList.map((user, index) => (
                <Dropdown.Item className='align-items-center'
                    eventKey={user.userName} key={index}>{user.userName}</Dropdown.Item>
            ))}

            <Dropdown.Divider></Dropdown.Divider>

            <Dropdown.Header>Chat Rooms</Dropdown.Header>
            {chatRoomsList?.length > 0 && chatRoomsList.map((room, index) => (
                <Dropdown.Item
                    eventKey={DisplayLabels.ChatRoom + room.name} key={index}>{room.name}</Dropdown.Item>
            ))}
        </>
    );
}

export default ChatRecipient;