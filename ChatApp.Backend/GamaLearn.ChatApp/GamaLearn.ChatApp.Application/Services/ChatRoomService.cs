namespace GamaLearn.ChatApp.Application.Services
{
    /// <summary>
    /// Service for ChatRooms entity
    /// </summary>
    /// <param name="repository"></param>
    public class ChatRoomService(IChatRoomRepository<ChatRoom> repository) : IChatRoomService<ChatRoom>
    {
        private readonly IChatRoomRepository<ChatRoom> chatRepository = repository;

        /// <summary>
        /// Returns list of ChatRooms
        /// </summary>
        /// <returns>List of ChatRooms</returns>
        public async Task<IReadOnlyList<ChatRoom>> GetChatRooms()
        {
            return await chatRepository.GetChatRooms();
        }
    }
}