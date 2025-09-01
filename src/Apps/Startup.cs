namespace Apps;

using Apps.Migrations;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDataMigration<AppsMigrations>();
    }
}
