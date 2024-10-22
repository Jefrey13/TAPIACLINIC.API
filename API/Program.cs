using Infrastructure;
using Application;
using API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DotNetEnv.Env.Load();

// Add Application layer services
builder.Services.AddApplicationServices(builder.Configuration);  // Register Application layer services

// Add Infrastructure layer services with configuration
builder.Services.AddInfrastructureServices(builder.Configuration);  // Register Infrastructure layer services with configuration


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCustomCors();  // Register the custom CORS middleware

// Register JWT authentication
//builder.Services.AddJwtAuthentication(builder.Configuration);

// Enable HTTP to HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    options.HttpsPort = 443;
});

// Enable response compression
builder.Services.AddResponseCompression();

//JWT
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
});

var app = builder.Build();

// Enable CORS middleware
app.UseCustomCors();  // Use the custom CORS middleware in the app pipeline

// Enable JWT authentication middleware
//app.UseJwtAuthentication();

// Use response compression middleware
app.UseResponseCompression();

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

//Easy
/**
 * Exams
 * Permisos
 * Especialidades
 * States
 * Ciriguas
 * Roles
 * Menus
 * Usuarios
 *  * staff (Incluir la relacion con los usuarios)
 *  Schedule (Relacionar staff (deveriA ser la especialidad) y los formatos de fecha)
 *  Medical record
 */

//HARD
/**
 * Appointments
 **/