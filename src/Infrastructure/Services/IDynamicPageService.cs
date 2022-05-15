using OpenQA.Selenium;

namespace Infrastructure.Services;

public interface IDynamicPageService
{
    string GetHtml(string uri, Func<IWebDriver, IWebElement> waitUntil);
}
