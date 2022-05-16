using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacancyParser.Infrastructure.Extensions;

namespace VacancyParser.Infrastructure.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Testing.json", true, true)
            .Build();

        services.AddInfrastructure(configuration);
    }
}