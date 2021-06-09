#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/WebApplication15.Server.csproj", "Server/"]
COPY ["Shared/WebApplication15.Shared.csproj", "Shared/"]
COPY ["Client/WebApplication15.Client.csproj", "Client/"]
RUN dotnet restore "Server/WebApplication15.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "WebApplication15.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication15.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication15.Server.dll"]
# ASPNETCORE_URLS=http://*:$PORT dotnet WebApplication15.Server.dll