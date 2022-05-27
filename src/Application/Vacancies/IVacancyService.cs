namespace VacancyParser.Application.Vacancies;

public interface IVacancyService
{
    Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request);
    Task<IEnumerable<GetVacancyResponse>> Parse(GetVacanciesRequest request);
}
