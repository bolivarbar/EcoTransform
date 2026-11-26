using EcoTransform.Api.Data;
using EcoTransform.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTransform.Api.Controllers;

[ApiController]
[Route("api/tipos-material")]
public class TiposMaterialController : ControllerBase
{
    private readonly EcoTransformDbContext _contexto;

    public TiposMaterialController(EcoTransformDbContext contexto)
    {
        _contexto = contexto;
    }

    // Lista los tipos de material. Opcionalmente se filtra por ruta (1 = plástico, 2 = caucho, 3 = orgánico).
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoMaterial>>> Listar([FromQuery] RutaMaterial? ruta)
    {
        var consulta = _contexto.TiposMaterial.AsNoTracking();

        if (ruta.HasValue)
        {
            consulta = consulta.Where(t => t.Ruta == ruta.Value);
        }

        var tipos = await consulta.OrderBy(t => t.Ruta).ThenBy(t => t.Nombre).ToListAsync();

        return Ok(tipos);
    }

    // Consulta un tipo de material por su Id.
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoMaterial>> ObtenerPorId(int id)
    {
        var tipo = await _contexto.TiposMaterial.FindAsync(id);

        if (tipo is null)
        {
            return NotFound();
        }

        return Ok(tipo);
    }

    // Registra un nuevo tipo de material en el catálogo.
    [HttpPost]
    public async Task<ActionResult<TipoMaterial>> Crear(TipoMaterial tipo)
    {
        // Valida que los porcentajes estén entre 0 y 100.
        if (tipo.PurezaMinima < 0 || tipo.PurezaMinima > 100 ||
            tipo.ContaminacionMaxima < 0 || tipo.ContaminacionMaxima > 100)
        {
            return BadRequest("La pureza y la contaminación deben estar entre 0 y 100.");
        }

        // Verifica que el nombre no esté repetido.
        var existe = await _contexto.TiposMaterial.AnyAsync(t => t.Nombre == tipo.Nombre);

        if (existe)
        {
            return Conflict("Ya existe un tipo de material con ese nombre.");
        }

        // El Id lo asigna la base de datos.
        tipo.Id = 0;

        _contexto.TiposMaterial.Add(tipo);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = tipo.Id }, tipo);
    }

    // Actualiza un tipo de material existente.
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, TipoMaterial datos)
    {
        var tipo = await _contexto.TiposMaterial.FindAsync(id);

        if (tipo is null)
        {
            return NotFound();
        }

        // Valida que los porcentajes estén entre 0 y 100.
        if (datos.PurezaMinima < 0 || datos.PurezaMinima > 100 ||
            datos.ContaminacionMaxima < 0 || datos.ContaminacionMaxima > 100)
        {
            return BadRequest("La pureza y la contaminación deben estar entre 0 y 100.");
        }

        // Verifica que el nombre no pertenezca a otro tipo de material.
        var repetido = await _contexto.TiposMaterial
            .AnyAsync(t => t.Nombre == datos.Nombre && t.Id != id);

        if (repetido)
        {
            return Conflict("Ya existe otro tipo de material con ese nombre.");
        }

        tipo.Nombre = datos.Nombre;
        tipo.Ruta = datos.Ruta;
        tipo.Descripcion = datos.Descripcion;
        tipo.PurezaMinima = datos.PurezaMinima;
        tipo.ContaminacionMaxima = datos.ContaminacionMaxima;
        tipo.Activo = datos.Activo;

        await _contexto.SaveChangesAsync();

        return NoContent();
    }

    // Desactiva un tipo de material (baja lógica, no se elimina el registro).
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var tipo = await _contexto.TiposMaterial.FindAsync(id);

        if (tipo is null)
        {
            return NotFound();
        }

        tipo.Activo = false;
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}