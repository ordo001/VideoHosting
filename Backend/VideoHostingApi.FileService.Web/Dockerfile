FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VideoHostingApi.FileService.Web/VideoHostingApi.FileService.Web.csproj", "VideoHostingApi.FileService.Web/"]
COPY ["VideoHostingApi.Common.Context/VideoHostingApi.Common.Context.csproj", "VideoHostingApi.Common.Context/"]
COPY ["VideoHosting.Entities.Configuration/VideoHosting.Entities.Configuration.csproj", "VideoHosting.Entities.Configuration/"]
COPY ["VideoHosting.FileService.Context/VideoHosting.FileService.Context.csproj", "VideoHosting.FileService.Context/"]
COPY ["VideoHostingApi.FileService.Repositories.Contracts/VideoHostingApi.FileService.Repositories.Contracts.csproj", "VideoHostingApi.FileService.Repositories.Contracts/"]
COPY ["VideoHosting.FileSerivce.Entities/VideoHosting.FileSerivce.Entities.csproj", "VideoHosting.FileSerivce.Entities/"]
COPY ["VideoHostingApi.FileService.Repositories/VideoHostingApi.FileService.Repositories.csproj", "VideoHostingApi.FileService.Repositories/"]
COPY ["VideoHostingApi.FileService.Service.Contracts/VideoHostingApi.FileService.Service.Contracts.csproj", "VideoHostingApi.FileService.Service.Contracts/"]
RUN dotnet restore "VideoHostingApi.FileService.Web/VideoHostingApi.FileService.Web.csproj"
COPY . .
WORKDIR "/src/VideoHostingApi.FileService.Web"
RUN dotnet build "./VideoHostingApi.FileService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VideoHostingApi.FileService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoHostingApi.FileService.Web.dll"]
