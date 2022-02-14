FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS="http://*:5002"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/review-api/Review.WebApi/Review.WebApi.csproj", "src/review-api/Review.WebApi/"]
COPY ["src/review-api/Review.Application/Review.Application.csproj", "src/review-api/Review.Application/"]
COPY ["src/review-api/Review.Infrastructure/Review.Infrastructure.csproj", "src/review-api/Review.Infrastructure/"]
COPY ["src/review-api/Review.Domain/Review.Domain.csproj", "src/review-api/Review.Domain/"]
COPY ["src/shared/Shared.Api/Shared.Api.csproj", "src/shared/Shared.Api/"]
COPY ["src/shared/Shared.Domain/Shared.Domain.csproj", "src/shared/Shared.Domain/"]
COPY ["src/shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/shared/Shared.Infrastructure/"]

RUN dotnet restore "src/review-api/Review.WebApi/Review.WebApi.csproj"

COPY . .
WORKDIR "/src/src/review-api/Review.WebApi/"
RUN dotnet build "Review.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Review.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ISDOCKER=yes 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Review.WebApi.dll", "--environment=Development"]