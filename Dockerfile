FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY src/MyUserWebApi.Application/*.csproj ./src/MyUserWebApi.Application/
COPY src/MyUserWebApi.CrossCutting/*.csproj ./src/MyUserWebApi.CrossCutting/
COPY src/MyUserWebApi.Data/*.csproj ./src/MyUserWebApi.Data/
COPY src/MyUserWebApi.Domain/*.csproj ./src/MyUserWebApi.Domain/
COPY src/MyUserWebApi.Service/*.csproj ./src/MyUserWebApi.Service/

RUN dotnet restore src/MyUserWebApi.Application/Application.csproj

COPY src ./src

RUN dotnet publish ./src/MyUserWebApi.Application/Application.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /publish ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Application.dll"]