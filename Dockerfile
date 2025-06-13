FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GlicareApp.Api/GlicareApp.Api.csproj", "GlicareApp.Api/"]
COPY ["GlicareApp.CrossCuting/GlicareApp.CrossCuting.csproj", "GlicareApp.CrossCuting/"]
COPY ["GlicareApp.ExternalServices/GlicareApp.ExternalServices.csproj", "GlicareApp.ExternalServices/"]
COPY ["GlicareApp.Repositories/GlicareApp.Repositories.csproj", "GlicareApp.Repositories/"]
COPY ["GlicareApp.Services/GlicareApp.Services.csproj", "GlicareApp.Services/"]
COPY ["GlicareApp.Domain/GlicareApp.Domain.csproj", "GlicareApp.Domain/"]
RUN dotnet restore "./GlicareApp.Api/GlicareApp.Api.csproj"
COPY . .
WORKDIR "/src/GlicareApp.Api"
RUN dotnet build "./GlicareApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GlicareApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlicareApp.Api.dll"]