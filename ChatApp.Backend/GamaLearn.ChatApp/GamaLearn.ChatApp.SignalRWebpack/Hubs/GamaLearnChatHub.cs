namespace GamaLearn.ChatApp.SignalRWebpack.Hubs
{
    /// <summary>
    /// Represents GamaLearn SignalR Hub.
    /// </summary>
    /// <param name="messageStore"></param>
    /// <param name="userStore"></param>
    /// <param name="logger"></param>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamaLearnChatHub(IMessageStore<UserMessage> messageStore, IOnlineUserStore userStore, ILogger<GamaLearnChatHub> logger) : Hub
    {
        private readonly IMessageStore<UserMessage> _messageStore = messageStore;
        private readonly IOnlineUserStore _userStore = userStore;
        private readonly ILogger<GamaLearnChatHub> _logger = logger;

        /// <summary>
        /// Called when a new connection is established with the GamaLearn hub.
        /// </summary> 
        public override async Task OnConnectedAsync()
        {
            try
            {
                // Upon new connection, add the user to the OnlineUserStore.
                _userStore.AddUser(Context);

                _logger.LogInformation($"Client {Context.UserIdentifier} connected.");

                // Current user is the "Recipient\Reciever" for all the messages sent by "Sender" user(selected on Login UI)
                var currentUser = Context.User.Identity.Name;

                // Get the recipient user details
                var chatRecipient = this.GetRecipientFromClaims();

                // If the current user is not connected to a chatRoom, then get all the non-delivered messages. 
                if (chatRecipient != null && !chatRecipient.IsChatRoom)
                {
                    // Fetch all the pending messages
                    var offlineMessages = await _messageStore.GetPendingMessagesForUser(chatRecipient.Name, currentUser);

                    // If messages exist, send the list to the caller.
                    if (offlineMessages?.Count > 0)
                    {
                        await Clients.Caller.SendAsync("pendingMessagesReceived", offlineMessages);
                        _logger.LogInformation($"Pending messages from {chatRecipient.Name} sent to {currentUser}");

                        // Clear the messages once received by current user.
                        await _messageStore.ClearMessagesForUser(chatRecipient.Name, currentUser);
                        _logger.LogInformation($"Pending messages cleared for {currentUser}.");
                    }

                }
                await base.OnConnectedAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        /// <summary>
        /// Method to handle and send a new user chat message.
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="message"></param> 
        public async Task NewMessage(string recipient, string message)
        {
            try
            {
                var sender = Context.User.Identity.Name;
                var currentDate = DateTime.Now;
                string msgDate = currentDate.ToString("dd MMM hh:mm tt");

                // If user is online, send the message to the recipient.
                if (_userStore.IsUserOnline(recipient, out Guid recipientID))
                {
                    await Clients.User(Convert.ToString(recipientID))
                        .SendAsync("messageReceived", sender, msgDate, message);

                    _logger.LogInformation($"Message from {sender} sent to {recipient} at {currentDate}.");
                }
                // else if offline, then store the sender user message in database.
                else
                {
                    await _messageStore.StoreMessageForUser(new UserMessage
                    {
                        Sender = sender,
                        CreatedDate = currentDate,
                        Message = message,
                        Recipient = recipient
                    });

                    _logger.LogInformation($"Message from {sender} to {recipient} stored in database.");
                }

                // Send the message to the caller/sender user.
                await Clients.Caller.SendAsync("messageReceived", sender, msgDate, message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }  

        /// <summary>
        /// Method to add new user to the ChatRoom group.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task JoinGroup(string groupName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                _logger.LogInformation($"Client {Context.ConnectionId} joined {groupName}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        /// <summary>
        /// Method to handle and send a new user chat message to the ChatRoom group.
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            try
            {
                string msgDate = DateTime.Now.ToString("dd MMM hh:mm tt");

                await Clients.Group(groupName).SendAsync("messageReceived", user, msgDate, message);
                
                _logger.LogInformation($"Message from {user} sent to {groupName} at {DateTime.Now}.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        /// <summary>
        /// Called when a connection is ended with the GamaLearn hub.
        /// </summary>
        /// <param name="exception"></param> 
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var userId = Guid.Parse(Context.UserIdentifier);

                // Get the recipient user details
                var chatRecipient = this.GetRecipientFromClaims();

                // If current user is connected to a chatRoom, then remove it from Group.
                if (chatRecipient != null && chatRecipient.IsChatRoom)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRecipient.Name);

                    _logger.LogInformation($"Client {Context.ConnectionId} removed from {chatRecipient.Name}");
                }

                // Remove current user from OnlineUserStore
                _userStore.RemoveUser(userId);

                _logger.LogInformation($"Client {Context.UserIdentifier} disconnected.");

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        /// <summary>
        /// Gets the Chat recipient user details
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns>ChatRecipient</returns>
        private ChatRecipient GetRecipientFromClaims()
        {
            // Get claims from the current httpContext.
            var userDataClaim = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);

            if (userDataClaim == null)
                return null;

            var chatRecipient = JsonSerializer.Deserialize<ChatRecipient>(userDataClaim.Value);

            return chatRecipient;

        }
    }
}