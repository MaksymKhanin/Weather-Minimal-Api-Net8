
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /source
COPY WeatherMinimalApi.sln ./
COPY ./Api/Api.csproj ./Api/
COPY ./Business/Business.csproj ./Business/
COPY ./Persistence/Persistence.csproj ./Persistence/
COPY ./Core/Core.csproj ./Core/
COPY ./Tests/Business.Tests/Business.Tests.csproj ./Tests/Business.Tests/

RUN dotnet restore
COPY . .
WORKDIR /source/Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /publish
WORKDIR /source/Business
RUN dotnet build -c $BUILD_CONFIGURATION -o /publish
WORKDIR /source/Persistence
RUN dotnet build -c $BUILD_CONFIGURATION -o /publish
WORKDIR /source/Core
RUN dotnet build -c $BUILD_CONFIGURATION -o /publish

WORKDIR /source
RUN dotnet publish WeatherMinimalApi.sln -c $BUILD_CONFIGURATION -o /publish
RUN dotnet test Tests/Business.Tests

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /publish
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Api.dll"]
