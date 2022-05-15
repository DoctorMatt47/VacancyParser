using Application.Vacancies;
using Infrastructure.HtmlParsers;

namespace Infrastructure.IntegrationTests.HtmlParsers;

public class RabotaUaVacancyParserTests
{
    private readonly IEnumerable<IVacancyParser> _parsers;

    public RabotaUaVacancyParserTests(IEnumerable<IVacancyParser> parsers) => _parsers = parsers;

    [Fact]
    public void GetParsedVacancies()
    {
        var parser = _parsers.First(p => p is RabotaUaVacancyParser);

        var vacancies = parser.Get(new GetVacanciesRequest(".net", "киев"));

        Assert.All(vacancies, vacancy =>
        {
            var (title, companyName, _, _, _) = vacancy;

            Assert.NotEqual(title, string.Empty);
            Assert.NotEqual(companyName, string.Empty);
        });
    }
}