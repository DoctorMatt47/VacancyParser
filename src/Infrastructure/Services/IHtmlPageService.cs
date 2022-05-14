namespace Infrastructure.Services;

public interface IHtmlPageService
{
    Task<string> Get(string uri);
}