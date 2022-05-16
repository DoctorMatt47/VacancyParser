using Microsoft.Extensions.DependencyInjection;
using VacancyParser.Application.Vacancies;

namespace VacancyParser.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddTransient<IVacancyService, VacancyService>();
}
