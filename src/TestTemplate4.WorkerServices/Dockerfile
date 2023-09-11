#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TestTemplate4.WorkerServices/TestTemplate4.WorkerServices.csproj", "src/TestTemplate4.WorkerServices/"]
COPY ["src/TestTemplate4.Core/TestTemplate4.Core.csproj", "src/TestTemplate4.Core/"]
COPY ["src/TestTemplate4.Common/TestTemplate4.Common.csproj", "src/TestTemplate4.Common/"]
RUN dotnet restore "src/TestTemplate4.WorkerServices/TestTemplate4.WorkerServices.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate4.WorkerServices"
RUN dotnet build "TestTemplate4.WorkerServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate4.WorkerServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/TestTemplate4.WorkerServices/entrypoint.sh", "src/entrypoint.sh"]
COPY ["src/TestTemplate4.WorkerServices/wait-for-it.sh", "src/wait-for-it.sh"]
CMD ["/bin/bash", "src/entrypoint.sh"]
