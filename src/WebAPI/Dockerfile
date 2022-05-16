FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["src/WebAPI/WebAPI.csproj", "src/WebAPI/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
RUN dotnet restore "src/WebAPI/WebAPI.csproj"
COPY . .

WORKDIR "/src/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacancyParser.WebAPI.dll"]
