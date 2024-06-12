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
    public async Task<ActionResult<Empleado>> CrearEmpleado(string nombre, string puesto, float salario, bool status)
    {
        int estadostatus = status ? 1 : 0;

        var Nuevoempleado = new Empleado
        {
            nombre = nombre,
            puesto = puesto,
            salario = salario,
            status = estadostatus,
        };

        _context.Empleado.Add(Nuevoempleado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoriaPorId), new { id = Nuevoempleado.idEmpleado }, Nuevoempleado);
    }


    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> ActualizarEmpleado(int id, string nombre, string puesto, float salario)
    {
        var categoria = await _context.Empleado.FindAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        categoria.nombre = nombre;
        categoria.puesto = puesto;
        categoria.salario = salario;

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