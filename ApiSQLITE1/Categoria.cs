using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ApiSQLITE1
{
    public class Categoria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCategoria { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string descripcion { get; set; }

        [Required]
        public string estado { get; set; }
        [Required]
        public int status { get; set; }
    }
}
