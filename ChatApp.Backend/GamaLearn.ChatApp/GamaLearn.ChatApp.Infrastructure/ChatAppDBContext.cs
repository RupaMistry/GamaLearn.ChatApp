namespace GamaLearn.ChatApp.Infrastructure
{
    /// <summary>
    /// DBContext for ChatApp DB.
    /// </summary>
    public class ChatAppDBContext(DbContextOptions<ChatAppDBContext> options) : DbContext(options)
    {
        public DbSet<UserMessage> UserMessages { get; set; } = null!;

        public DbSet<ChatRoom> ChatRooms { get; set; } = null!;

        /// <summary>
        /// Configures the schema needed for the ChatApp db.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Chat");

            modelBuilder.Entity<ChatRoom>().HasData(SeedChatRooms);

            base.OnModelCreating(modelBuilder);
        }

        private static readonly ChatRoom[] SeedChatRooms =
        {
            new() { ID=1, Name="Swift Assess"},
            new() {ID=2, Name = "Swift Task" },
            new() { ID=3,Name = "Smart Zone" },
        };
    }
}
