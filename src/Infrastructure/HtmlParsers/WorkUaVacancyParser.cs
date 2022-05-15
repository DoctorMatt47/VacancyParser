using Application.Vacancies;
using HtmlAgilityPack;
using Infrastructure.Services;

namespace Infrastructure.HtmlParsers;

public class WorkUaVacancyParser : IVacancyParser
{
    private readonly IDynamicPageService _dynamicPage;

    public WorkUaVacancyParser(IDynamicPageService dynamicPage) => _dynamicPage = dynamicPage;

    public async Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request)
    {
        var uri = $"https://www.work.ua/jobs-{request.City}-{request.Category}";
        var html = await _dynamicPage.GetHtml(uri, null);

        var document = new HtmlDocument();
        document.LoadHtml(html);

        return document.GetElementbyId("pjax-job-list").ChildNodes
            .Where(node => node.HasClass("job-link"))
            .Select(node =>
            {
                var companyAndCityNode = node.ChildNodes.First(n => n.HasClass("flex"));
                return new
                {
                    Title = node.ChildNodes.First(n => n.OriginalName == "h2").ChildNodes[1].InnerText,
                    CompanyName = companyAndCityNode.ChildNodes[1].InnerText,
                    City = companyAndCityNode.ChildNodes[3].InnerText
                };
            })
            .Select(v => new GetVacancyResponse(v.Title, v.CompanyName, string.Empty, string.Empty));
    }
}
