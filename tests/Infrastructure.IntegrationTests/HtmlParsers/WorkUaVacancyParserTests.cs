using VacancyParser.Application.Vacancies;
using VacancyParser.Infrastructure.HtmlParsers;

namespace VacancyParser.Infrastructure.IntegrationTests.HtmlParsers;

public class WorkUaVacancyParserTests
{
    private readonly IEnumerable<IVacancyParser> _parsers;

    public WorkUaVacancyParserTests(IEnumerable<IVacancyParser> parsers) => _parsers = parsers;

    [Fact]
    public void GetParsedVacancies()
    {
        var parser = _parsers.First(p => p is WorkUaVacancyParser);

        var vacancies = parser.Parse(new GetVacanciesRequest(".net", "киев"));

        Assert.All(vacancies, vacancy =>
        {
            var (title, companyName, _, _, _) = vacancy;

            Assert.NotEqual(title, string.Empty);
            Assert.NotEqual(companyName, string.Empty);
        });
    }
}
