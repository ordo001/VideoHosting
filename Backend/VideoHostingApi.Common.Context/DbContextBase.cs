using Microsoft.EntityFrameworkCore;

namespace VideoHostingApi.Common.Context;

public abstract class DbContextBase<TConfigurationAnchor>(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TConfigurationAnchor).Assembly);
    }
}