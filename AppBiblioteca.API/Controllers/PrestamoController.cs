using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.Models.Dto;
using AppBiblioteca.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBiblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        public PrestamoController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            var lista = await _db.Prestamos
                .Include(p => p.Libro)
                .ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de prestamos";
            return Ok(_response);
            //return Ok(lista); // Status code = 200
        }

        [HttpGet("{id}", Name = "GetPrestamo")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos(int id)
        {
            var prestamo = await _db.Prestamos.FindAsync(id);
            _response.Resultado = prestamo;
            _response.Mensaje = "Datos del prestamo" + prestamo.ID;
            return Ok(_response);

            //return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo([FromBody] Prestamo prestamo)
        {
            var libroExistente = await _db.Libros.FindAsync(prestamo.LibroID);
            if (libroExistente == null)
            {
                // Si el libro no existe, devuelve un error o crea un nuevo libro según tus necesidades
                return BadRequest("El libro especificado no existe.");
            }

            // Asigna el libro existente al préstamo
            prestamo.Libro = libroExistente;

            await _db.Prestamos.AddAsync(prestamo);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetPrestamo", new { id = prestamo.ID }, prestamo); //Status Code = 201
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPrestamo(int id, [FromBody] Prestamo prestamo)
        {
            if (id != prestamo.ID)
            {
                return BadRequest("Id Prestamo no coincide");
            }
            // Verifica si el libro asociado al préstamo existe
            var libroExistente = await _db.Libros.FindAsync(prestamo.LibroID);
            if (libroExistente == null)
            {
                return NotFound("El libro asociado al préstamo no existe.");
            }

            _db.Update(prestamo);
            await _db.SaveChangesAsync();
            return Ok(prestamo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrestamo(int id)
        {
            var prestamo = await _db.Prestamos.FindAsync(id);

            if (prestamo == null)
            {
                return NotFound();
            }
            _db.Prestamos.Remove(prestamo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}