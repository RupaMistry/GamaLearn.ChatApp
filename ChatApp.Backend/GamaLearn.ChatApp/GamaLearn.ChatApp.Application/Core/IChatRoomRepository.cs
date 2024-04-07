namespace GamaLearn.ChatApp.Application.Core
{
    /// <summary>
    /// IRepository for ChatRooms entity
    /// </summary>
    public interface IChatRoomRepository<T> where T : Entity
    { 
        /// <summary>
        /// Returns list of ChatRooms
        /// </summary>
        /// <returns>List of ChatRooms</returns>
        Task<IReadOnlyList<T>> GetChatRooms(); 
    }
}