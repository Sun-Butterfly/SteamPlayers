namespace Players;

public class Player
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public List<long> GameIds { get; set; } = null!;
    public List<long>? GetedAchievementIds { get; set; }
}