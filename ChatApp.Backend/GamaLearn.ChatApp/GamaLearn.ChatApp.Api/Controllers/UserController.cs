namespace GamaLearn.ChatApp.Api.Controllers
{
    /// <summary>
    /// User API Controller
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="logger"></param>
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserManagerService userService, ILogger<ChatRoomController> logger) : ControllerBase
    {
        private readonly IUserManagerService _userService= userService ?? throw new ArgumentNullException(nameof(userService));
        private readonly ILogger<ChatRoomController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <summary>
        /// Authenticates user login credentials.
        /// </summary>
        /// <param name="LoginModel"></param>
        /// <returns>UserSecurityToken</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // validate and authenticate user credentials
            var securityToken = await this._userService.ValidateUserLogin(model.Username, model.Password, model.ChatRecipient,model.IsChatRoom);

            // if token is null, return authentication failure response
            if (securityToken == null)
                return Unauthorized(new { Message = "Invalid user credentials. Authentication failed!" });
             
            // If authentication is success, return user token.
            return Ok(new
            {
                token = securityToken.Token,
                expiration = securityToken.Validity
            });
        }

        /// <summary>
        /// Registers a new user in system
        /// </summary>
        /// <param name="RegisterModel"></param>
        /// <returns>User creation status enum</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // register new user in system
            var result = await this._userService.RegisterUser(model.UserName, model.Email, model.Password);

            // If validation error, return BadResponse, else return success response.
            if (result == UserCreationEnum.UserNameExists)
                return BadRequest(new { Message = $"User already exists with userName: {model.UserName}" });
            else if (result == UserCreationEnum.EmailIDExists)
                return BadRequest(new { Message = $"User already exists with emailID: {model.Email}" });
            else if (result == UserCreationEnum.InvalidUserError)
                return BadRequest(new { Message = "User creation failed! Please check user details and try again." });
            else
                return Ok(new { Message = "User created successfully!" });
        } 

        /// <summary>
        /// Returns all users in system
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers()
        {
            // Fetch and return all users in system
            var users = this._userService.GetAllUsers();

            // if list is empty, return NotFound() else success response.
            if (users == null)
            {
                _logger.LogInformation("No users found in database");
                return NotFound();
            }

            _logger.LogInformation("Users list returned successfully."); 

            return Ok(new { Data = users } );
        }
    }
}