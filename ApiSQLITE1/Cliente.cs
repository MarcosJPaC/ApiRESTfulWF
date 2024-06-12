using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ApiSQLITE1
{
    public class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCliente { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string direccion { get; set; }

        [Required]
        public string telefono { get; set; }

        public int status { get; set; }
    }
}
