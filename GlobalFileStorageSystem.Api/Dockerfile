FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY GlobalFileStorageSystem.Domain/GlobalFileStorageSystem.Domain.csproj ./GlobalFileStorageSystem.Domain/
COPY GlobalFileStorageSystem.Application/GlobalFileStorageSystem.Application.csproj ./GlobalFileStorageSystem.Application/
COPY GlobalFileStorageSystem.Infrastructure/GlobalFileStorageSystem.Infrastructure.csproj ./GlobalFileStorageSystem.Infrastructure/
COPY GlobalFileStorageSystem.Api/GlobalFileStorageSystem.Api.csproj ./GlobalFileStorageSystem.Api/

RUN dotnet restore ./GlobalFileStorageSystem.Api/GlobalFileStorageSystem.Api.csproj

COPY . .

RUN dotnet publish ./GlobalFileStorageSystem.Api/GlobalFileStorageSystem.Api.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "GlobalFileStorageSystem.Api.dll"]