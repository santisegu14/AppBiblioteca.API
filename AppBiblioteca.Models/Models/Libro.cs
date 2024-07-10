using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBiblioteca.Models.Models
{
    public class Libro
    {
        [Key]
        public int ID { get; set; } 
        public string Titulo { get; set; }
        [ForeignKey("Autor")]
        public int AutorID { get; set; } 
        [ForeignKey("Categoria")]
        public int CategoriaID { get; set; }
        public int AnioPublicacion { get; set; }

        // Relaciones
        public Autor Autor { get; set; } 
        public Categoria Categoria { get; set; } 
    }


}
