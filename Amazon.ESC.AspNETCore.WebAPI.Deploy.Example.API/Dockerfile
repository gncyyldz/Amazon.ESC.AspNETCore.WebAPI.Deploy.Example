FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app/publish
COPY --from=build /app/publish ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Amazon.ESC.AspNETCore.WebAPI.Deploy.Example.API.dll"]