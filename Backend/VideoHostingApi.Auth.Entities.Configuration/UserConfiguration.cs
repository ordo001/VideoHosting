using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VideoHostingApi.Auth.Entities.Configuration;

/// <summary>
/// Конфигурация <see cref="User"/>
/// </summary>
public class UserConfiguration : IAuthEntityConfiguration, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
        
        builder.HasIndex(u => u.Login)
            .IsUnique();
    }
}