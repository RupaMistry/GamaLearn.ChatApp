namespace GamaLearn.ChatApp.Infrastructure
{
    /// <summary>
    /// AuthDBContext for identity framework.
    /// </summary>
    public class AuthDBContext(DbContextOptions<AuthDBContext> options) : IdentityDbContext<ApplicationUser>(options)
    { 
        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");

            base.OnModelCreating(modelBuilder); 
        }
    }
}