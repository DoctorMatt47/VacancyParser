using Microsoft.EntityFrameworkCore;

namespace VacancyParser.Application.Interfaces;

public interface IVacancyParserContext
{
    DbSet<T> Set<T>() where T : class;
    Task SaveChangesAsync();
}
