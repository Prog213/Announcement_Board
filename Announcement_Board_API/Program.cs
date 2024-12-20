using System.Data;
using Announcement_Board_API.Interfaces;
using Announcement_Board_API.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnection>(options => 
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly).AddFluentValidationAutoValidation();

var allowedOrigin = builder.Configuration["CorsSettings:AllowedOrigin"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRazorPagesApp",
        policy => policy.WithOrigins(allowedOrigin!)
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowRazorPagesApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
