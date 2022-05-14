namespace Application.Vacancies;

public interface IVacancyParser
{ 
    Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request);
}
