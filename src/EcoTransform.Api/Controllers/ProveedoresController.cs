using EcoTransform.Api.Data;
using EcoTransform.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTransform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly EcoTransformDbContext _contexto;

    public ProveedoresController(EcoTransformDbContext contexto)
    {
        _contexto = contexto;
    }

    // Lista todos los proveedores ordenados por nombre.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> Listar()
    {
        var proveedores = await _contexto.Proveedores
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .ToListAsync();

        return Ok(proveedores);
    }

    // Consulta un proveedor por su Id.
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Proveedor>> ObtenerPorId(int id)
    {
        var proveedor = await _contexto.Proveedores.FindAsync(id);

        if (proveedor is null)
        {
            return NotFound();
        }

        return Ok(proveedor);
    }

    // Registra un nuevo proveedor.
    [HttpPost]
    public async Task<ActionResult<Proveedor>> Crear(Proveedor proveedor)
    {
        // Verifica que la identificación no esté repetida.
        var existe = await _contexto.Proveedores
            .AnyAsync(p => p.Identificacion == proveedor.Identificacion);

        if (existe)
        {
            return Conflict("Ya existe un proveedor con esa identificación.");
        }

        // El Id y la fecha de registro los asigna el sistema.
        proveedor.Id = 0;
        proveedor.FechaRegistro = DateTime.UtcNow;

        _contexto.Proveedores.Add(proveedor);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPorId), new { id = proveedor.Id }, proveedor);
    }

    // Actualiza los datos de un proveedor existente.
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Proveedor datos)
    {
        var proveedor = await _contexto.Proveedores.FindAsync(id);

        if (proveedor is null)
        {
            return NotFound();
        }

        // Verifica que la identificación no pertenezca a otro proveedor.
        var repetida = await _contexto.Proveedores
            .AnyAsync(p => p.Identificacion == datos.Identificacion && p.Id != id);

        if (repetida)
        {
            return Conflict("Ya existe otro proveedor con esa identificación.");
        }

        proveedor.Nombre = datos.Nombre;
        proveedor.Identificacion = datos.Identificacion;
        proveedor.Telefono = datos.Telefono;
        proveedor.Correo = datos.Correo;
        proveedor.Direccion = datos.Direccion;
        proveedor.Activo = datos.Activo;

        await _contexto.SaveChangesAsync();

        return NoContent();
    }

    // Desactiva un proveedor (baja lógica, no se elimina el registro).
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var proveedor = await _contexto.Proveedores.FindAsync(id);

        if (proveedor is null)
        {
            return NotFound();
        }

        proveedor.Activo = false;
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}