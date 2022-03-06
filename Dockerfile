FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS build
WORKDIR /dummy
COPY ./dummy
ENTRYPOINT ["dotnet", "WebApp.dll"]