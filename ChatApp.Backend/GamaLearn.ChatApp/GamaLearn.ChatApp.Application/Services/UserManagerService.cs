namespace GamaLearn.ChatApp.Application.Services
{
    /// <summary>
    /// Service for User management
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="tokenService"></param>
    public class UserManagerService(UserManager<ApplicationUser> userManager, ITokenService tokenService) : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;
        private readonly ITokenService tokenService = tokenService;

        /// <summary>
        /// Authenticates user login credentials.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="chatRecipient"></param>
        /// <param name="isChatRoom"></param>
        /// <returns>UserSecurityToken</returns>
        public async Task<UserSecurityToken> ValidateUserLogin(string userName, string password, 
            string chatRecipient, bool isChatRoom)
        {
            UserSecurityToken securityToken = null;

            // Check if user exists by userName
            var user = await userManager.FindByNameAsync(userName);

            // If exists, validate the user password 
            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                // If authenticated, create claims for user and jwtToken
                var chatRec = new ChatRecipient() { Name=chatRecipient, IsChatRoom = isChatRoom };

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                    new Claim(ClaimTypes.UserData, chatRec.ToString()),
                };

                // Get JwtSecurityToken for the authenticated user
                securityToken = this.tokenService.GetJwtSecurityToken(authClaims);
            }

            return securityToken;
        }

        /// <summary>
        /// Registers a new user in system
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>User creation status enum</returns>
        public async Task<UserCreationEnum> RegisterUser(string userName, string email, string password)
        {
            // Check if user exists by userName
            var userExists = await this.userManager.FindByNameAsync(userName);

            // If exists, return duplicate userName error
            if (userExists != null)
                return UserCreationEnum.UserNameExists;

            // Check if user exists by emailID
            var emailExists = await this.userManager.FindByEmailAsync(email);

            // If exists, return duplicate emailID error
            if (emailExists != null)
                return UserCreationEnum.EmailIDExists;

            var user = new ApplicationUser()
            {   
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userName
            };

            // Create new application user
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return UserCreationEnum.InvalidUserError;
        
            return UserCreationEnum.UserCreated; 
        }

        /// <summary>
        /// Returns all users in system
        /// </summary>
        /// <returns>List of users</returns>
        public List<UserDetails> GetAllUsers()
        {
            // Get all users having valid userName
            var users =  this.userManager.Users.Where(u => !string.IsNullOrEmpty(u.UserName));

            if (users?.Count() <= 0)
                return null;

            // Simplify the returned list by only sending required data.
            var usersList = new List<UserDetails>();
            foreach (var user in users)
            {
                usersList.Add(new() { UserID = user.Id, UserName = user.UserName});
            }

            return usersList;
        }
    }
}