namespace GamaLearn.ChatApp.Application.Core
{
    /// <summary>
    /// IStore for UserMessages entity
    /// </summary>
    public interface IMessageStore<T>
    {
        /// <summary>
        /// Returns list of pending user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>List of UserMessages</returns>
        Task<IReadOnlyList<T>> GetPendingMessagesForUser(string sender, string recipient);

        /// <summary>
        /// Inserts a new user message
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns>Rows affected count</returns>
        Task<int> StoreMessageForUser(T message);

        /// <summary>
        /// Deletes list of user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>Rows affected count</returns>
        Task<int> ClearMessagesForUser(string sender, string recipient);
    }
}