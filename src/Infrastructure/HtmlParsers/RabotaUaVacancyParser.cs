using Application.Vacancies;
using HtmlAgilityPack;
using Infrastructure.Services;

namespace Infrastructure.HtmlParsers;

public class RabotaUaVacancyParser : IVacancyParser
{
    private readonly IHtmlPageService _htmlPage;

    public RabotaUaVacancyParser(IHtmlPageService htmlPage) => _htmlPage = htmlPage;
    
    public async Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request)
    {
        var (category, city) = request;
        var html = await _htmlPage.Get($"https://rabota.ua/zapros/{category}/{city}");

        var document = new HtmlDocument();
        document.LoadHtml(html);

        return document.DocumentNode.Descendants()
            .First(node => node.HasClass("list-container")).ChildNodes
            .First(node => node.HasClass("santa-flex")
                && node.HasClass("santa-flex-col")
                && node.HasClass("ng-star-inserted")).ChildNodes
            .Take(1..^8)
            .Select(node => node.FirstChild.FirstChild.ChildNodes
                .First(n => n.HasClass("santa-flex")).ChildNodes[3].FirstChild)
            .Select(node => new
            {
                title = node.FirstChild.InnerText,
                companyName = node.ChildNodes[2].FirstChild.InnerText,
                city = node.ChildNodes[2].ChildNodes[3].InnerText
            }).Select(v => new GetVacancyResponse(v.title, v.companyName, "", ""));
    }
}
