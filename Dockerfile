FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy csproj and restore dependencies
COPY *.sln .
COPY MONKEY5_API/*.csproj ./MONKEY5_API/
COPY BusinessObjects/*.csproj ./BusinessObjects/
COPY DataAccessObjects/*.csproj ./DataAccessObjects/
COPY Repositories/*.csproj ./Repositories/
COPY Services/*.csproj ./Services/
RUN dotnet restore

# Copy all files and build
COPY . .
# Specify the startup project to publish
RUN dotnet publish "MONKEY5_API/MONKEY5_API.csproj" -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MONKEY5_API.dll"]
