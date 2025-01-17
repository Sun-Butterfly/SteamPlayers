using Microsoft.EntityFrameworkCore;

namespace Achievements;

public class AchivDataBaseContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; } = null!;

    public AchivDataBaseContext(DbContextOptions<AchivDataBaseContext> options) : base(options)
    {
        
    }
}