// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

[ApiController]
[Route("api/[controller]/[Action]")]
public class VentaController : ControllerBase
{

    private readonly AppDbContext _context;

    public VentaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetTodasLasVentas()
    {
        return await _context.Venta.ToListAsync();
    }

    // GET: api
    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>>
        GetVentaPorId(int id)
    {
        var Venta = await _context.Venta.FindAsync(id);

        if (Venta == null)
        {
            return NotFound();
        }

        return Venta;
    }
    [HttpPost("Create")]
    public async Task<ActionResult<Venta>> CrearVenta(string fecha, float total, bool status)
    {
        int estadostatus = status ? 1 : 0;

        var NuevaVenta = new Venta
        {
            fecha = fecha,
            total = total,
            status = estadostatus,
        };

        _context.Venta.Add(NuevaVenta);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVentaPorId), new { id = NuevaVenta.idVenta }, NuevaVenta);
    }


    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> ActualizarVenta(int id, string fecha, float total)
    {
        var categoria = await _context.Venta.FindAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        categoria.fecha = fecha;
        categoria.total = total;


        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VentaExiste(id))
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
    public async Task<IActionResult> EliminarVenta(int id)
    {
        var categoria = await _context.Venta.FindAsync(id);
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
            if (!VentaExiste(id))
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

    private bool VentaExiste(int id)
    {
        return _context.Venta.Any(e => e.idVenta == id);
    }

    //
}