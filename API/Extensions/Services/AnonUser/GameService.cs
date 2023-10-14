using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class GameService : IGameService
{

    private static string sessionId = Guid.NewGuid().ToString();
    private readonly AppDbContext _dbContext;

    public GameService(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public void ResetUser()
    {
        sessionId = Guid.NewGuid().ToString();
    }

    public async Task<object> PlayGame(string userChoice)
    {
        try
        {

            //Verify if session ID is saved, else save anon
            var qAnon = _dbContext.AnonUserEntity.Where(x => x.AnonUserId == sessionId).FirstOrDefault();
            if (qAnon == null)
            {
                AnonUserEntity anonUser = new AnonUserEntity();
                anonUser.AnonUserId = sessionId;
                _dbContext.AnonUserEntity.Add(anonUser);
            }

            //Save all and return results
            var gameEntity = InitRPSGame(userChoice);
            gameEntity.AnonUserId = sessionId;
            _dbContext.GameEntity.Add(gameEntity);
            _dbContext.SaveChanges();

            //Update user - game statistics
            UpdateGameStatistics(sessionId);

            return string.Concat("Machine selection: ", gameEntity.ComputerChoice, "  Your selection: ", gameEntity.UserChoice, "  Game Result: ", gameEntity.Result);
        }
        catch (Exception e)
        {
            return e;
        }

    }

    private void UpdateGameStatistics(string anonUserId)
    {

        var userGames = _dbContext.GameEntity.Where(x => x.AnonUserId == anonUserId).ToList();
        var anonUser = _dbContext.AnonUserEntity.Where(x => x.AnonUserId == anonUserId).FirstOrDefault();

        if (anonUser != null && userGames != null)
        {

            anonUser.TotalGamesPlayed = userGames.Count();
            anonUser.TotalWins = userGames.Where(x => x.Result == "Win" && x.AnonUserId == sessionId).Count();
            anonUser.TotalDraws = userGames.Where(x => x.Result == "Draw" && x.AnonUserId == sessionId).Count();
            anonUser.TotalLosses = userGames.Where(x => x.Result == "Loss" && x.AnonUserId == sessionId).Count();

            _dbContext.AnonUserEntity.Update(anonUser);
            _dbContext.SaveChanges();
        }

    }

    public async Task<object> GetUsersStatistics(){

        return await _dbContext.AnonUserEntity.Include(x => x.GameEntities).ToListAsync();
    } 

    private GameEntity InitRPSGame(string userChoice)
    {
        GameEntity gameEntity = new GameEntity();
        string[] choices = { "ROCK", "PAPER", "SCISSORS" };


        if (!choices.Contains(userChoice))
        {
            gameEntity.Result = "Invalid Choice";
            return gameEntity;
        }

        //Computer selection
        Random random = new Random();
        int computerChoiceIndex = random.Next(choices.Length);
        string computerChoice = choices[computerChoiceIndex];


        string result = DetermineGameResult(userChoice, computerChoice);
        gameEntity.UserChoice = userChoice;
        gameEntity.ComputerChoice = computerChoice;
        gameEntity.Result = result;

        return gameEntity;
    }


    private string DetermineGameResult(string userChoice, string computerChoice)
    {
        Dictionary<string, string> winningCombos = new Dictionary<string, string>
        {
            { "Rock", "Scissors" },
            { "Paper", "Rock" },
            { "Scissors", "Paper" }
        };

        if (userChoice == computerChoice)
        {
            return "Draw";
        }

        if (winningCombos.ContainsKey(userChoice) && winningCombos[userChoice] == computerChoice)
        {
            return "Win";
        }

        return "Loss";
    }

    public static string GenerateRandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder stringBuilder = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(characters.Length);
            stringBuilder.Append(characters[index]);
        }

        return stringBuilder.ToString();
    }
}