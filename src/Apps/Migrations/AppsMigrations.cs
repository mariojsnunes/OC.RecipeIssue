namespace Apps.Migrations;

using System.Threading.Tasks;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;

public class AppsMigrations(IRecipeMigrator recipeMigrator) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        try
        {
            await recipeMigrator.ExecuteAsync("apps.recipe.json", this);
        }
        catch (Exception ex)
        {
            throw;
        }

        return 1;
    }
}
