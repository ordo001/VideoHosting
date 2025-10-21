FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VideoHostingApi.Auth.Web/VideoHostingApi.Auth.Web.csproj", "VideoHostingApi.Auth.Web/"]
COPY ["VideoHostingApi.Common.Entities/VideoHostingApi.Common.Entities.csproj", "VideoHostingApi.Common.Entities/"]
COPY ["VideoHostingApi.Common.Context/VideoHostingApi.Common.Context.csproj", "VideoHostingApi.Common.Context/"]
COPY ["VideoHostingApi.Auth.Entities.Configuration/VideoHostingApi.Auth.Entities.Configuration.csproj", "VideoHostingApi.Auth.Entities.Configuration/"]
COPY ["VideoHostingApi.Auth.Entities/VideoHostingApi.Auth.Entities.csproj", "VideoHostingApi.Auth.Entities/"]
COPY ["VideoHostingApi.Auth.Context/VideoHostingApi.Auth.Context.csproj", "VideoHostingApi.Auth.Context/"]
COPY ["VideoHostingApi.Common.Web/VideoHostingApi.Common.Web.csproj", "VideoHostingApi.Common.Web/"]
COPY ["VideoHostingApi.Auth.Repositories/VideoHostingApi.Auth.Repositories.csproj", "VideoHostingApi.Auth.Repositories/"]
COPY ["VideoHosting.Common.Repositories/VideoHosting.Common.Repositories.csproj", "VideoHosting.Common.Repositories/"]
COPY ["VideoHostingApi.Common.Repositories.Contracts/VideoHostingApi.Common.Repositories.Contracts.csproj", "VideoHostingApi.Common.Repositories.Contracts/"]
COPY ["VideoHosting.Auth.Repositories.Contracts/VideoHosting.Auth.Repositories.Contracts.csproj", "VideoHosting.Auth.Repositories.Contracts/"]
RUN dotnet restore "VideoHostingApi.Auth.Web/VideoHostingApi.Auth.Web.csproj"
COPY . .
WORKDIR "/src/VideoHostingApi.Auth.Web"
RUN dotnet build "./VideoHostingApi.Auth.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VideoHostingApi.Auth.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoHostingApi.Auth.Web.dll"]
