using Microsoft.EntityFrameworkCore;
using VacancyParser.Application.Interfaces;
using VacancyParser.Domain.Entities;

namespace VacancyParser.Application.Vacancies;

public class VacancyService : IVacancyService
{
    private readonly IVacancyParserContext _context;
    private readonly IEnumerable<IVacancyParser> _parsers;

    public VacancyService(IEnumerable<IVacancyParser> parsers, IVacancyParserContext context)
    {
        _parsers = parsers;
        _context = context;
    }

    public async Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request)
    {
        return await _context.Set<Vacancy>()
            .Where(v => v.City == request.City && v.Category == request.Category)
            .Select(v => new GetVacancyResponse(v.Title, v.CompanyName, v.Salary, v.Link))
            .ToListAsync();
    }

    public async Task<IEnumerable<GetVacancyResponse>> Parse(GetVacanciesRequest request)
    {
        var vacancyResponses = _parsers
            .Select(parser => parser.Parse(request))
            .SelectMany(v => v)
            .ToList();

        var existLinks = _context.Set<Vacancy>().Select(v => v.Link);

        var vacancies = vacancyResponses
            .ExceptBy(existLinks, v => v.Link)
            .Select(v => new Vacancy(v.Link, v.Title, v.CompanyName, v.Salary, request.City, request.Category));

        await _context.Set<Vacancy>().AddRangeAsync(vacancies);
        await _context.SaveChangesAsync();

        return vacancyResponses;
    }
}
