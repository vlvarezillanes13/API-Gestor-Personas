using ApiGestionPersonas.CasosDeUso;
using ApiGestionPersonas.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AGREGAR JWT A LA CONF
builder.Configuration.AddJsonFile("appsettings.json");
var SecretKey = builder.Configuration.GetSection("Settings").GetSection("SecretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(SecretKey!);
builder.Services.AddAuthentication( config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( config =>
{
    config.RequireHttpsMetadata= false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestor de Personas", Version = "v1" }));
// builder.Services.AddRouting( routing => routing.LowercaseUrls = true);

builder.Services.AddDbContext<PersonaDataBaseContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("ConnectionMysql")!);
});
builder.Services.AddDbContext<AutenticacionDataBaseContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("ConnectionMysql")!);
});

builder.Services.AddScoped<IUpdatePersonaUserCase, UpdatePersonaUserCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//AGREGAR JWT A LA CONF
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
