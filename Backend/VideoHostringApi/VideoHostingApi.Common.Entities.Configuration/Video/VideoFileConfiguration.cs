using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHostingApi.Common.Entities.Video;

namespace VideoHostingApi.Common.Entities.Configuration.Video;

public class VideoFileConfiguration : IFileServiceEntityConfiguration, IEntityTypeConfiguration<VideoFile>
{
    public void Configure(EntityTypeBuilder<VideoFile> builder)
    {
        builder.ToTable("VideoFiles");
        
        builder.HasKey(p => p.Id);
        
        builder.HasOne(x => x.Video)
            .WithMany()
            .HasForeignKey(x => x.VideoId);
    }
}