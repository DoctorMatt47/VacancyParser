namespace VacancyParser.Domain.Entities;

public class Vacancy
{
    public Vacancy(
        string link,
        string title,
        string companyName,
        string salary,
        string city,
        string category)
    {
        Link = link;
        Title = title;
        CompanyName = companyName;
        Salary = salary;
        City = city;
        Category = category;
    }

    public string Link { get; protected set; }
    public string Title { get; protected set; }
    public string CompanyName { get; protected set; }
    public string Salary { get; protected set; }
    public string City { get; protected set; }
    public string Category { get; protected set; }
}
