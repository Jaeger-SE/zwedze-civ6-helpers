FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Zwedze.CivilizationHelper.Host.BlazorWasm/Zwedze.CivilizationHelper.Host.BlazorWasm.csproj", "src/Zwedze.CivilizationHelper.Host.BlazorWasm/"]
COPY ["frameworks/Zwedze.Framework.Blazor.Reactive/Zwedze.Framework.Blazor.Reactive.csproj", "frameworks/Zwedze.Framework.Blazor.Reactive/"]
COPY ["src/Zwedze.CivilizationHelper/Zwedze.CivilizationHelper.csproj", "src/Zwedze.CivilizationHelper/"]
RUN dotnet restore "src/Zwedze.CivilizationHelper.Host.BlazorWasm/Zwedze.CivilizationHelper.Host.BlazorWasm.csproj"
COPY . .
WORKDIR "/src/src/Zwedze.CivilizationHelper.Host.BlazorWasm"
RUN dotnet build "./Zwedze.CivilizationHelper.Host.BlazorWasm.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Zwedze.CivilizationHelper.Host.BlazorWasm.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zwedze.CivilizationHelper.Host.BlazorWasm.dll"]
