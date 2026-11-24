using Microsoft.EntityFrameworkCore;

namespace EcoTransform.Api.Data;

public class EcoTransformDbContext : DbContext
{
    public EcoTransformDbContext(DbContextOptions<EcoTransformDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}