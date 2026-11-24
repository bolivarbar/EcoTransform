namespace EcoTransform.Api.Entities;

/// <summary>
/// Proveedor que entrega material reciclable (plástico, caucho de neumáticos u orgánicos).
/// </summary>
public class Proveedor
{
    public int Id { get; set; }

    // Nombre o razón social del proveedor.
    public string Nombre { get; set; } = string.Empty;

    // Cédula o RUC del proveedor.
    public string Identificacion { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    // Indica si el proveedor está habilitado para registrar entregas.
    public bool Activo { get; set; } = true;

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}