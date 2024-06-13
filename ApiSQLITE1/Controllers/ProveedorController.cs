// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

[ApiController]
[Route("api/[controller]/[Action]")]
public class ProveedorController : ControllerBase
{

    private readonly AppDbContext _context;

    public ProveedorController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> GetTodasLosProveedores()
    {
        return await _context.Proveedor.ToListAsync();
    }

    // GET: api
    [HttpGet("{id}")]
    public async Task<ActionResult<Proveedor>>
        GetProveedorPorId(int id)
    {
        var Proveedor = await _context.Proveedor.FindAsync(id);

        if (Proveedor == null)
        {
            return NotFound();
        }

        return Proveedor;
    }
    [HttpPost("Create")]
    public async Task<ActionResult<Proveedor>> CrearEmpleado([FromBody] Proveedor nuevoProducto)
    {
        if (nuevoProducto == null)
        {
            return BadRequest();
        }

        _context.Proveedor.Add(nuevoProducto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodasLosProveedores), new { id = nuevoProducto.idProveedor }, nuevoProducto);
    }

    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Proveedor actualizadoProducto)
    {
        if (id != actualizadoProducto.idProveedor)
        {
            return BadRequest();
        }

        var producto = await _context.Proveedor.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        producto.nombre = actualizadoProducto.nombre;
        producto.direccion = actualizadoProducto.direccion;
        producto.telefono = actualizadoProducto.telefono;
        producto.status = actualizadoProducto.status;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProveedorExistente(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Cliente/DeleteCliente/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> EliminarProveedor(int id)
    {
        var categoria = await _context.Proveedor.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        categoria.status = 0; // Cambiando el status a 0 en lugar de eliminar

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProveedorExistente(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool ProveedorExistente(int id)
    {
        return _context.Proveedor.Any(e => e.idProveedor == id);
    }

    //
}