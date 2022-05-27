﻿using HtmlAgilityPack;
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
        var html = _dynamicPage.GetHtml(uri, ExpectedConditions.ElementExists(By.Id("pjax-job-list")));

        var document = new HtmlDocument();
        document.LoadHtml(html);

        return document.GetElementbyId("pjax-job-list").ChildNodes
            .TakeWhile(node => !node.HasClass("text-muted"))
            .Where(node => node.HasClass("job-link"))
            .Select(node =>
            {
                var companyAndCityNode = node.ChildNodes.First(n => n.HasClass("flex"));
                var titleNode = node.ChildNodes.First(n => n.OriginalName == "h2").ChildNodes[1];
                return new
                {
                    Title = titleNode.InnerText,
                    CompanyName = companyAndCityNode.ChildNodes[1].InnerText,
                    City = companyAndCityNode.ChildNodes[3].InnerText,
                    Link = titleNode.Attributes.First(attr => attr.Name == "href").Value
                };
            })
            .Select(v => new GetVacancyResponse(v.Title, v.CompanyName, string.Empty, string.Empty, host + v.Link));
    }
}