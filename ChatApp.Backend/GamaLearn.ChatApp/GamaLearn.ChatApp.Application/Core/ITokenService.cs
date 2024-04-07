namespace GamaLearn.ChatApp.Application.Core
{
    /// <summary>
    /// IService for issuing JwtSecurityTokens
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Returns serialized JwtSecurityToken for given user claims.
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns>UserSecurityToken</returns>
        UserSecurityToken GetJwtSecurityToken(List<Claim> authClaims);
    }
}