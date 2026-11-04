using InventoryFlow.Business;
using InventoryFlow.DataAccess;
using InventoryFlow.DataAccess.Context;
using InventoryFlow.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductService>();
builder.Services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=InventoryFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

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