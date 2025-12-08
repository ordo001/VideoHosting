using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHostingApi.Common.Entities.Video;

namespace VideoHostingApi.Common.Entities.Configuration.Video;

/// <summary>
/// Конфигурация сущности <see cref="VideoFile"/>
/// </summary>
public class VideoConfiguration : IFileServiceEntityConfiguration, IEntityTypeConfiguration<Entities.Video.Video>
{
    public void Configure(EntityTypeBuilder<Entities.Video.Video> builder)
    {
        builder.ToTable("Video");
        
        builder.HasKey(p => p.Id);
    }
}