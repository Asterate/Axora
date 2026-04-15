FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY Proposal/contact-saas-main1/contact-saas-main/*.sln ./
COPY Proposal/contact-saas-main1/contact-saas-main/WebApp/WebApp.csproj WebApp/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Domain/App.Domain.csproj App.Domain/
COPY Proposal/contact-saas-main1/contact-saas-main/App.DAL.EF/App.DAL.EF.csproj App.DAL.EF/
COPY Proposal/contact-saas-main1/contact-saas-main/App.BLL/App.BLL.csproj App.BLL/
COPY Proposal/contact-saas-main1/contact-saas-main/App.DTO/App.DTO.csproj App.DTO/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Resources/App.Resources.csproj App.Resources/
COPY Proposal/contact-saas-main1/contact-saas-main/App.Helpers/App.Helpers.csproj App.Helpers/
COPY Proposal/contact-saas-main1/contact-saas-main/Base.Resources/Base.Resources.csproj Base.Resources/
RUN dotnet restore

COPY Proposal/contact-saas-main1/contact-saas-main/ .
RUN dotnet publish WebApp/WebApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Axora.dll"]