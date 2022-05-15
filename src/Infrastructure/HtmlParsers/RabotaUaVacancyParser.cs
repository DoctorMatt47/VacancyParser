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

    public IEnumerable<GetVacancyResponse> Get(GetVacanciesRequest request)
    {
        var (category, city) = request;
        const string host = "https://rabota.ua";

        var url = $"{host}/zapros/{category}/{city}";
        var waitUntil = ExpectedConditions.ElementExists(By.ClassName("list-container"));

        var html = _dynamicPage.GetHtml(url, waitUntil);

        var document = new HtmlDocument();
        document.LoadHtml(html);

        return document.DocumentNode.Descendants()
            .First(node => node.HasClass("list-container")).ChildNodes
            .First(node => node.HasClass("santa-flex")
                && node.HasClass("santa-flex-col")
                && node.HasClass("ng-star-inserted")).ChildNodes
            .Take(1..^8)
            .Select(node => node.FirstChild.FirstChild)
            .Select(node =>
            {
                var vacancyInfoNode = node.ChildNodes
                    .First(n => n.HasClass("santa-flex"))
                    .ChildNodes[3].FirstChild;

                return new
                {
                    Title = vacancyInfoNode.FirstChild.InnerText,
                    CompanyName = vacancyInfoNode.ChildNodes[2].FirstChild.InnerText,
                    City = vacancyInfoNode.ChildNodes[2].ChildNodes[3].InnerText,
                    Link = node.Attributes["href"].Value
                };
            })
            .Select(v => new GetVacancyResponse(v.Title, v.CompanyName, string.Empty, string.Empty, host + v.Link));
    }
}