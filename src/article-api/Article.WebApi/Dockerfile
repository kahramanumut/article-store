FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS="http://*:5001"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/article-api/Article.WebApi/Article.WebApi.csproj", "src/article-api/Article.WebApi/"]
COPY ["src/article-api/Article.Application/Article.Application.csproj", "src/article-api/Article.Application/"]
COPY ["src/article-api/Article.Infrastructure/Article.Infrastructure.csproj", "src/article-api/Article.Infrastructure/"]
COPY ["src/article-api/Article.Domain/Article.Domain.csproj", "src/article-api/Article.Domain/"]
COPY ["src/shared/Shared.Api/Shared.Api.csproj", "src/shared/Shared.Api/"]
COPY ["src/shared/Shared.Domain/Shared.Domain.csproj", "src/shared/Shared.Domain/"]
COPY ["src/shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/shared/Shared.Infrastructure/"]

RUN dotnet restore "src/article-api/Article.WebApi/Article.WebApi.csproj"

COPY . .
WORKDIR "/src/src/article-api/Article.WebApi/"
RUN dotnet build "Article.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Article.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ISDOCKER=yes
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Article.WebApi.dll", "--environment=Development"]