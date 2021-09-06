FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EnergyAccountManagement.Api/EnergyAccountManagement.Api.csproj", "EnergyAccountManagement.Api/"]
RUN dotnet restore "EnergyAccountManagement.Api/EnergyAccountManagement.Api.csproj"
COPY . .
WORKDIR "/src/EnergyAccountManagement.Api"
RUN dotnet build "EnergyAccountManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnergyAccountManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnergyAccountManagement.Api.dll"]