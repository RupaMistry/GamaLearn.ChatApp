/** Constants for API URL's */
export const ApiUrls = {
  userApiURL: "https://localhost:7217/api/user",
  chatRoomApiURL: "https://localhost:7217/api/chatroom",
  signalRHubURL: "https://localhost:7143/hub",
  loginURLPath: "/login",
  registerURLPath: "/register",
  listURLPath: "/list"
};

/** Constants for system alert messages */
export const AlertMessages = {
  PasswordMismatch: "Password and ConfirmPassword do not match.",
  SystemError: "Some error occured while processing your request. Please try again later",
  APIOnline: "Some error occured while processing your request. Please ensure API services are online.",
  SignalRConnected: "SignalR server is connected!",
  ChatRecipient: "Please select a User or ChatRoom",
  SameUserError: "Please select a different user",
};

/** Constants for UI labels and texts. */
export const DisplayLabels = {
  ChatRecipient: "Select User or ChatRoom",
  ChatRoom: "ChatRoom - "
}