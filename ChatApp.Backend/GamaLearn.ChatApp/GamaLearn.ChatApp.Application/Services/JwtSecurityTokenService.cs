namespace GamaLearn.ChatApp.Application.Services
{
    /// <summary>
    /// Service for issuing JwtSecurityTokens
    /// </summary>
    /// <param name="options"></param>
    public class JwtSecurityTokenService(IOptions<JwtOptions> options) : ITokenService
    {
        private readonly JwtOptions options = options.Value;

        /// <summary>
        /// Returns serialized JwtSecurityToken for given user claims.
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns>UserSecurityToken</returns>
        public UserSecurityToken GetJwtSecurityToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.Secret));

            var token = new JwtSecurityToken
                (
                    issuer: this.options.ValidIssuer,
                    audience: this.options.ValidAudience,
                    expires: DateTime.UtcNow.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new UserSecurityToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Validity = token.ValidTo,
                UserClaims = authClaims
            };
        }
    }
}