FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ClientDashboardAPI.csproj", "./"]
RUN dotnet restore "ClientDashboardAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ClientDashboardAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientDashboardAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientDashboardAPI.dll"]
