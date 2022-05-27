using HtmlAgilityPack;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using VacancyParser.Application.Vacancies;
using VacancyParser.Infrastructure.Services;

namespace VacancyParser.Infrastructure.HtmlParsers;

public class RabotaUaVacancyParser : IVacancyParser
{
    private readonly IDynamicPageService _dynamicPage;

    public RabotaUaVacancyParser(IDynamicPageService dynamicPage) => _dynamicPage = dynamicPage;

    public IEnumerable<GetVacancyResponse> Parse(GetVacanciesRequest request)
    {
        var (category, city) = request;

        const string host = "https://rabota.ua";
        var url = $"{host}/ua/zapros/{category}/{city}";
        var waitUntil = ExpectedConditions.ElementExists(By.ClassName("card"));
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

                var title = vacancyInfoNode.FirstChild.InnerText.Trim();
                var companyName = vacancyInfoNode.ChildNodes[2].FirstChild.InnerText.Trim();
                var link = host + node.Attributes["href"].Value;
                var salary = vacancyInfoNode.ChildNodes[1].InnerText.Replace("&nbsp", "").Trim();

                return new GetVacancyResponse(title, companyName, salary, link);
            });
    }
}
