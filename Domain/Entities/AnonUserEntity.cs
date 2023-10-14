public class AnonUserEntity : BaseEntity
{
    public string AnonUserId { get; set; }
    public int? TotalGamesPlayed { get; set; }
    public int? TotalWins { get; set; }
    public int? TotalLosses { get; set; }
    public int? TotalDraws { get; set; }

    public ICollection<GameEntity> GameEntities { get; set; } = new List<GameEntity>();

}