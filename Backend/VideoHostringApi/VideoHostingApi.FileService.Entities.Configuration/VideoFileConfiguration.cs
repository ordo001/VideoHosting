using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VideoHostingApi.FileService.Entities.Configuration;

public class VideoFileConfiguration : IFileServiceEntityConfiguration, IEntityTypeConfiguration<VideoFile>
{
    public void Configure(EntityTypeBuilder<VideoFile> builder)
    {
        builder.ToTable("VideoFiles");
        
        builder.HasKey(p => p.Id);
    }
}