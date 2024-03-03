using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string cadena = "server = labsystec.net;user = labsyste_clubDep; database = clubDeportivo; password = 8We0ds?20";

builder.Services.AddDbContext<ClubDeportivoContext>(option => option.UseMySql(cadena, ServerVersion.AutoDetect(cadena)));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<Repository<Categoria>>();
//builder.Services.AddTransient<Repository<Usuarios>>();
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryJugadores>();
builder.Services.AddTransient<RepositoryPagos>();


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
