using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacancyParser.Application.Vacancies;
using VacancyParser.Infrastructure.HtmlParsers;
using VacancyParser.Infrastructure.Services;

namespace VacancyParser.Infrastructure.Extensions;

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
