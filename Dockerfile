FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set environment variables
ENV Logging__LogLevel__Default=Information
ENV Logging__LogLevel__Microsoft.AspNetCore=Warning
ENV AllowedHosts=*
ENV ConnectionStrings__MySqlConnection="workstation id=NobelTest.mssql.somee.com;packet size=4096;user id=dskato1219_SQLLogin_1;pwd=acyn4zmwvu;data source=NobelTest.mssql.somee.com;persist security info=False;initial catalog=NobelTest"

# Expose port 80 for the application
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "RockPaperScissorsDA.dll"]











