using System.Security.Claims;

namespace GamaLearn.ChatApp.Domain.Authentication
{
    /// <summary>
    /// Represents the issued JWT security token 
    /// </summary>
    public class UserSecurityToken
    {
        public string Token { get; set; }

        public DateTime Validity { get; set; }

        public IEnumerable<Claim> UserClaims { get; set; }
    }
}
