using Microsoft.AspNetCore.Mvc;
using VacancyParser.Application.Vacancies;

namespace VacancyParser.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacanciesController : ControllerBase
{
    private readonly IVacancyService _vacancies;

    public VacanciesController(IVacancyService vacancies) => _vacancies = vacancies;

    [HttpGet]
    public async Task<IEnumerable<GetVacancyResponse>> Get([FromQuery] GetVacanciesRequest request) =>
        await _vacancies.Get(request);

    [HttpPost]
    public async Task<IEnumerable<GetVacancyResponse>> Parse([FromBody] GetVacanciesRequest request) =>
        await _vacancies.Parse(request);
}
