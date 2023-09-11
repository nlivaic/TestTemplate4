#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
# Uncomment if you have other certs this services needs to trust.
#COPY nginx/id-local.crt /usr/local/share/ca-certificates/id-local.crt
#RUN update-ca-certificates
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TestTemplate4.Api/TestTemplate4.Api.csproj", "src/TestTemplate4.Api/"]
COPY ["src/TestTemplate4.Data/TestTemplate4.Data.csproj", "src/TestTemplate4.Data/"]
COPY ["src/TestTemplate4.Common/TestTemplate4.Common.csproj", "src/TestTemplate4.Common/"]
COPY ["src/TestTemplate4.Core/TestTemplate4.Core.csproj", "src/TestTemplate4.Core/"]
RUN dotnet restore "src/TestTemplate4.Api/TestTemplate4.Api.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate4.Api"
RUN dotnet build "TestTemplate4.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate4.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTemplate4.Api.dll"]