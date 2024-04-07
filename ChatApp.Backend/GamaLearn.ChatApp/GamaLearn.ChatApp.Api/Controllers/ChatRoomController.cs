namespace GamaLearn.ChatApp.Api.Controllers
{
    /// <summary>
    /// ChatRoom API Controller
    /// </summary>
    /// <param name="service"></param>
    /// <param name="logger"></param>
    [Route("api/chatroom")]
    [ApiController]
    public class ChatRoomController(IChatRoomService<ChatRoom> service, ILogger<ChatRoomController> logger) : ControllerBase
    {
        private readonly IChatRoomService<ChatRoom> _chatService = service ?? throw new ArgumentNullException(nameof(service));
        private readonly ILogger<ChatRoomController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(IReadOnlyList<ChatRoom>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        /// <summary>
        /// Gets a list of available chat rooms
        /// </summary>
        public async Task<ActionResult<IReadOnlyList<ChatRoom>>> GetChatRooms()
        {
            // Fetch and return all chatRooms in system
            var chatRooms = await _chatService.GetChatRooms();

            // if list is empty, return NotFound() else success response.
            if (chatRooms == null)
            {
                _logger.LogInformation("No ChatRooms found in database");
                return NotFound();
            }

            _logger.LogInformation("ChatRooms list returned successfully.");
            return Ok(new { Data = chatRooms });
        }
    }
}