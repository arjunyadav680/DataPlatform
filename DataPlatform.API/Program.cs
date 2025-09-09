using DataPlatform.API;
using DataPlatform.Application;
using DataPlatform.Infrastructure;
using DataPlatform.Infrastructure.Extensions;
using Microsoft.AspNetCore.Rewrite;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Insert(0, new RoutePrefixConvention("api")); // To add prefix "api" in the APIs.
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

AppConfigure.AddCorsSettings(builder.Services, allowedOrigins, _myAllowSpecificOrigins);
AppConfigure.AddHttpClient(builder.Services);

// Get virtual path from appsettings or environment variable
var pathBase = builder.Configuration["App:VirtualPath"] ?? "/";

var app = builder.Build();

app.UseCors(_myAllowSpecificOrigins);

// Configure virtual path if specified

app.UsePathBase(pathBase);

// Write logic to redirect by default swagger page when the application starts.
// And also fix trailing slash url issue.
var option = new RewriteOptions();

if (app.Environment.IsDevelopment())
{
    option.AddRedirect("^$", "/scalar", 302);
}
else
{ 
    // In production, you can choose to redirect to Scalar or your Angular app
    // option.AddRedirect("^$", "/scalar", 302);  // Uncomment to redirect to API docs
    option.AddRedirect("^$", "index.html", 302);   // Redirect to Angular app
}

app.UseRewriter(option);

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Add Scalar UI(a modern Swagger-style UI, built into .NET)
    app.MapScalarApiReference();
}
else
{
    app.UseHttpsRedirection();
    // Enable Scalar in Production too
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

// Seed database in development
//if (app.Environment.IsDevelopment())
//{
    await app.Services.SeedDatabaseAsync();
//}

app.Run();
