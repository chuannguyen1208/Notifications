#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Tools/Tools.csproj", "src/Tools/"]

COPY ["src/OAuthService/OAuthService/OAuthService.csproj", "src/OAuthService/OAuthService/"]
COPY ["src/OAuthService/OAuthService.Client/OAuthService.Client.csproj", "src/OAuthService/OAuthService.Client/"]

RUN dotnet restore "src/OAuthService/OAuthService/OAuthService.csproj"

COPY . .

WORKDIR "/src/src/OAuthService/OAuthService"

RUN dotnet build "./OAuthService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OAuthService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OAuthService.dll"]