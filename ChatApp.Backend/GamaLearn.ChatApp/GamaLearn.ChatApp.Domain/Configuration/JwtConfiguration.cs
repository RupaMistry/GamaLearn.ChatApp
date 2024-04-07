namespace GamaLearn.ChatApp.Domain.Configuration
{
    /// <summary>
    /// Represents JwtOptions configuration
    /// </summary>
    public class JwtOptions
    {
        public const string JwtConfiguration = "JwtConfiguration";

        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
