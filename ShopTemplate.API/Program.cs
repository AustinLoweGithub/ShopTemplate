using Microsoft.EntityFrameworkCore;
using ShopTemplate.Api.Data;
using ShopTemplate.Api.Repositories.Contracts;
using ShopTemplate.Api.Repositories;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContextPool<ShopTemplateDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShopTemplateConnection")));


builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.WithOrigins("http://localhost:7241", "https://localhost:7241").AllowAnyMethod().WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
