// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using Microsoft.AspNetCore.Routing.Constraints;
[ApiController]
[Route("api/[controller]/[Action]")]
public class ProductoController : ControllerBase
{

    private readonly AppDbContext _context;

    public ProductoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetTodasLasProducto()
    {
        return await _context.Producto.ToListAsync();
    }

    // GET: api
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProductoPorId(int id)
    {
        var Producto = await _context.Producto.FindAsync(id);

        if (Producto == null)
        {
            return NotFound();
        }

        return Producto;
    }
    [HttpPost("Create")]
    public async Task<ActionResult<Producto>> CrearProduto([FromBody] Producto nuevoProducto)
    {
        if (nuevoProducto == null)
        {
            return BadRequest();
        }

        _context.Producto.Add(nuevoProducto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductoPorId), new { id = nuevoProducto.idProducto }, nuevoProducto);
    }



    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto actualizadoProducto)
    {
        if (id != actualizadoProducto.idProducto)
        {
            return BadRequest();
        }

        var producto = await _context.Producto.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        producto.nombre = actualizadoProducto.nombre;
        producto.descripcion = actualizadoProducto.descripcion;
        producto.precio = actualizadoProducto.precio;
        producto.status = actualizadoProducto.status;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExistente(id))
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
    public async Task<IActionResult> EliminarProducto(int id)
    {
        var categoria = await _context.Producto.FindAsync(id);
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
            if (!ProductoExistente(id))
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

    private bool ProductoExistente(int id)
    {
        return _context.Producto.Any(e => e.idProducto == id);
    }

    //
}