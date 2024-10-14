namespace API.Middlewares
{
    /// <summary>
    /// Middleware to configure Cross-Origin Resource Sharing (CORS) for allowing external domains.
    /// </summary>
    public static class CorsMiddleware
    {
        /// <summary>
        /// Extension method to configure CORS policy allowing specific origins, methods, and headers.
        /// </summary>
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            // Configure the CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()   // Allow requests from any origin
                           .AllowAnyMethod()   // Allow all HTTP methods (GET, POST, PUT, etc.)
                           .AllowAnyHeader();  // Allow all headers (Content-Type, Authorization, etc.)
                });
            });

            return services;
        }

        /// <summary>
        /// Use the defined CORS policy in the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            // Use the CORS policy that allows all origins, methods, and headers
            app.UseCors("AllowAllOrigins");
            return app;
        }
    }
}
