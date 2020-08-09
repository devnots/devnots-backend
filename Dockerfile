
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet publish DevNots.sln -c Release -o build --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime

WORKDIR /app

COPY --from=build /app/build .

ENTRYPOINT ["dotnet", "DevNots.RestApi.dll"]
