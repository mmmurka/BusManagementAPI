using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LoggerService;
using ExceptionHandlingMiddleware;
using LoggingMiddleware;
using BusManagementAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

// Налаштування сервісів
// 1. Кросдоменні запити (CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 2. Логування (припускаємо, що LoggerService створено у вашому окремому проекті LoggingService)
builder.Services.AddSingleton<LoggerService.LoggerService>();

// 3. Підтримка IIS
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

// 4. Підтримка контролерів
builder.Services.AddControllers();

// 5. API документація (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Налаштування middleware
// Дозволити CORS
app.UseCors("AllowAll");

// Підключити Swagger у режимі розробки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware для HTTPS
app.UseHttpsRedirection();

// Middleware для авторизації
app.UseAuthorization();

// Middleware для обробки виключень
app.UseMiddleware<ExceptionHandlingMiddleware.ExceptionHandlingMiddleware>();

// Middleware для запису запитів у лог
app.UseMiddleware<LoggingMiddleware.LoggingMiddleware>();

// Маршрутизація контролерів
app.MapControllers();

// Запуск програми
app.Run();
