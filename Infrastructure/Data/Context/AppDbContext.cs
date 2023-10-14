using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AnonUserConfig());
        modelBuilder.ApplyConfiguration(new GameConfig());

    }


    public DbSet<AnonUserEntity> AnonUserEntity { get; set; }
    public DbSet<GameEntity> GameEntity { get; set; }



}