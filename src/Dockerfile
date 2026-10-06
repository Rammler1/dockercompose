FROM mcr.microsoft.com/dotnet/sdk:10.0 AS Build
WORKDIR /src
COPY ["DockerComposeApi/DockerComposeApi.csproj", "DockerComposeApi/"]
RUN dotnet restore DockerComposeApi/DockerComposeApi.csproj

COPY . /src
RUN dotnet publish DockerComposeApi/DockerComposeApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS Final
WORKDIR /app
COPY --from=Build /app/publish .
ENTRYPOINT ["dotnet", "DockerComposeApi.dll"]
