# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy and restore the project files
COPY RockPaperScissorsDA/RockPaperScissorsDA.csproj RockPaperScissorsDA/
RUN dotnet restore RockPaperScissorsDA/RockPaperScissorsDA.csproj

# Copy the entire project and publish
COPY . .
WORKDIR /app/RockPaperScissorsDA
RUN dotnet publish -c Release -o out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the published output and appsettings.json file
COPY --from=build /app/RockPaperScissorsDA/out .

# Set environment variables for configurations
ENV Logging__LogLevel__Default=Information
ENV Logging__LogLevel__Microsoft.AspNetCore=Warning
ENV AllowedHosts=*
ENV ConnectionStrings__MySqlConnection="workstation id=NobelTest.mssql.somee.com;packet size=4096;user id=dskato1219_SQLLogin_1;pwd=acyn4zmwvu;data source=NobelTest.mssql.somee.com;persist security info=False;initial catalog=NobelTest"

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "RockPaperScissorsDA.dll"]
