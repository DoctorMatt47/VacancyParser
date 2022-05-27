using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace VacancyParser.Infrastructure.Services;

public class DynamicPageService : IDynamicPageService
{
    private readonly IConfiguration _configuration;

    public DynamicPageService(IConfiguration configuration) => _configuration = configuration;

    public string GetHtml(string uri, Func<IWebDriver, IWebElement> waitUntil)
    {
        var binaryLocation = _configuration.GetSection("ChromeExecutablePath").Value;
        var chromeDriverDirectory = _configuration.GetSection("ChromeDriverDirectory").Value;
        var chromeOptions = new ChromeOptions {BinaryLocation = binaryLocation};

        using var driver = new ChromeDriver(chromeDriverDirectory, chromeOptions);

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        driver.Navigate().GoToUrl(uri);

        wait.Until(waitUntil);

        return driver.PageSource;
    }
}
