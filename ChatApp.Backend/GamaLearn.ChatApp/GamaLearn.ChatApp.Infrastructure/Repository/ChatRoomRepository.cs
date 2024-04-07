namespace GamaLearn.ChatApp.Infrastructure.Repository
{
    /// <summary>
    /// Repository for ChatRooms entity
    /// </summary>
    /// <param name="dbContext"></param>
    public class ChatRoomRepository(ChatAppDBContext dbContext) : IChatRoomRepository<ChatRoom>
    {
        private readonly ChatAppDBContext _chatAppContext = dbContext;

        /// <summary>
        /// Returns list of ChatRooms
        /// </summary>
        /// <returns>List of ChatRooms</returns>
        public async Task<IReadOnlyList<ChatRoom>> GetChatRooms()
        { 
            var chatRooms = await this._chatAppContext.ChatRooms.ToListAsync();

            return chatRooms;
        }
    }
}