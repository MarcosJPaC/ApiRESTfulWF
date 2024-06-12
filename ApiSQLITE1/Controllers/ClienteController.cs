// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetTodosLosClientes()
    {
        return await _context.Cliente.ToListAsync();
    }

    // GET: api/Cliente/GetCliente/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetClientePorId(int id)
    {
        var cliente = await _context.Cliente.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return cliente;
    }
    // POST: api/Cliente/CreateCliente
    [HttpPost("CreateCliente")]
    public async Task<ActionResult<Cliente>> CreateCliente(string nombre, string direccion, string telefono, bool status)
    {
        int estadostatus = status ? 1 : 0;

        var nuevoCliente = new Cliente
        {
            nombre = nombre,
            direccion = direccion,
            telefono = telefono,
            status = estadostatus,
        };

        _context.Cliente.Add(nuevoCliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClientePorId), new { id = nuevoCliente.idCliente }, nuevoCliente);
    }


    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("UpdateCliente/{id}")]
    public async Task<IActionResult> UpdateCliente(int id, string nombre, string direccion, string telefono)
    {
        var cliente = await _context.Cliente.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        cliente.nombre = nombre;
        cliente.direccion = direccion;
        cliente.telefono = telefono;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExiste(id))
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
    [HttpDelete("DeleteCliente/{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var cliente = await _context.Cliente.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }

        cliente.status = 0; // Cambiando el status a 0 en lugar de eliminar

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExiste(id))
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

    private bool ClienteExiste(int id)
    {
        return _context.Cliente.Any(e => e.idCliente == id);
    }

    //
}