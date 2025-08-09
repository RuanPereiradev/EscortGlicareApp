using DataConnection.Repositories;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Repositories.DataConnection;
using GlicareApp.Services.CommandsHandlers;
using MediatR;
using Npgsql;
using Serilog;
using System.Data;
using System.Reflection;
using FluentValidation.AspNetCore;
using GlicareApp.Services.Commands;
using GlicareApp.Services.Implements;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Aqui você registra o MediatR explicitando o assembly do handler
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateEscortCommandHandler).Assembly);
});

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("PostgreSQL");
    var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    return connection;
});

builder.Services.AddScoped<IDbTransaction>(sp =>
{
    var connection = sp.GetRequiredService<IDbConnection>();
    return connection.BeginTransaction();
});

builder.Services.AddScoped<PostgreDbSession>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEscortRepository, EscortRepository>();
builder.Services.AddScoped<IPacientRepository, PatientRepository>();
builder.Services.AddScoped<ITokenValidatorService, TokenValidatorService>();


builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateEscortCommandValidator>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();