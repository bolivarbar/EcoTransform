namespace EcoTransform.Api.Entities;

/// <summary>
/// Tipo de material del catálogo (por ejemplo: PET, HDPE, caucho granulado, residuo de cocina).
/// Define los criterios con los que se acepta o rechaza un lote.
/// </summary>
public class TipoMaterial
{
    public int Id { get; set; }

    // Nombre del tipo de material.
    public string Nombre { get; set; } = string.Empty;

    // Ruta de transformación a la que pertenece (plástico, caucho u orgánico).
    public RutaMaterial Ruta { get; set; }

    public string? Descripcion { get; set; }

    // Pureza mínima (en porcentaje) para aceptar un lote de este material.
    public decimal PurezaMinima { get; set; }

    // Contaminación máxima (en porcentaje) tolerada para aceptar un lote.
    public decimal ContaminacionMaxima { get; set; }

    // Indica si el tipo de material está disponible en el catálogo.
    public bool Activo { get; set; } = true;
}