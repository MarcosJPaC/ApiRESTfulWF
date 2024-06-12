// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

[ApiController]
[Route("api/[controller]/[Action]")]
public class CategoriaController : ControllerBase
{

    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetTodosLasCategorias()
    {
        return await _context.Categoria.ToListAsync();
    }

    // GET: api/Cliente/GetCliente/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoriaPorId(int id)
    {
        var cliente = await _context.Categoria.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return cliente;
    }
    [HttpPost("CreateCategoria")]
    public async Task<ActionResult<Categoria>> CrearCategoria(string nombre, string descripcion, string estado)
    {

        var nuevaCategoria = new Categoria
        {
            nombre = nombre,
            descripcion = descripcion,
            estado = estado,
            status=1,
        };

        _context.Categoria.Add(nuevaCategoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoriaPorId), new { id = nuevaCategoria.idCategoria }, nuevaCategoria);
    }


    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("UpdateCategoria/{id}")]
    public async Task<IActionResult> ActualizarCategoria(int id, string nombre, string descripcion, string estado)
    {
        var categoria = await _context.Categoria.FindAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        categoria.nombre = nombre;
        categoria.descripcion = descripcion;
        categoria.estado = estado;

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
    [HttpDelete("DeleteCategoria/{id}")]
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        var categoria = await _context.Categoria.FindAsync(id);
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
        return _context.Categoria.Any(e => e.idCategoria == id);
    }

    //
}