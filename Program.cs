/*
David Araujo
Practical test for NOBEL.
Date: 14/10/2023
*/

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGameService, GameService>();

#region CONFIGURATION
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                            .Build();

var connectionString = configuration.GetConnectionString("MySqlConnection");
ServerExtension.ConfigureSQLServices(builder, connectionString);
OpenApiExtension.ConfigureOpenApi(builder);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();
app.UseSwagger();
app.UseCors("DevelopmentPolicy");
#endregion

#region API CONTROLLERS
var baseController = new APIControllerBase();
string[] choices = { "ROCK", "PAPER", "SCISSORS", "RESET", "STATISTICS" };
var infoMessage = "Use the options: 'Rock, Paper, Scissors' to start a game. Use 'Statistics' to see users statistics. Use 'Reset' to create a new user.";

app.MapPost("Game/PlayGame", [AllowAnonymous] async (IGameService service, string userSelection) =>
{
    var userUpp = userSelection.ToUpper();
    var responseCode = ResponseCode.OK;

    if (!choices.Contains(userUpp))
    {
        return baseController.Response(infoMessage, responseCode);
    }
    if (userUpp == "RESET")
    {
        service.ResetUser();
        return baseController.Response("New session ID generated", responseCode);
    }
    if (userUpp == "STATISTICS")
    {
        var statistics = await service.GetUsersStatistics();
        return baseController.Response(statistics, responseCode);
    }

    var response = await service.PlayGame(userSelection.ToUpper());
    return baseController.Response(response, responseCode);

}).WithTags("Game");

#endregion

app.MigrateDatabase();
app.MapSwagger();
app.UseSwaggerUI();
app.Run();
