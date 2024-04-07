namespace GamaLearn.ChatApp.Application.Core
{
    /// <summary>
    /// IRepository for UserMessage entity
    /// </summary>
    public interface IMessageRepository<T> where T : Entity
    { 
        /// <summary>
        /// Returns list of user messages for given recipient
        /// </summary>
        /// <param name="recipient"></param>
        /// <returns>List of UserMessages</returns>
        Task<IReadOnlyList<T>> GetAll(string recipient);

        /// <summary>
        /// Returns list of pending user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>List of UserMessages</returns>
        Task<IReadOnlyList<T>> GetPending(string sender, string recipient);

        /// <summary>
        /// Inserts a new user message
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns>Rows affected count</returns>
        Task<int> Insert(T userMessage);

        /// <summary>
        /// Deletes list of user messages for recipient sent by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns>Rows affected count</returns>
        Task<int> DeleteAll(string sender, string recipient);
    }
}
