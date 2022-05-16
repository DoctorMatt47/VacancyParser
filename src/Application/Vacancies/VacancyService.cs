namespace VacancyParser.Application.Vacancies;

public class VacancyService : IVacancyService
{
    private readonly IEnumerable<IVacancyParser> _parsers;

    public VacancyService(IEnumerable<IVacancyParser> parsers) => _parsers = parsers;

    public IEnumerable<GetVacancyResponse> Get(GetVacanciesRequest request)
    {
        var vacancies = _parsers.Select(parser => parser.Get(request)).ToList();

        return vacancies.SelectMany(v => v);
    }
}
