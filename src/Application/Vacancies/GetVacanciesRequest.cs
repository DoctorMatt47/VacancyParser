namespace VacancyParser.Application.Vacancies;

public record GetVacanciesRequest(
    string Category,
    string City);
