
var builder = WebApplication.CreateBuilder(args);
var signingKey = builder.Configuration.GetValue<string>("JwtConfiguration:Secret");

// Database services
var dbConnection = builder.Configuration.GetConnectionString("ChatAppDBConnection");
builder.Services.SetupDatabase(dbConnection);

// JWTAuthentication services
builder.Services.SetupJwtAuthentication(signingKey);

// Repository and ApplicationServices
builder.Services.AddScoped<IOnlineUserStore, OnlineUserStore>();
builder.Services.AddScoped<IMessageRepository<UserMessage>, UserMessageRepository>();
builder.Services.AddScoped<IMessageStore<UserMessage>, MessageStoreService>();

// Add services to the container.
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 

// Add SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Read Dealing with CORS from blog post https://www.abrahamberg.com/blog/aspnet-signalr-and-react/
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // allow credentials
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Map and host GamaLearn hub to "/hub" route
app.MapHub<GamaLearnChatHub>("/hub");

app.Run();
