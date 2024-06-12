using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ApiSQLITE1
{
    public class Login
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idLogin { get; set; }

        [Required]
        public string usuario { get; set; }

        [Required]
        public string contraseña { get; set; }

        public int status { get; set; }
    }
}
