using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AnonUserConfig : IEntityTypeConfiguration<AnonUserEntity>
{


    public void Configure(EntityTypeBuilder<AnonUserEntity> builder)
    {
        builder.ToTable("anon_user");
        builder.HasKey(x => x.AnonUserId);
        builder.HasMany(p => p.GameEntities).WithOne(p => p.AnonUserEntity).OnDelete(DeleteBehavior.Cascade);
 
    }
}