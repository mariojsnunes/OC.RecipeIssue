var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(
    (context, options) =>
    {
        // Remove Server HTTP Header from response.
        options.AddServerHeader = false;
        // Handle requests up to 150 MB - This is necessary for linux, for windows IIS, use web.config or Configure<IISServerOptions>
        options.Limits.MaxRequestBodySize = 157286400;
    }
);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

var ocBuilder = builder.Services.AddOrchardCms().AddCaching();

builder.Services.AddResponseCompression();

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();

app.UseResponseCompression();

// Host OrchardCore in /cms subfolder
app.Map(
    new PathString("/cms"),
    cms =>
    {
        cms.UseOrchardCore();
    }
);

// Subfolder is not an issue, without it the error also occurs.
//app.UseOrchardCore();

await app.RunAsync();
