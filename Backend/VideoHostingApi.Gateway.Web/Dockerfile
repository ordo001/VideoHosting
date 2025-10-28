FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VideoHostingApi.Gateway.Web/VideoHostingApi.Gateway.Web.csproj", "VideoHostingApi.Gateway.Web/"]
RUN dotnet restore "VideoHostingApi.Gateway.Web/VideoHostingApi.Gateway.Web.csproj"
COPY . .
WORKDIR "/src/VideoHostingApi.Gateway.Web"
RUN dotnet build "./VideoHostingApi.Gateway.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VideoHostingApi.Gateway.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoHostingApi.Gateway.Web.dll"]
