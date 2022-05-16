using VacancyParser.Application.Extensions;
using VacancyParser.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseExceptionHandler("/error")
    .UseHttpsRedirection();

app.MapControllers();

app.Run();
