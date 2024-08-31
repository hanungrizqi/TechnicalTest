using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Customers.Commands;
using MediatR;
using FluentValidation;
using Application.Customers.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan konfigurasi DbContext untuk SQL Server
builder.Services.AddDbContext<_dbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tambahkan MediatR
builder.Services.AddMediatR(typeof(CreateCustomerHandler).Assembly);

builder.Services.AddControllers();
// Tambahkan Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

// Mendaftarkan validator dari assembly yang mengandung CustomerValidator
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>();

// Tambahkan layanan otorisasi
builder.Services.AddAuthorization();

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
