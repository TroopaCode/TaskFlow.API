using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.API.AutoMappers;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Exceptions;
using TaskFlow.API.Models;
using TaskFlow.API.Repositories;
using TaskFlow.API.Repositories.Interfaces;
using TaskFlow.API.Repositories.UnitOfWork;
using TaskFlow.API.Services;
using TaskFlow.API.Services.Interfaces;
using TaskFlow.API.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Inyección de dependencias para el servicio de tareas
builder.Services.AddScoped<ITaskService, TaskService>();

// 🔹 REGISTRO DEL DbContext (ESTO ES CLAVE)
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Validador
builder.Services.AddScoped<IValidator<TaskItemInsertDTO>, TaskItemInsertValidator>();
builder.Services.AddScoped<IValidator<TaskItemUpdateDTO>, TaskItemUpdateValidator>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

//Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Exceptions (Middleware)
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
