using ApiGestionPersonas.CasosDeUso;
using ApiGestionPersonas.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddRouting( routing => routing.LowercaseUrls = true);

builder.Services.AddDbContext<PersonaDataBaseContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("ConnectionMysql"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
