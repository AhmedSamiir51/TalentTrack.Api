using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using TalentTrack.Application;
using TalentTrack.Infrastructure;
using TalentTrack.Infrastructure.Data;
using TMG.SharedKernel.Constants;
using TMG.SharedKernel.Middleware;
using TMG.SharedKernel.Models;
using TMG.SharedKernel.SecurityFilters;

var logger = loggerConfiguration();

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile(CoreConstants.AppSetting, optional: true, reloadOnChange: true)
        .Build();

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureForAuth(configuration);



builder.Services.Configure<JWTConfiguration>(configuration.GetSection(JwtConfigConstants.JWT));
builder.Services.Configure<EmailConfiguration>(configuration.GetSection(EmailConfigurationConstants.EmailConfiguration));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Allow CROS
app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseRouting();

// Register the Exception middleware with the Exception instance
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();
SeedDatabase(app);

app.Run();

static void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}
static Serilog.ILogger loggerConfiguration()
{
    var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

    logger.Information("Starting web host");
    return logger;
}
