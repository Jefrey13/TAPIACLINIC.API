using Infrastructure;
using Application;
using API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using API.Utils;
using Newtonsoft.Json;
using Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Se agregan servicios a la colección de servicios

// Carga de variables de entorno (opcional)
// DotNetEnv.Env.Load();

// Registra los servicios de la capa de aplicación
builder.Services.AddApplicationServices(builder.Configuration);


// Registra los servicios de la capa de infraestructura con su configuración
builder.Services.AddInfrastructureServices(builder.Configuration);

// Agrega los controladores y configuraciones de Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger con autenticación JWT
builder.Services.AddSwaggerGen(options =>
{
    // Define el documento de Swagger con título y versión de la API
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinica Tapia API", Version = "v1" });

    // Agrega la definición de seguridad para el uso de JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {token}"
    });

    // Aplica el esquema de seguridad globalmente a todos los endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configuración de controladores con política de autorización global
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// Registra y configura la política CORS personalizada
builder.Services.AddCustomCors();

// Configura la autenticación y manejo de tokens JWT
builder.Services.AddAuthentication(options =>
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    // Configura los eventos para personalizar las respuestas de error de autenticación
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].ToString();
            Console.WriteLine("Token received: " + token);
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            return Task.CompletedTask;
        },
        OnChallenge = async context =>
        {
            context.HandleResponse(); // Evita la respuesta predeterminada de no autorizado

            // Genera una respuesta de error con el formato de ApiResponse
            var errorResponse = new ApiResponse<string>(
                success: false,
                message: "Unauthorized access",
                data: null,
                statusCode: StatusCodes.Status401Unauthorized
            );

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    };
});


// Configuración para redirección HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    options.HttpsPort = 443;
});

// Configuración para convenciones de tipo de respuesta en controladores
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new ProducesResponseTypeConvention());
});

// Activa la compresión de respuestas
builder.Services.AddResponseCompression();

var app = builder.Build();

// Crear el usuario administrador al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userRepository = services.GetRequiredService<IUserRepository>(); // Repositorio de usuario
    await userRepository.CreateAdminUserAsync();  // Crear usuario administrador
}

// Usa el middleware de manejo global de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Habilita el middleware de CORS personalizado
app.UseCustomCors();

// Habilita el middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Activa el middleware de compresión de respuestas
app.UseResponseCompression();

// Configura la canalización de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();