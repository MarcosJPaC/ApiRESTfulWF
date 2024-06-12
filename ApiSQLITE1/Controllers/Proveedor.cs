using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ApiSQLITE1
{
    public class Proveedor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProveedor { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string direccion { get; set; }
        [Required]

        public string telefono { get; set; }
        [Required]

        public int status { get; set; }
    }
}
