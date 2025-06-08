using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.Shared.Infrastructure;
using DittoBox.API.Shared.Infrastructure.Repositories;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Handlers.Internal;
using DittoBox.API.UserProfile.Application.Services;
using DittoBox.API.UserProfile.Domain.Clients;
using DittoBox.API.UserProfile.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Services.Application;
using DittoBox.API.UserProfile.Infrastructure.Clients;
using DittoBox.API.UserProfile.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DittoBox.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Configuration.AddUserSecrets<Program>();

            var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

            if (string.IsNullOrEmpty(postgresConnectionString))
            {
                postgresConnectionString = builder.Configuration.GetConnectionString("POSTGRES_CONNECTION_STRING");
            }
            if (string.IsNullOrEmpty(postgresConnectionString))
            {
                throw new ArgumentException("PostgreSQL connection string is not configured.");
            }

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(
                    postgresConnectionString
                )
            );

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            RegisterHandlers(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();


            // Reset database
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (Environment.GetEnvironmentVariable("RESET_DATABASE") == "true") {
                  db.Database.EnsureDeleted();
                }
                db.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void RegisterHandlers(WebApplicationBuilder builder)
        {
            /* UserProfile handlers */
            builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", corsPolicyBuilder =>
                            {
                corsPolicyBuilder.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
            });
    }); builder.Services.AddScoped<ICreateUserCommandHandler, CreateUserCommandHandler>();
            builder.Services.AddScoped<IGetUserQueryHandler, GetUserQueryHandler>();
            builder.Services.AddScoped<IDeleteUserCommandHandler, DeleteUserCommandHandler>();
            builder.Services.AddScoped<IChangePasswordCommandHandler, ChangePasswordCommandHandler>();
            builder.Services.AddScoped<IGetProfileDetailsQueryHandler, GetProfileDetailsQueryHandler>();
            builder.Services.AddScoped<IGrantPrivilegeCommandHandler, GrantPrivilegeCommandHandler>();
            builder.Services.AddScoped<IRevokePrivilegeCommandHandler, RevokePrivilegeCommandHandler>();
            builder.Services.AddScoped<IUpdateProfileNamesCommandHandler, UpdateProfileNamesCommandHandler>();
            builder.Services.AddScoped<ILoginCommandHandler, LoginCommandHandler>();

            
        }

        public static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfilePrivilegeRepository, ProfilePrivilegeRepository>();

        }

        public static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddHttpClient<IAccountServiceClient, AccountServiceClient>();
        }
    }
}
