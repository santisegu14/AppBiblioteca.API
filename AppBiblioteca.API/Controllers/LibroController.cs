using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.Models.Dto;
using AppBiblioteca.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBiblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        public LibroController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            var lista = await _db.Libros
                .Include(p => p.Autor)
                .Include(p => p.Categoria)
                .ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de libros";
            return Ok(_response);
            //return Ok(lista); // Status code = 200
        }

        [HttpGet("{id}", Name = "GetLibro")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros(int id)
        {
            var libro = await _db.Libros.FindAsync(id);
            _response.Resultado = libro;
            _response.Mensaje = "Datos del libro" + libro.ID;
            return Ok(_response);

            //return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro([FromBody] Libro libro)
        {
            var autorExistente = await _db.Autores.FindAsync(libro.AutorID);
            if (autorExistente == null)
            {
                // Crea un nuevo autor solo si no existe
                _db.Autores.Add(libro.Autor);
            }
            else
            {
                // Asigna el autor existente al libro
                libro.Autor = autorExistente;
            }

            var categoriaExistente = await _db.Categorias.FindAsync(libro.CategoriaID);
            if (categoriaExistente == null)
            {
                // Crea un nuevo autor solo si no existe
                _db.Categorias.Add(libro.Categoria);
            }
            else
            {
                // Asigna el autor existente al libro
                libro.Categoria = categoriaExistente;
            }


            await _db.Libros.AddAsync(libro);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetLibro", new { id = libro.ID }, libro); //Status Code = 201
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutLibro(int id, [FromBody] Libro libro)
        {
            if (id != libro.ID)
            {
                return BadRequest("Id Libro no coincide");
            }

            var autorExistente = await _db.Autores.FindAsync(libro.AutorID);
            if (autorExistente == null)
            {
                return NotFound("El autor asociado al libro no existe.");
            }

            // Verifica si la categoría asociada al libro existe
            var categoriaExistente = await _db.Categorias.FindAsync(libro.CategoriaID);
            if (categoriaExistente == null)
            {
                return NotFound("La categoría asociada al libro no existe.");
            }

            _db.Update(libro);
            await _db.SaveChangesAsync();
            return Ok(libro);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLibro(int id)
        {
            var libro = await _db.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }
            _db.Libros.Remove(libro);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}