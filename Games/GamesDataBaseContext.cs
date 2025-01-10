using Microsoft.EntityFrameworkCore;

namespace Games;

public class GamesDataBaseContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;

    public GamesDataBaseContext(DbContextOptions<GamesDataBaseContext> options) : base(options)
    {
        
    }
}