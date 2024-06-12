using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSQLITE1
{
    public class Empleado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idEmpleado { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string puesto { get; set; }

        [Required]
        public float salario { get; set; }

        public int status { get; set; }
    }
}
