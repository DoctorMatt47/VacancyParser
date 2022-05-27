namespace VacancyParser.Application.Vacancies;

public interface IVacancyParser
{
    IEnumerable<GetVacancyResponse> Parse(GetVacanciesRequest request);
}
