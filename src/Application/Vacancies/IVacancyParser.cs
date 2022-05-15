namespace Application.Vacancies;

public interface IVacancyParser
{ 
    IEnumerable<GetVacancyResponse> Get(GetVacanciesRequest request);
}
