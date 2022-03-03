FROM mcr.microsoft.com/dotnet/sdk:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS build
WORKDIR /src
COPY WebApp.csproj WebApp/
RUN dotnet restore WebApp/WebApp.csproj
COPY . .
WORKDIR /src
RUN ls
RUN dotnet build WebApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApp.dll"]