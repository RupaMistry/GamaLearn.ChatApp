import * as signalR from "@microsoft/signalr";
import UserMessage from "../models/UserMessage";
import { ApiUrls } from "./Constants";
import LoginUser from "../models/LoginUser";

class Connector {

    private connection: signalR.HubConnection;

    /**  Registers an event for receiving a new message between users. */
    public messageReceivedEvent: (onMessageReceived: (sender: string, messageDate: Date, message: string) => void) => void;

    /**  Registers an event for receiving all pending messages to current user. */
    public pendingMessagesReceivedEvent: (onPendingMessagesReceived: (pendingMessages: UserMessage[]) => void) => void;

    static instance: Connector;

    constructor(authToken: string, userDetails: LoginUser) {
        // Setup Hub connection to send accessToken in every request for authentication
        const options: signalR.IHttpConnectionOptions = {
            accessTokenFactory: () => {
                return authToken;
            }
        };

        // Setup SignalR connection
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(ApiUrls.signalRHubURL, options)
            .withAutomaticReconnect()
            .build();

        // Start the SignalR connection
        this.connection.start()
            .then(x => {
                // If user has selected ChatRoom, then add him to the signalR group.
                if (userDetails.IsChatRoom) {
                    this.joinGroup(userDetails.ChatRecipient);
                }
                console.log("started");
            })
            .catch(err => {
                document.write(err + " Please login to setup SignalR connection.");
            });

        this.messageReceivedEvent = (onMessageReceived) => {
            this.connection.on("messageReceived", (sender, messageDate, message) => {
                onMessageReceived(sender, messageDate, message);
            });
        };

        this.pendingMessagesReceivedEvent = (onPendingMessagesReceived) => {
            this.connection.on("pendingMessagesReceived", (pendingMessages: UserMessage[]) => {
                onPendingMessagesReceived(pendingMessages);
            });
        };
    }

    /**  Sends a new message to given recipient */
    public newMessage = (userMessage: UserMessage) => {
        this.connection
            .send("newMessage", userMessage.Recipient, userMessage.Message)
            .then(x => console.log("sent"));
    }

    /**  Adds current user to a Chat room group */
    private joinGroup = (groupName: string) => {
        this.connection
            .send("joinGroup", groupName)
            .then(x => console.log("joined"));
    }

    /**  Sends a new message to entire chatroom. */
    public newMessageToGroup = (userMessage: UserMessage) => {
        this.connection
            .send("sendMessageToGroup", userMessage.ChatRoomName, userMessage.Sender, userMessage.Message)
            .then(x => console.log("sent to group"));
    }

    /**  Provides GamaLearn signalR instance */
    public static getInstance(authToken: string, userDetails: LoginUser): Connector {

        if (!Connector.instance)
            Connector.instance = new Connector(authToken, userDetails);
        return Connector.instance;
    }
}

export default Connector.getInstance;