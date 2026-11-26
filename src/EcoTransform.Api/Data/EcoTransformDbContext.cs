using EcoTransform.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoTransform.Api.Data;

public class EcoTransformDbContext : DbContext
{
    public EcoTransformDbContext(DbContextOptions<EcoTransformDbContext> options)
        : base(options)
    {
    }

    // Tabla de proveedores de material reciclable.
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la tabla Proveedores.
        modelBuilder.Entity<Proveedor>(entidad =>
        {
            entidad.ToTable("Proveedores");
            entidad.HasKey(p => p.Id);
            entidad.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            entidad.Property(p => p.Identificacion).IsRequired().HasMaxLength(20);
            entidad.Property(p => p.Telefono).HasMaxLength(20);
            entidad.Property(p => p.Correo).HasMaxLength(150);
            entidad.Property(p => p.Direccion).HasMaxLength(250);

            // No se permiten dos proveedores con la misma identificación.
            entidad.HasIndex(p => p.Identificacion).IsUnique();
        });
    }
}