namespace VacancyParser.Application.Vacancies;

public record GetVacancyResponse(
    string Title,
    string CompanyName,
    string Description,
    string Salary,
    string Link);
