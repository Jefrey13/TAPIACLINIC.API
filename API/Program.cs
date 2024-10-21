using Infrastructure;
using Application;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DotNetEnv.Env.Load();

// Add Application layer services
builder.Services.AddApplicationServices();  // Register Application layer services

// Add Infrastructure layer services with configuration
builder.Services.AddInfrastructureServices(builder.Configuration);  // Register Infrastructure layer services with configuration


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCustomCors();  // Register the custom CORS middleware

// Register JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Enable HTTP to HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    options.HttpsPort = 443;
});

// Enable response compression
builder.Services.AddResponseCompression();

var app = builder.Build();

// Enable CORS middleware
app.UseCustomCors();  // Use the custom CORS middleware in the app pipeline

// Enable JWT authentication middleware
app.UseJwtAuthentication();

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
 */

//MEDIUM
/**
 * Schedule (Relacionar staff (deveriA ser la especialidad) y los formatos de fecha)
 * 
 **/

//HARD
/**
 * Medical record
 * Appointments
 **/

/**
 * Agregar una respuesta estandar
 * Manejar 2 dtos, para el las requests y las responses.
 * 
 * 
 {
  "id": 0,
  "firstName": "string",
  "lastName": "string",
  "email": "string@gmail.com",
  "phone": "87906545",
  "address": "string",
  "gender": "M",
  "birthDate": "2001-10-21T11:08:53.902Z",
  "idCard": "73637-3737-3737S",
  "active": true,
  "createdAt": "2024-10-21T11:08:53.902Z",
  "updatedAt": "2024-10-21T11:08:53.902Z",
  "roleId": 1,
  "stateId": 1
}
 */