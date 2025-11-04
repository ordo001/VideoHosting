using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHostingApi.FileService.Entities;

namespace VideoHostingApi.FileService.Entities.Configuration;

/// <summary>
/// Конфигурация сущности <see cref="Video"/>
/// </summary>
public class VideoConfiguration : IFileServiceEntityConfiguration, IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Video");
        
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Name).IsUnique();
    }
}