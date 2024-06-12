using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.Data;
// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;
using static ApiSQLITE1.ApiDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.Data;
using ApiSQLITE1.Controllers;

namespace ProyectoAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(AppDbContext context, ILogger<UsuarioController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<ActionResult<Login>> Register([FromBody] ApiSQLITE1.Controllers.RegisterRequest registerRequest)
        {
            if (await _context.Login.AnyAsync(u => u.usuario == registerRequest.usuario))
            {
                return BadRequest("Correo electrónico ya está registrado.");
            }

            var nuevoUsuario = new Login
            {
                usuario = registerRequest.usuario,
                contraseña = registerRequest.contraseña, // Almacenando la contraseña sin encriptar (no recomendado para producción)
            };

            _context.Login.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Usuario registrado: {Nombre}", registerRequest.usuario);
            return CreatedAtAction(nameof(Register), new { id = nuevoUsuario.idLogin }, nuevoUsuario);
        }



        [HttpPost("login")]
        public async Task<ActionResult<Login>> Login([FromBody] ApiSQLITE1.Controllers.LoginRequest loginRequest)
        {
            // Imprimir los datos recibidos en la consola para depuración
            Console.WriteLine($"Received login request: CorreoElectronico = {loginRequest.usuario}, Password = {loginRequest.contraseña}");
            _logger.LogInformation($"Received login request: CorreoElectronico = {loginRequest.usuario}, Password = {loginRequest.contraseña}");

            _logger.LogInformation("Intento de login para: {CorreoElectronico}", loginRequest.usuario);

            var usuario = await _context.Login.FirstOrDefaultAsync(u => u.usuario == loginRequest.usuario);

            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado: {CorreoElectronico}", loginRequest.usuario);
                return NotFound("Usuario no encontrado");
            }

            if (usuario.contraseña != loginRequest.contraseña) // Considera usar un método de hash para verificar la contraseña
            {
                _logger.LogWarning("Contraseña incorrecta para el usuario: {CorreoElectronico}", loginRequest.usuario);
                return BadRequest("Contraseña incorrecta");
            }

            _logger.LogInformation("Usuario logueado correctamente: {CorreoElectronico}", loginRequest.usuario);
            return Ok(usuario);
        }

        // Método GET para verificar la conexión y obtener todos los usuarios
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Login>>> GetAll()
        {
            _logger.LogInformation("Obteniendo todos los usuarios");
            var usuarios = await _context.Login.ToListAsync();
            return Ok(usuarios);
        }
    }
}