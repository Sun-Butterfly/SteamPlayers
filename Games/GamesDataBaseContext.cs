using Microsoft.EntityFrameworkCore;

namespace Games;

public class GamesDataBaseContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;

    public GamesDataBaseContext(DbContextOptions<GamesDataBaseContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .Property(x => x.AchievementIds)
            .HasDefaultValue(new List<long>());
    }
}