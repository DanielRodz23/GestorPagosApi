using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GestorPagosApi.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cadena = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("Conexion.json")
               .Build()
               .GetConnectionString("NeatConnection");

builder.Services.AddDbContext<ClubDeportivoContext>(option => option.UseMySql(cadena, ServerVersion.AutoDetect(cadena)));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<Repository<Categoria>>();
//builder.Services.AddTransient<Repository<Usuarios>>();
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryJugadores>();
builder.Services.AddTransient<RepositoryPagos>();
builder.Services.AddTransient<RepositoryTemporadas>();

var jwtconfig = new ConfigurationBuilder()
    .AddJsonFile("jwtsettings.json")
    .Build();

var tknValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwtconfig["Jwt:Issuer"],
    ValidAudience = jwtconfig["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtconfig["Jwt:Key"]))
};

builder.Services.AddSingleton(tknValidationParameters);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x=>{
    x.TokenValidationParameters = tknValidationParameters;
});

builder.Services.AddAuthorization(x=>{
    x.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
    x.AddPolicy(IdentityData.TesoreroUserPolicyName, p => p.RequireClaim(IdentityData.TesoreroUserClaimName, "true"));
    x.AddPolicy(IdentityData.ResponsableUserPolicyName, p => p.RequireClaim(IdentityData.ResponsableUserClaimName, "true"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
