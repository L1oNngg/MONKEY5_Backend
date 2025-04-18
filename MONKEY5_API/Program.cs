using Microsoft.EntityFrameworkCore;
using MONKEY5.DataAccessObjects;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    // Set a maximum depth for serialization
    options.JsonSerializerOptions.MaxDepth = 8; // Limit to 8 levels of nesting
});

// Add DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyDB")));

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "MONKEY5 API", 
        Version = "v1",
        Description = "API for MONKEY5 Home Service Booking System"
    });
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Apply migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MyDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation("Applying database migrations...");
        context.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
        
        // Create uploads directory if it doesn't exist
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
            logger.LogInformation("Created uploads directory.");
        }
        
        // Try multiple possible locations for MockImages
        string[] possiblePaths = new string[] {
            Path.Combine(app.Environment.ContentRootPath, "MockImages"),
            Path.Combine(app.Environment.ContentRootPath, "MONKEY5_API", "MockImages"),
            Path.Combine(Directory.GetCurrentDirectory(), "MockImages"),
            Path.Combine(Directory.GetCurrentDirectory(), "MONKEY5_API", "MockImages"),
            "/app/MockImages",
            "/app/MONKEY5_API/MockImages"
        };

        bool foundMockImages = false;
        foreach (var path in possiblePaths)
        {
            logger.LogInformation($"Checking for MockImages at: {path}");
            if (Directory.Exists(path))
            {
                logger.LogInformation($"Found MockImages directory at: {path}");
                foreach (var file in Directory.GetFiles(path))
                {
                    var fileName = Path.GetFileName(file);
                    var destPath = Path.Combine(uploadsFolder, fileName);
                    if (!System.IO.File.Exists(destPath))
                    {
                        System.IO.File.Copy(file, destPath);
                        logger.LogInformation($"Copied mock image: {fileName}");
                    }
                }
                foundMockImages = true;
                break;
            }
        }

        if (!foundMockImages)
        {
            logger.LogWarning("MockImages directory not found in any of the checked locations.");
        }

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database or copying mock images.");
    }
}

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MONKEY5 API v1");
    // Set Swagger UI at the app's root
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

var wwwrootPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Created wwwroot directory.");
}

app.UseStaticFiles();

app.MapControllers();

app.Run();
