public interface IGameService
{

    Task<object> PlayGame(string userChoice);
    void ResetUser();
    Task<object> GetUsersStatistics();
}