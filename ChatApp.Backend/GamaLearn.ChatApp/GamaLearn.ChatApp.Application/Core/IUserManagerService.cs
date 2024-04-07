namespace GamaLearn.ChatApp.Application.Core
{
    /// <summary>
    /// IService for User management
    /// </summary>
    public interface IUserManagerService
    { 
        /// <summary>
        /// Authenticates user login credentials.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="chatRecipient"></param>
        /// <param name="isChatRoom"></param>
        /// <returns>UserSecurityToken</returns>
        Task<UserSecurityToken> ValidateUserLogin(string userName, string password, string chatRecipient, bool isChatRoom);

        /// <summary>
        /// Registers a new user in system
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>User creation status enum</returns>
        Task<UserCreationEnum> RegisterUser(string userName, string email, string password);

        /// <summary>
        /// Returns all users in system
        /// </summary>
        /// <returns>List of users</returns>
        List<UserDetails> GetAllUsers();
    }
}