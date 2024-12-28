

using CSVTest.DataAccess.Contexts;
using CSVTest.DataAccess.CSV;
using CSVTest.DataAccess.CSV.Map;
using CSVTest.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CsvContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.SeedCsvData<CsvContext, Trip>(
    builder.Configuration.GetValue<string>("CsvFilePath"),
    dbContext => dbContext.Trips,
    new TripMap());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
