#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Modules.Blog/Modules.Blog/Modules.Blog.csproj", "src/Modules.Blog/Modules.Blog/"]
COPY ["src/Modules.Shared/Modules.Shared.csproj", "src/Modules.Shared/"]
COPY ["src/Tools/Tools.csproj", "src/Tools/"]
COPY ["src/Modules.Blog/Modules.Blog.Client/Modules.Blog.Client.csproj", "src/Modules.Blog/Modules.Blog.Client/"]
COPY ["src/Modules.Blog/Modules.Blog.Shared/Modules.Blog.Shared.csproj", "src/Modules.Blog/Modules.Blog.Shared/"]
COPY ["src/Modules.Blog/Modules.Blog.UseCases/Modules.Blog.UseCases.csproj", "src/Modules.Blog/Modules.Blog.UseCases/"]
RUN dotnet restore "./src/Modules.Blog/Modules.Blog/./Modules.Blog.csproj"
COPY . .
WORKDIR "/src/src/Modules.Blog/Modules.Blog"
RUN dotnet build "./Modules.Blog.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Modules.Blog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modules.Blog.dll"]