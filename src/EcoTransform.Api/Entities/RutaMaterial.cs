namespace EcoTransform.Api.Entities;

/// <summary>
/// Rutas de transformación que maneja la plataforma, una por cada tipo de residuo.
/// </summary>
public enum RutaMaterial
{
    // Plástico: clasificación, lavado, trituración y moldeo.
    Plastico = 1,

    // Caucho de neumáticos: trituración, separación de acero y fibras, prensado y curado.
    Caucho = 2,

    // Orgánicos: mezcla, compostaje por etapas, humedad y aireación.
    Organico = 3
}