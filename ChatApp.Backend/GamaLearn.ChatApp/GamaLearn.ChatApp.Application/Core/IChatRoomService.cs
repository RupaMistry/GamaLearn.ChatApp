namespace GamaLearn.ChatApp.Application.Core
{
    /// <summary>
    /// IService for ChatRooms entity
    /// </summary>
    public interface IChatRoomService<T> where T : Entity
    {
        /// <summary>
        /// Returns list of ChatRooms
        /// </summary>
        /// <returns>List of ChatRooms</returns>
        Task<IReadOnlyList<T>> GetChatRooms(); 
    }
}