namespace Games;

public class Game
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public List<long> AchievementIds { get; set; } = null!;
}