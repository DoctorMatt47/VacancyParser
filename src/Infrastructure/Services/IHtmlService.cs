namespace Infrastructure.Services;

public interface IHtmlService
{
    Task<string> Get(string uri);
}