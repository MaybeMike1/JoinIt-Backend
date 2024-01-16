using JoinIt_Backend.Features.Activity;
using JoinIt_Backend.Features.Authentication;
using JoinIt_Backend.Features.Payment;
using JoinIt_Backend.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JoinIt_Backend.Shared;
using JoinIt_Backend.Features.Chat;
using JoinIt_Backend.Features.Chat.Services;
using Microsoft.AspNetCore.SignalR;
using JoinIt_Backend.Features.Location;

namespace JoinIt_Backend
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var _somePolicy = "_policy";
            services.AddControllers();
            services.AddCors(x =>
            {
                x.AddPolicy( name: _somePolicy, x => x.AllowAnyHeader().WithOrigins("*").AllowAnyMethod().DisallowCredentials());
            });
            services.AddSwaggerGen()
                .AddEndpointsApiExplorer()
                .AddShared(_configuration)
                .AddLocation()
                .AddChat()
                .AddActivity()
                .AddPaymentService()
                .AddAuth();
        }
                

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("_policy");
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(x =>
            {
                x.MapHub<ChatHub>("/chat");
                x.MapControllers();
            });
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
