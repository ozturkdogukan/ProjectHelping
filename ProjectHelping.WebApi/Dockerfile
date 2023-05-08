#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProjectHelping.WebApi/ProjectHelping.WebApi.csproj", "ProjectHelping.WebApi/"]
COPY ["ProjectHelping.Business/ProjectHelping.Business.csproj", "ProjectHelping.Business/"]
COPY ["ProjectHelping.Data/ProjectHelping.Data.csproj", "ProjectHelping.Data/"]
COPY ["ProjectHelping.DataAccess/ProjectHelping.DataAccess.csproj", "ProjectHelping.DataAccess/"]
RUN dotnet restore "ProjectHelping.WebApi/ProjectHelping.WebApi.csproj"
COPY . .
WORKDIR "/src/ProjectHelping.WebApi"
RUN dotnet build "ProjectHelping.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectHelping.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectHelping.WebApi.dll"]