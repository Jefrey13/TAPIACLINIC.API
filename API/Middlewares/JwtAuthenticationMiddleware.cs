using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Middlewares
{
    /// <summary>
    /// Middleware to configure JWT authentication for securing the API.
    /// </summary>
    public static class JwtAuthenticationMiddleware
    {
        /// <summary>
        /// Extension method to configure JWT authentication using values from configuration.
        /// </summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero // Reduce token expiration time discrepancy
                };
            });

            return services;
        }

        /// <summary>
        /// Use the JWT authentication in the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
        {
            // Use the authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
