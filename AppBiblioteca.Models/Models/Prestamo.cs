﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBiblioteca.Models.Models
{
    public class Prestamo
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Libro")]
        public int LibroID { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        // Relaciones
        public Libro Libro { get; set; } 

    }
}