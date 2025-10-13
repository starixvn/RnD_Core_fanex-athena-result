using Fanex.Result.Sites.NET8.Middleware;
using Fanex.Result.Sites.NET8.Services;
using Fanex.Result.Sites.NET8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SystemWebAdapters;
using Microsoft.AspNetCore.Mvc.Razor;
using Fanex.Result.Core.Base;
using Fanex.Result.Core.RemotingRef;
using Fanex.RemotingRef;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews(options =>
{
    // Add global filters if needed
    // options.Filters.Add<GlobalExceptionFilter>();
});

// Configure view location to ensure proper view resolution
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Clear default formats and set our own
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
    options.ViewLocationFormats.Add("Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("Views/Shared/{0}.cshtml");
});


// Add API controllers
builder.Services.AddControllers();

// Add logging
builder.Services.AddLogging();

// Add memory cache for circuit breaker pattern
builder.Services.AddMemoryCache();

// Add health checks
builder.Services.AddHealthChecks();

// Add custom services
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IThirdPartyResult, ThirdPartyResult>();
ResultCoreManager.RegisterRefDataHelper(new RefDataHelper());
RefDataTypeContainer
            .ConnectTo(new Uri("http://remotingref-api-core.nexdev.net/"))
            .FromApplication(Assembly.GetExecutingAssembly().FullName);
// Add System.Web adapters for compatibility
builder.Services.AddSystemWebAdapters()
    .AddJsonSessionSerializer(options =>
    {
        options.RegisterKey<string>("UserSession");
    })
    .AddRemoteAppClient(options =>
    {
        options.RemoteAppUrl = new Uri(builder.Configuration["RemotingRefService"] ?? "http://remotingref-api-core.nexdev.net/");
        options.ApiKey = builder.Configuration["ApiKey"] ?? "default-key";
    })
    .AddSessionClient();

// Configure JSON options
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Global exception handling middleware - MUST be first
app.UseGlobalExceptionHandler();

// System.Web adapters
app.UseSystemWebAdapters();

// Security middleware
app.UseHttpsRedirection();
app.UseCors();

// Static files
app.UseStaticFiles();

// Routing
app.UseRouting();

// Authentication & Authorization (if needed)
// app.UseAuthentication();
// app.UseAuthorization();

// Endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

// Health checks
app.MapHealthChecks("/health");

// Legacy routes for backward compatibility
app.MapControllerRoute(
    name: "legacy",
    pattern: "api/{controller}/{action}",
    defaults: new { controller = "ThirdPartyAPI", action = "Index" });

app.Run();
