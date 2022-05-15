using Application.Vacancies;
using HtmlAgilityPack;
using Infrastructure.Services;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Infrastructure.HtmlParsers;

public class RabotaUaVacancyParser : IVacancyParser
{
    private readonly IDynamicPageService _dynamicPage;

    public RabotaUaVacancyParser(IDynamicPageService dynamicPage) => _dynamicPage = dynamicPage;

    public async Task<IEnumerable<GetVacancyResponse>> Get(GetVacanciesRequest request)
    {
        var (category, city) = request;

        var url = $"https://rabota.ua/zapros/{category}/{city}";
        var waitUntil = ExpectedConditions.ElementExists(By.ClassName("list-container"));

        var html = await _dynamicPage.GetHtml(url, waitUntil);

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
                Title = node.FirstChild.InnerText,
                CompanyName = node.ChildNodes[2].FirstChild.InnerText,
                City = node.ChildNodes[2].ChildNodes[3].InnerText
            })
            .Select(v => new GetVacancyResponse(v.Title, v.CompanyName, string.Empty, string.Empty));
    }
}
