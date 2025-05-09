using APICatalog.API.Filters;
using APICatalog.APICataolog.Data.Context;
using APICatalog.Core.DI;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true; // Força todas as URLs para minúsculo
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var stringPostgres = builder.Configuration.GetConnectionString("DefaultConnectionPostgres");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(stringPostgres));
var stringSqlServer = builder.Configuration.GetConnectionString("DefaultConnectionSqlServer");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(stringSqlServer));

builder.Services.AddDependencyInjectionConfig();

var app = builder.Build();

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
