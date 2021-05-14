#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApplication15/Server/WebApplication15.Server.csproj", "WebApplication15/Server/"]
COPY ["WebApplication15/Shared/WebApplication15.Shared.csproj", "WebApplication15/Shared/"]
COPY ["WebApplication15/Client/WebApplication15.Client.csproj", "WebApplication15/Client/"]
RUN dotnet restore "WebApplication15/Server/WebApplication15.Server.csproj"
COPY . .
WORKDIR "/src/WebApplication15/Server"
RUN dotnet build "WebApplication15.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication15.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication15.Server.dll"]
# ASPNETCORE_URLS=http://*:$PORT dotnet WebApplication15.Server.dll