using Microsoft.AspNetCore.Identity;
using System;
using Web_search_job.DatabaseClasses.UserFolder.ConfigurationModels;
using Web_search_job.DatabaseClasses.UserFolder.Services;
using Web_search_job.DatabaseClasses.UserFolder;
using Web_search_job.DatabaseClasses;
using Web_search_job.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web_search_job.Host.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtConfiguration>(
                configuration.GetSection(JwtConfiguration.Position));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddTransient<IAuthService, AuthService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ElevatedRights", policy =>
                    policy.RequireRole(Role.Admin));
                options.AddPolicy("StandardRights", policy =>
                    policy.RequireRole(Role.Admin, Role.User, Role.Employer));
            });

            return services;
        }
    }
}
