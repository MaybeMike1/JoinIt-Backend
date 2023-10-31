using JoinIt_Backend.Data;
using JoinIt_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<ICryptService, CryptService>();
builder.Services.AddScoped<IActivityContextProvider, ActivityContextProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters();
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
