﻿namespace Application.Vacancies;

public interface IVacancyService
{
    IEnumerable<GetVacancyResponse> Get(GetVacanciesRequest request);
}