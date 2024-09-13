using AdminBack.Application.DTO;
using AdminBack.Application.Services;
using AdminBack.Infraestructure.Data.Repositories;
using AdminBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ADMINContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>(); // Registro de UsuarioService
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); // Registro de UsuarioRepository
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Registro de AutoMapper

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/login", async (int userId, IUsuarioService usuarioService) =>
{
    var result = await usuarioService.LoginAsync(userId);
    return result.StartsWith("Error") ? Results.BadRequest(result) : Results.Ok(result);
})
.WithName("Login");

app.MapPost("/logout", async (int userId, IUsuarioService usuarioService) =>
{
    await usuarioService.LogoutAsync(userId);
    return Results.Ok("Sesión cerrada con éxito.");
})
.WithName("Logout");

app.MapGet("/usuarios/{id:int}", async (int id, IUsuarioService usuarioService) =>
{
    var usuario = await usuarioService.GetUsuarioByIdAsync(id);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
})
.WithName("GetUsuarioById");

app.MapGet("/usuarios", async (IUsuarioService usuarioService) =>
{
    var usuarios = await usuarioService.GetAllUsuariosAsync();
    return Results.Ok(usuarios);
})
.WithName("GetAllUsuarios");

app.MapPost("/usuarios", async ([FromBody] CreateUsuarioDto usuarioDto, IUsuarioService usuarioService) =>
{
    await usuarioService.AddUsuarioAsync(usuarioDto);
    return Results.Created($"/usuarios/{usuarioDto.Usuario}", usuarioDto);
})
.WithName("AddUsuario");

app.MapPut("/usuarios", async ([FromBody] UpdateUsuarioDto usuarioDto, IUsuarioService usuarioService) =>
{
    await usuarioService.UpdateUsuarioAsync(usuarioDto);
    return Results.NoContent();
})
.WithName("UpdateUsuario");

app.MapDelete("/usuarios/{id:int}", async (int id, IUsuarioService usuarioService) =>
{
    await usuarioService.DeleteUsuarioAsync(id);
    return Results.NoContent();
})
.WithName("DeleteUsuario");

app.Run();

