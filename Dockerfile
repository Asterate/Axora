FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY Proposal/contact-saas-main1/contact-saas-main/*.sln ./
COPY Proposal/contact-saas-main1/contact-saas-main/WebApp/WebApp.csproj WebApp/
COPY Proposal/contact-saas-main1/contact-saas-main/App.DTO/App.DTO.csproj App.DTO/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Helpers/App.Helpers.csproj App.Helpers/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Modules.Audit/App.Modules.Audit.csproj App.Modules.Audit/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Modules.Identity/App.Modules.Identity.csproj App.Modules.Identity/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Modules.Lab/App.Modules.Lab.csproj App.Modules.Lab/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Modules.Research/App.Modules.Research.csproj App.Modules.Research/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Resources/App.Resources.csproj App.Resources/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Shared/App.Shared.csproj App.Shared/
COPY Proposal/contact-saas-main1/contact-saas-main/Base.Resources/Base.Resources.csproj Base.Resources/
COPY Proposal/contact-saas-main1/contact-saas-main/WebApp.Tests/WebApp.Tests.csproj WebApp.Tests/
RUN dotnet restore
COPY Proposal/contact-saas-main1/contact-saas-main/ .
RUN dotnet publish WebApp/WebApp.csproj -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Axora.dll"]