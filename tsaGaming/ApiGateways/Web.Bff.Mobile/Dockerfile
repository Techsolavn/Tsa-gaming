FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5048
EXPOSE 7117

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiGateways/Web.Bff.Mobile/Web.Bff.Mobile.csproj", "ApiGateways/Web.Bff.Mobile/"]
RUN dotnet restore "ApiGateways/Web.Bff.Mobile/Web.Bff.Mobile.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Web.Bff.Mobile"
RUN dotnet build "Web.Bff.Mobile.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Bff.Mobile.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Bff.Mobile.dll"]