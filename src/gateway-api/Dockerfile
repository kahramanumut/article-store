FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5003

ENV ASPNETCORE_URLS="http://*:5003"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY Gateway.Api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /publish

FROM base AS final
WORKDIR /app
ENV ISDOCKER=yes 
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "Gateway.Api.dll","--environment=Development"]