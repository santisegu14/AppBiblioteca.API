using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBiblioteca.Models.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        
    }
}
