namespace GamaLearn.ChatApp.Application.Services
{
    /// <summary>
    /// Service for UserMessages entity
    /// </summary>
    /// <param name="repository"></param>
    public class MessageStoreService(IMessageRepository<UserMessage> repository) : IMessageStore<UserMessage>
    {
        private readonly IMessageRepository<UserMessage> messageRepository = repository;

        /// <summary>
        /// Returns list of pending user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>List of UserMessages</returns>
        public async Task<IReadOnlyList<UserMessage>> GetPendingMessagesForUser(string sender, string recipient)
        {
            return await messageRepository.GetPending(sender, recipient);
        }

        /// <summary>
        /// Inserts a new user message
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns>Rows affected count</returns>
        public async Task<int> StoreMessageForUser(UserMessage userMessage)
        {
            return await this.messageRepository.Insert(userMessage);
        }

        /// <summary>
        /// Deletes list of user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>Rows affected count</returns>
        public async Task<int> ClearMessagesForUser(string sender, string recipient)
        {
            return await messageRepository.DeleteAll(sender, recipient);
        }
    } 
}