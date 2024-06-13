// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

[ApiController]
[Route("api/[controller]/[Action]")]
public class EmpleadoController : ControllerBase
{

    private readonly AppDbContext _context;

    public EmpleadoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empleado>>> GetTodosLosEmpleados()
    {
        return await _context.Empleado.ToListAsync();
    }

    // GET: api
    [HttpGet("{id}")]
    public async Task<ActionResult<Empleado>> GetCategoriaPorId(int id)
    {
        var Empleado = await _context.Empleado.FindAsync(id);

        if (Empleado == null)
        {
            return NotFound();
        }

        return Empleado;
    }
    [HttpPost("Create")]
    public async Task<ActionResult<Empleado>> CrearEmpleado([FromBody] Empleado nuevoProducto)
    {
        if (nuevoProducto == null)
        {
            return BadRequest();
        }

        _context.Empleado.Add(nuevoProducto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodosLosEmpleados), new { id = nuevoProducto.idEmpleado }, nuevoProducto);
    }

    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Empleado actualizadoProducto)
    {
        if (id != actualizadoProducto.idEmpleado)
        {
            return BadRequest();
        }

        var producto = await _context.Empleado.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        producto.nombre = actualizadoProducto.nombre;
        producto.puesto = actualizadoProducto.puesto ;
        producto.salario = actualizadoProducto.salario;
        producto.status = actualizadoProducto.status;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoriaExiste(id))
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
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        var categoria = await _context.Empleado.FindAsync(id);
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
            if (!CategoriaExiste(id))
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

    private bool CategoriaExiste(int id)
    {
        return _context.Empleado.Any(e => e.idEmpleado == id);
    }

    //
}