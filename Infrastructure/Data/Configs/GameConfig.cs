using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GameConfig : IEntityTypeConfiguration<GameEntity>
{


    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.ToTable("game");
        builder.HasKey(x => x.GameId);
        builder.HasOne(p => p.AnonUserEntity).WithMany(u => u.GameEntities).HasForeignKey(p => p.AnonUserId).OnDelete(DeleteBehavior.NoAction);
 
    }
}