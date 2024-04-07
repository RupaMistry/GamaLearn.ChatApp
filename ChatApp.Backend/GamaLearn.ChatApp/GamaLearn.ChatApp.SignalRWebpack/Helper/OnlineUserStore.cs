namespace GamaLearn.ChatApp.SignalRWebpack.Helper
{
    /// <summary>
    /// Store for all online available users.
    /// </summary>
    public class OnlineUserStore : IOnlineUserStore
    {
        //define a dictionary to store the <userid,username>
        private static readonly Dictionary<Guid, string> _onlineUsers = [];

        /// <summary>
        /// Adds user to store collection.
        /// </summary>
        /// <param name="context"></param>
        public void AddUser(HubCallerContext context)
        {
            //var username = context.User.Identity.Name;
            var userId = Guid.Parse(context.UserIdentifier);

            //store the userid to the list.  
            if (!_onlineUsers.ContainsKey(userId))
            {
                _onlineUsers.Add(userId, context.User.Identity.Name);
            } 
        }

        /// <summary>
        /// Checks if user status is currently connected to the hub or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userID"></param>  
        public bool IsUserOnline(string userName, out Guid userID)
        {
            bool exists = _onlineUsers.ContainsValue(userName);

            // If userName exists in list, then its in available in the set of users.
            userID = exists ? (_onlineUsers.FirstOrDefault(o => o.Value == userName).Key) : Guid.Empty; 

            return exists;
        }

        /// <summary>
        /// Removes user from store collection.
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveUser(Guid userId)
        {
            //store the userid to the list.  
            if (_onlineUsers.ContainsKey(userId))
            {
                _onlineUsers.Remove(userId);
            }
        }
    }
}