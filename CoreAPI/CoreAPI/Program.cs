using CoreAPI.Data.DBContext;
using CoreAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DataDBContextSetup.AddDbContext(builder.Services, builder.Configuration.GetConnectionString("DBContext"));

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrateDbContext<CoreAPIDBContext>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
