# Use the official .NET 7 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project files to the container
COPY RockPaperScissorsDA/RockPaperScissorsDA.csproj RockPaperScissorsDA/

# Restore NuGet packages and build the project
RUN dotnet restore

# Publish the application for release
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 7 runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .


ENV Logging__LogLevel__Default=Information
ENV Logging__LogLevel__Microsoft.AspNetCore=Warning
ENV AllowedHosts=*
ENV ConnectionStrings__MySqlConnection="workstation id=NobelTest.mssql.somee.com;packet size=4096;user id=dskato1219_SQLLogin_1;pwd=acyn4zmwvu;data source=NobelTest.mssql.somee.com;persist security info=False;initial catalog=NobelTest"

# Expose port 80 for the application
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "RockPaperScissorsDA.dll"]
