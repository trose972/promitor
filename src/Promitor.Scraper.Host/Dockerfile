FROM microsoft/dotnet:2.1.500-sdk AS build
WORKDIR /src
COPY Promitor.Core/* Promitor.Core/
COPY Promitor.Core.Scraping/* Promitor.Core.Scraping/
COPY Promitor.Core.Telemetry/* Promitor.Core.Telemetry/
COPY Promitor.Integrations.AzureMonitor/* Promitor.Integrations.AzureMonitor/
COPY Promitor.Scraper.Host/* Promitor.Scraper.Host/
RUN dotnet --info
RUN dotnet publish Promitor.Scraper.Host/Promitor.Scraper.Host.csproj --configuration release -o app

FROM microsoft/dotnet:2.1.6-aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=build /src/Promitor.Scraper.Host/app .

ENTRYPOINT ["dotnet", "Promitor.Scraper.Host.dll"]