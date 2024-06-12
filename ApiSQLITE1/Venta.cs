using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ApiSQLITE1
{
    public class Venta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idVenta { get; set; }

        [Required]
        public string fecha { get; set; }

        [Required]
        public float total { get; set; }

        public int status { get; set; }
    }
}