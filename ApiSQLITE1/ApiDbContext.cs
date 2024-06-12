using Microsoft.EntityFrameworkCore;
using ApiSQLITE1;

namespace ApiSQLITE1
{
    public class ApiDbContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Cliente> Cliente { get; set; } //
            public DbSet<Categoria> Categoria { get; set; } //
            public DbSet<Empleado> Empleado { get; set; } //
            public DbSet<Venta> Venta { get; set; }
            public DbSet<Producto> Producto { get; set; } //
            public DbSet<Proveedor> Proveedor { get; set; }
            public DbSet<Login> Login { get; set; }


        }
    }
}
