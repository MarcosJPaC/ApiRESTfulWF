using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Working with SQLLite In Asp.net Core Web API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("nuevapolitica", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("nuevapolitica");
app.UseAuthorization();
app.MapControllers();
app.Run();
