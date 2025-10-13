using System.Text;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Identity.DbContext;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        serviceCollection.AddDbContext<HrLeaveManagementIdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnection"));
        });
        
        serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<HrLeaveManagementIdentityDbContext>()
            .AddDefaultTokenProviders();

        serviceCollection.AddTransient<IAuthService, AuthService>();
        serviceCollection.AddTransient<IUserService, UserService>();

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });
        
        return serviceCollection;
    }
}