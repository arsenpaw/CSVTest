

using AutoMapper;
using CSVTest.DataAccess.Contexts;
using CSVTest.DataAccess.CSV;
using CSVTest.DataAccess.CSV.Map;
using CSVTest.DataAccess.Entities;
using CSVTest.Extensions;
using CSVTest.Middleware;
using CSVTest.ServiceConfiguration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "CSVTest.API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<CsvContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.InjectRepositories();
builder.Services.InjectServices();
builder.Services.AddControllers();
var app = builder.Build();
app.MigrateDatabase<CsvContext>();
var parentOfRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;

builder.Services.SeedCsvData<CsvContext, Trip>(
    Path.Combine(parentOfRoot, builder.Configuration.GetValue<string>("CsvFilePath")),
    dbContext => dbContext.Trips,
    new TripMap());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseExceptionMiddleware();

app.Run();
