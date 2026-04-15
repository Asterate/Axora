FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY *.sln ./
COPY WebApp/WebApp.csproj WebApp/
COPY App.Domain/App.Domain.csproj App.Domain/
COPY App.DAL.EF/App.DAL.EF.csproj App.DAL.EF/
COPY App.BLL/App.BLL.csproj App.BLL/
COPY App.DTO/App.DTO.csproj App.DTO/
COPY App.Resources/App.Resources.csproj App.Resources/
RUN dotnet restore

COPY . .
RUN dotnet publish WebApp/WebApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Axora.dll"]