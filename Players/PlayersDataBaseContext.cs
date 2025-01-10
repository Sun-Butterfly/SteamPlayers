using Microsoft.EntityFrameworkCore;

namespace Players;

public class PlayersDataBaseContext : DbContext
{
    public DbSet<Player> Players { get; set; } = null!;

    public PlayersDataBaseContext(DbContextOptions<PlayersDataBaseContext> options) : base(options)
    {

    }
}