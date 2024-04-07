namespace GamaLearn.ChatApp.Infrastructure.Repository
{
    /// <summary>
    /// Repository for UserMessage entity
    /// </summary>
    /// <param name="dbContext"></param>
    public class UserMessageRepository(ChatAppDBContext dbContext) : IMessageRepository<UserMessage>
    {
        private readonly ChatAppDBContext _chatAppContext = dbContext;

        /// <summary>
        /// Returns list of user messages for given recipient
        /// </summary>
        /// <param name="recipient"></param>
        /// <returns>List of UserMessages</returns>
        public async Task<IReadOnlyList<UserMessage>> GetAll(string recipient)
        {
            try
            {
                var userMessages = await this._chatAppContext.UserMessages
                .Where(m => m.Recipient == recipient)
                .OrderBy(n => n.CreatedDate)
                .ToListAsync();

                return userMessages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns list of pending user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>List of UserMessages</returns>
        public async Task<IReadOnlyList<UserMessage>> GetPending(string sender, string recipient)
        {
            try
            {
                // Query and return all messages that are still not delivered to recipient by sender
                var userMessages = await this._chatAppContext.UserMessages
                     .Where(m => m.Recipient == recipient && m.Sender == sender && m.IsDelivered == false)
                     .OrderBy(n => n.CreatedDate)
                     .ToListAsync();

                return userMessages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts a new user message
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns>Rows affected count</returns>
        public async Task<int> Insert(UserMessage userMessage)
        {
            try
            {
                userMessage.ID = 0; 

                this._chatAppContext.UserMessages.Add(userMessage);

                int rowsAffected = await this._chatAppContext.SaveChangesAsync();

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes list of user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>Rows affected count</returns>
        public async Task<int> DeleteAll(string sender, string recipient)
        {
            try
            {
                // Query and delete all messages that are delivered to recipient by sender
                var userMessages = await this._chatAppContext.UserMessages
                .Where(m => m.Sender == sender && m.Recipient == recipient && m.IsDelivered == false)
                .ToListAsync();

                if (userMessages?.Count <= 0)
                    return -1;

                userMessages.ForEach(m => m.IsDelivered = true);

                int rowsAffected = await this._chatAppContext.SaveChangesAsync();

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}