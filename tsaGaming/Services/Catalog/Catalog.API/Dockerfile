#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Product/Catalog.API/Catalog.API.csproj", "Services/Product/Catalog.API/"]
COPY ["Services/Common/Services.Common/Services.Common.csproj", "Services/Common/Services.Common/"]
COPY ["Services/Product/Catalog.Domain/Catalog.Domain.csproj", "Services/Product/Catalog.Domain/"]
COPY ["Services/Product/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Services/Product/Catalog.Infrastructure/"]
RUN dotnet restore "Services/Product/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Product/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]