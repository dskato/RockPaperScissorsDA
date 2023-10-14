public class GameEntity : BaseEntity
{
    public int GameId { get; set; }
    public string UserChoice { get; set; }
    public string ComputerChoice { get; set; }
    public string Result { get; set; }

    public string AnonUserId { get; set; }
    public AnonUserEntity AnonUserEntity { get; set; }

}