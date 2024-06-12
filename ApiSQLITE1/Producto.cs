using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ApiSQLITE1
{
    public class Producto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProducto  { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string descripcion { get; set; }

        [Required]
        public double precio { get; set; }
        [Required]
        public int status { get; set; }
    }
}
