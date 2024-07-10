using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBiblioteca.Models.Dto
{
    public class ResponseDto
    {
        public bool IsExitoso { get; set; }
        public object Resultado { get; set; }
        public string Mensaje { get; set; }
    }
}
