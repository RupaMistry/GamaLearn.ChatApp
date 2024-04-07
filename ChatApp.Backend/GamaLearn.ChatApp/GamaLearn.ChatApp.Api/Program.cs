var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(opt =>
{
    opt.AddDebug();
    opt.AddConsole();
});

builder.Services.AddControllers(); 

// SwaggerUI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database services
var dbConnection = builder.Configuration.GetConnectionString("ChatAppDBConnection");
builder.Services.SetupDatabase(dbConnection);

// JWTAuthentication services
JwtOptions jwtConfig = new();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.JwtConfiguration));
builder.Configuration.GetSection(JwtOptions.JwtConfiguration).Bind(jwtConfig);
builder.Services.SetupJwtAuthentication(jwtConfig);

// Repository and ApplicationServices
builder.Services.AddScoped<ITokenService, JwtSecurityTokenService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();

builder.Services.AddScoped<IChatRoomRepository<ChatRoom>, ChatRoomRepository>();
builder.Services.AddScoped<IChatRoomService<ChatRoom>, ChatRoomService>();

builder.Services.AddScoped<IMessageStore<UserMessage>, MessageStoreService>();
builder.Services.AddScoped<IMessageRepository<UserMessage>, UserMessageRepository>();

// Allow Cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", policy =>
{
    policy.WithHeaders("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Enable Cors
app.UseCors((opt) =>
{
    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}); 

app.Run();