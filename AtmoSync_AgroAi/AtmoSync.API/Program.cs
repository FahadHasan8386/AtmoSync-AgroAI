
using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.API.Repository;
using AtmoSync.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database

builder.Services.AddScoped<IDbConnection>(sp =>new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region Repository

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IDHTSensorRepository, DHTSensorRepository>();

builder.Services.AddScoped<IMQ136SensorRepository, MQ136SensorRepository>();

builder.Services.AddScoped<IMQ7SensorRepository, MQ7SensorRepository>();

#endregion

#region Services

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IDHTSensorService, DHTSensorService>();

builder.Services.AddScoped<IMQ136SensorService, MQ136SensorService>();

builder.Services.AddScoped<IMQ7SensorService, MQ7SensorService>();

builder.Services.AddScoped<JwtService>();

#endregion

#region JWT

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });

builder.Services.AddAuthorization();

#endregion



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

    var adminEmail = "admin@atmosync.com";
    var existingAdmin = await userRepository.GetByEmailAsync(adminEmail);

    if (existingAdmin == null)
    {
        var admin = new User
        {
            FullName = "System Admin",
            Email = adminEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin",
            CreatedAt = DateTime.Now,
            InActive = false
        };

        await userRepository.CreateAsync(admin);
    }
}

app.Run();
