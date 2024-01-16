using JoinIt_Backend;
using JoinIt_Backend.Features.Activity;
using JoinIt_Backend.Features.Authentication;
using JoinIt_Backend.Features.Payment;
using JoinIt_Backend.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();
    

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                
            });
}


//var builder = WebApplication.CreateBuilder(args);

//var config = builder.Configuration;
//// Add services to the container.
//builder.Services.AddShared(config);
//builder.Services.AddAuth();
//builder.Services.AddActivity();
//builder.Services.AddPaymentService();

//builder.Services.AddScoped<DatabaseContext>();
//builder.Services.AddDbContext<DatabaseContext>( opt =>
//{
//    opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
//});
//builder.Services.AddControllers();


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.TokenValidationParameters = new TokenValidationParameters();
//});
//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
