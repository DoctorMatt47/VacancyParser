var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseExceptionHandler("/error")
    .UseHttpsRedirection();
    
app.MapControllers(); 

app.Run();
