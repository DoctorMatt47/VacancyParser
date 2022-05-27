using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VacancyParser.Application.Interfaces;
using VacancyParser.Domain.Entities;

namespace VacancyParser.Infrastructure.Persistence;

public sealed class VacancyParserContext : DbContext, IVacancyParserContext
{
    private VacancyParserContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Vacancy> Vacancies { get; set; } = null!;

    public Task SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}