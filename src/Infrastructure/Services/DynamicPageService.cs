using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Infrastructure.Services;

public class DynamicPageService : IDynamicPageService
{
    private readonly IConfiguration _configuration;

    public DynamicPageService(IConfiguration configuration) => _configuration = configuration;

    public string GetHtml(string uri, Func<IWebDriver, IWebElement> waitUntil)
    {
        var binaryLocation = _configuration.GetSection("ChromeExecutablePath").Value;
        var chromeDriverDirectory = _configuration.GetSection("ChromeDriverDirectory").Value;

        using var driver = new ChromeDriver(chromeDriverDirectory, new ChromeOptions {BinaryLocation = binaryLocation});

        driver.Navigate().GoToUrl(uri);
        
        //var waitForElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        //waitForElement.Until(waitUntil);

        return driver.PageSource;
    }
}
