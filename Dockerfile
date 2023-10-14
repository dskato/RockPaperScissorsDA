# Use the official .NET 7 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project files to the container
COPY RockPaperScissorsDA/RockPaperScissorsDA.csproj RockPaperScissorsDA/
COPY RockPaperScissorsDA/API/*.csproj RockPaperScissorsDA/API/
COPY RockPaperScissorsDA/Domain/*.csproj RockPaperScissorsDA/Domain/
COPY RockPaperScissorsDA/Infrastructure/*.csproj RockPaperScissorsDA/Infrastructure/
COPY RockPaperScissorsDA/Migrations/*.csproj RockPaperScissorsDA/Migrations/
COPY RockPaperScissorsDA/Properties/*.csproj RockPaperScissorsDA/Properties/

# Restore NuGet packages and build the project
RUN dotnet restore

# Copy the remaining source code to the container
COPY RockPaperScissorsDA ./RockPaperScissorsDA/

# Publish the application for release
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 7 runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Set environment variables
ENV Logging__LogLevel__Default=Information
ENV Logging__LogLevel__Microsoft.AspNetCore=Warning
ENV AllowedHosts=*
ENV ConnectionStrings__MySqlConnection="workstation id=NobelTest.mssql.somee.com;packet size=4096;user id=dskato1219_SQLLogin_1;pwd=acyn4zmwvu;data source=NobelTest.mssql.somee.com;persist security info=False;initial catalog=NobelTest"

# Expose port 80 for the application
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "RockPaperScissorsDA.dll"]
