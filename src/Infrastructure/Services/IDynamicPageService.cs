using OpenQA.Selenium;

namespace VacancyParser.Infrastructure.Services;

public interface IDynamicPageService
{
    string GetHtml(string uri, Func<IWebDriver, IWebElement> waitUntil);
}