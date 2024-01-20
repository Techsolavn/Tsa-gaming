#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiGateways/Web.Bff.AdminPortal.Ocelot/Web.Bff.AdminPortal.Ocelot.csproj", "ApiGateways/Web.Bff.AdminPortal.Ocelot/"]
RUN dotnet restore "ApiGateways/Web.Bff.AdminPortal.Ocelot/Web.Bff.AdminPortal.Ocelot.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Web.Bff.AdminPortal.Ocelot"
RUN dotnet build "Web.Bff.AdminPortal.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Bff.AdminPortal.Ocelot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Bff.AdminPortal.Ocelot.dll"]