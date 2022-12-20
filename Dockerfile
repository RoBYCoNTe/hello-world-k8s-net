# Create .NET 7 image with the latest SDK for my HelloWorldApp

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image for aspnet
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Ensure that volume path is accessible
# create /app/data folder and give it full access
RUN mkdir /app/data
RUN chmod -R 777 /app/data

ENTRYPOINT ["dotnet", "HelloWorldApp.dll"]