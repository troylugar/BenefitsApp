# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY src/* .
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet dev-certs https

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BenefitsApp.dll"]