#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:1.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:1.1 AS build
WORKDIR /src
COPY ["LNCWebApp/LNCWebApp.csproj", "LNCWebApp/"]
COPY ["LNCLibrary/LNCLibrary.csproj", "LNCLibrary/"]
RUN dotnet restore "LNCWebApp/LNCWebApp.csproj"
COPY . .
WORKDIR "/src/LNCWebApp"
RUN dotnet build "LNCWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LNCWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LNCWebApp.dll"]