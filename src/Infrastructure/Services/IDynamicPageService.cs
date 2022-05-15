using OpenQA.Selenium;

namespace Infrastructure.Services;

public interface IDynamicPageService
{
    Task<string> GetHtml(string uri, Func<IWebDriver, IWebElement> waitUntil);
}
