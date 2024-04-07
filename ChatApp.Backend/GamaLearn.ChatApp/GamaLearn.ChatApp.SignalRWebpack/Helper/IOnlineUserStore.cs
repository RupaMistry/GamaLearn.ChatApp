namespace GamaLearn.ChatApp.SignalRWebpack.Helper
{
    /// <summary>
    /// IStore for all online available users.
    /// </summary>
    public interface IOnlineUserStore
    {
        /// <summary>
        /// Checks if user status is online or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userID"></param> 
        bool IsUserOnline(string userName, out Guid userID);

        /// <summary>
        /// Adds user to store collection.
        /// </summary>
        /// <param name="context"></param>
        void AddUser(HubCallerContext context);

        /// <summary>
        /// Removes user from store collection.
        /// </summary>
        /// <param name="userId"></param>
        void RemoveUser(Guid userId);
    }
}