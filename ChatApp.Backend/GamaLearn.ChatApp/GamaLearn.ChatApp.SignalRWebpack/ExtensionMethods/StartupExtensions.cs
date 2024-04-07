namespace GamaLearn.ChatApp.SignalRWebpack.ExtensionMethods
{
    /// <summary>
    /// Extension methods for IServiceCollection.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Setups and register ChatApp DBContext for ChatHub.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConnection"></param>
        public static void SetupDatabase(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<ChatAppDBContext>(options =>
                options.UseSqlServer(dbConnection));
        }

        /// <summary>
        /// Setups and registers JWT Authentication for ChatHub.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="signingKey"></param>
        public static void SetupJwtAuthentication(this IServiceCollection services, string signingKey)
        {
            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}