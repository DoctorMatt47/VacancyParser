using Application.Vacancies;
using Infrastructure.HtmlParsers;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddSingleton(configuration)
            .AddTransient<IDynamicPageService, DynamicPageService>()
            .AddTransient<IVacancyParser, WorkUaVacancyParser>()
            .AddTransient<IVacancyParser, RabotaUaVacancyParser>();
}
