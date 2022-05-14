using OpenQA.Selenium.Chrome;

namespace Infrastructure.Services;

public class HtmlPagePageService : IHtmlPageService
{
    private readonly HttpClient _client;

    public HtmlPagePageService(HttpClient client) => _client = client;

    public Task<string> Get(string uri)
    {
        var options = new ChromeOptions {BinaryLocation = @"c:\Program Files\Google\Chrome\Application\chrome.exe"};
        using var driver = new ChromeDriver(@"E:\Programs\ChromeDriver", options);

        driver.Navigate().GoToUrl(uri);
        Thread.Sleep(1000);
        return Task.FromResult(driver.PageSource);
    }
}
