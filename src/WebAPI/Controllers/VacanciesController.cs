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
    public IEnumerable<GetVacancyResponse> Get([FromQuery] GetVacanciesRequest request) => _vacancies.Get(request);
}