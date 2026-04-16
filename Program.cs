using StationeryStore.Api.Models;
using StationeryStore.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<StationeryService>(); 


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new { Message = "Chào mừng đến với Mini Stationery Store API" }));
app.MapGet("/health", () => Results.Ok(new { Status = "Hệ thống đang chạy ổn định" }));

app.MapGet("/config", (IConfiguration config) => Results.Ok(new
{
    Store = config["AppSettings:StoreName"],
    Email = config["AppSettings:ContactEmail"],
    Environment = app.Environment.EnvironmentName
}));

app.MapControllers();

app.Run();