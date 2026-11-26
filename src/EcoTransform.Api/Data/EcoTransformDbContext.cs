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

    // Catálogo de tipos de material (plástico, caucho y orgánicos).
    public DbSet<TipoMaterial> TiposMaterial => Set<TipoMaterial>();

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

        // Configuración de la tabla TiposMaterial.
        modelBuilder.Entity<TipoMaterial>(entidad =>
        {
            entidad.ToTable("TiposMaterial");
            entidad.HasKey(t => t.Id);
            entidad.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
            entidad.Property(t => t.Descripcion).HasMaxLength(250);
            entidad.Property(t => t.PurezaMinima).HasPrecision(5, 2);
            entidad.Property(t => t.ContaminacionMaxima).HasPrecision(5, 2);

            // No se permiten dos tipos de material con el mismo nombre.
            entidad.HasIndex(t => t.Nombre).IsUnique();

            // Datos iniciales: un tipo representativo por cada ruta de reciclaje.
            entidad.HasData(
                new TipoMaterial
                {
                    Id = 1,
                    Nombre = "PET (botellas de plástico)",
                    Ruta = RutaMaterial.Plastico,
                    Descripcion = "Plástico PET limpio, sin tapas ni etiquetas.",
                    PurezaMinima = 90m,
                    ContaminacionMaxima = 10m,
                    Activo = true
                },
                new TipoMaterial
                {
                    Id = 2,
                    Nombre = "Caucho de neumáticos usados",
                    Ruta = RutaMaterial.Caucho,
                    Descripcion = "Neumáticos fuera de uso, con acero y fibras por separar.",
                    PurezaMinima = 85m,
                    ContaminacionMaxima = 15m,
                    Activo = true
                },
                new TipoMaterial
                {
                    Id = 3,
                    Nombre = "Residuos orgánicos para compost",
                    Ruta = RutaMaterial.Organico,
                    Descripcion = "Restos de cocina y vegetales, sin plásticos ni vidrio.",
                    PurezaMinima = 95m,
                    ContaminacionMaxima = 5m,
                    Activo = true
                });
        });
    }
}