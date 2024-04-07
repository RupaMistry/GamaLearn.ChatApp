namespace GamaLearn.ChatApp.Api.ExtensionMethods
{
    /// <summary>
    /// Extension methods for IServiceCollection.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Setups and registers IdentityAuth and ChatApp DBContext.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConnection"></param>
        public static void SetupDatabase(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<ChatAppDBContext>(options =>
                options.UseSqlServer(dbConnection,
                    o => o.MigrationsHistoryTable(
                        tableName: HistoryRepository.DefaultTableName,
                        schema: "Chat")));

            services.AddDbContext<AuthDBContext>(options =>
                options.UseSqlServer(dbConnection,
                    o => o.MigrationsHistoryTable(
                        tableName: HistoryRepository.DefaultTableName,
                        schema: "Identity")));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
            });

            // For Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDBContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Setups and registers JWT Authentication.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtConfig"></param>
        public static void SetupJwtAuthentication(this IServiceCollection services, JwtOptions jwtConfig)
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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.ValidAudience,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                };
            });
        }
    }
}