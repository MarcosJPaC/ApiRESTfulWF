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
    [HttpPost("Create")]
    public async Task<ActionResult<Cliente>> CrearCliente([FromBody] Cliente nuevoProducto)
    {
        if (nuevoProducto == null)
        {
            return BadRequest();
        }

        _context.Cliente.Add(nuevoProducto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClientePorId), new { id = nuevoProducto.idCliente }, nuevoProducto);
    }

    // PUT: api/Cliente/UpdateCliente/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Cliente actualizadoProducto)
    {
        if (id != actualizadoProducto.idCliente)
        {
            return BadRequest();
        }

        var producto = await _context.Cliente.FindAsync(id);
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