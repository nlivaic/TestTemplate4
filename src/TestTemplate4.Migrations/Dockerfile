#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TestTemplate4.Migrations/TestTemplate4.Migrations.csproj", "src/TestTemplate4.Migrations/"]
RUN dotnet restore "src/TestTemplate4.Migrations/TestTemplate4.Migrations.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate4.Migrations"
RUN dotnet build "TestTemplate4.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate4.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTemplate4.Migrations.dll"]