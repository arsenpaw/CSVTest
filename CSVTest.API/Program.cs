

using CSVTest.DataAccess.Contexts;
using CSVTest.DataAccess.CSV;
using CSVTest.DataAccess.CSV.Map;
using CSVTest.DataAccess.Entities;
using CSVTest.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<CsvContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var parentOfRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;

builder.Services.SeedCsvData<CsvContext, Trip>(
    Path.Combine(parentOfRoot, builder.Configuration.GetValue<string>("CsvFilePath")),
    dbContext => dbContext.Trips,
    new TripMap());
var app = builder.Build();
app.MigrateDatabase<CsvContext>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
