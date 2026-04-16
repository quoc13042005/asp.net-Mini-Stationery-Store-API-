using StationeryStore.Api.Models;
using StationeryStore.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CỰC KỲ QUAN TRỌNG: Đăng ký StationeryService
builder.Services.AddSingleton<StationeryService>(); 

// Ánh xạ cấu hình từ appsettings.json vào Model
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// 2. Cấu hình Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Các Endpoints cơ bản (Minimal API)
app.MapGet("/", () => Results.Ok(new { Message = "Chào mừng đến với Mini Stationery Store API" }));
app.MapGet("/health", () => Results.Ok(new { Status = "Hệ thống đang chạy ổn định" }));

app.MapGet("/config", (IConfiguration config) => Results.Ok(new
{
    Store = config["AppSettings:StoreName"],
    Email = config["AppSettings:ContactEmail"],
    Environment = app.Environment.EnvironmentName
}));

// 4. Kích hoạt Controller
app.MapControllers();

app.Run();