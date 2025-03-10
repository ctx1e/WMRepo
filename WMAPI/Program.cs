using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WMAPI.DTO.Settings;
using WMAPI.Models;
using WMAPI.Repository.Implementations;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Implementations;
using WMAPI.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add service CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEndWM",
       policy =>
        {
            policy.WithOrigins("https://localhost:7037", "http://localhost:5030")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });

});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // C?u hình ki?u ??t tên các thu?c tính JSON là CamelCase
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
// Regis DB
builder.Services.AddDbContext<WarehouseManagementContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddSingleton(provider =>
{
    var config = builder.Configuration.GetSection("Cloudinary").Get<CloundinarySettings>();
    return new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dki Repo and Service
// Product repo and service
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Warehouse In repo and service

builder.Services.AddScoped<IWarehouseInRepository, WarehouseInRepository>();
builder.Services.AddScoped<IWarehouseInService, WarehouseInService>();

// Warehouse In Detail repo and service

builder.Services.AddScoped<IWIDRepository, WIDRepository>();
builder.Services.AddScoped<IWIDService, WIDService>();


// Warehouse Out repo and service
builder.Services.AddScoped<IWarehouseOutRepository, WarehouseOutRepository>();
builder.Services.AddScoped<IWarehouseOutService, WarehouseOutService>();


// Warehouse Out Detail repo and service
builder.Services.AddScoped<IWODRepository, WODRepository>();
builder.Services.AddScoped<IWODService, WODService>();


// Inventory repo and service
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// User CORS
app.UseCors("AllowFrontEndWM");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
