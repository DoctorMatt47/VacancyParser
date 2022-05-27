using HtmlAgilityPack;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using VacancyParser.Application.Vacancies;
using VacancyParser.Infrastructure.Services;

namespace VacancyParser.Infrastructure.HtmlParsers;

public class WorkUaVacancyParser : IVacancyParser
{
    private readonly IDynamicPageService _dynamicPage;

    public WorkUaVacancyParser(IDynamicPageService dynamicPage) => _dynamicPage = dynamicPage;

    public IEnumerable<GetVacancyResponse> Parse(GetVacanciesRequest request)
    {
        var (city, category) = request;

        const string host = "https://www.work.ua";
        var uri = $"{host}/jobs-{city}-{category}";
        var waitUntil = ExpectedConditions.ElementExists(By.Id("pjax-job-list"));
        var html = _dynamicPage.GetHtml(uri, waitUntil);

        var document = new HtmlDocument();
        document.LoadHtml(html);

        return document.GetElementbyId("pjax-job-list").ChildNodes
            .TakeWhile(node => !node.HasClass("text-muted"))
            .Where(node => node.HasClass("job-link"))
            .Select(node =>
            {
                var titleNode = node.ChildNodes.First(n => n.OriginalName == "h2").ChildNodes[1];

                var title = titleNode.InnerText;
                var companyName = node.ChildNodes.First(n => n.HasClass("flex")).ChildNodes[1].InnerText;
                var link = host + titleNode.Attributes.First(attr => attr.Name == "href").Value;
                var salary = node.ChildNodes
                    .FirstOrDefault(n => n.OriginalName == "div" && !n.HasAttributes)
                    ?.ChildNodes[1].InnerText
                    .Replace("&nbsp", "")
                    .Trim() ?? string.Empty;

                return new GetVacancyResponse(title, companyName, salary, link);
            });
    }
}
