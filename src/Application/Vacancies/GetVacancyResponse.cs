namespace VacancyParser.Application.Vacancies;

public record GetVacancyResponse(
    string Title,
    string CompanyName,
    string Salary,
    string Link);
