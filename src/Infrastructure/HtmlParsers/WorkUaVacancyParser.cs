using Application.Vacancies;
using HtmlAgilityPack;
using Infrastructure.Services;

namespace Infrastructure.HtmlParsers;

public class WorkUaVacancyParser : IVacancyParser
{
    private readonly IHtmlPageService _htmlPage;

    public WorkUaVacancyParser(IHtmlPageService htmlPage) => _htmlPage = htmlPage;

    public async Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request)
    {
        throw new NotImplementedException();
    }
}
