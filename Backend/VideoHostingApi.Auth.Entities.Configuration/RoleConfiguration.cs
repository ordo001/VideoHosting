using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VideoHostingApi.Auth.Entities.Configuration;

/// <summary>
/// Конфигурация <see cref="Role"/>
/// </summary>
public class RoleConfiguration : IAuthEntityConfiguration, IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(r => r.Id);
        
    }
}