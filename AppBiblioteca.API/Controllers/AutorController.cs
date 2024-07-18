using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.Models.Dto;
using AppBiblioteca.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBiblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        public AutorController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutors()
        {
            var lista = await _db.Autores.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Autores";
            return Ok(_response);
            //return Ok(lista); // Status code = 200
        }

        [HttpGet("{id}", Name = "GetAutor")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores(int id)
        {
            var autor = await _db.Autores.FindAsync(id);
            _response.Resultado = autor;
            _response.Mensaje = "Datos del Autor" + autor.ID;
            return Ok(_response);

            //return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor([FromBody] Autor autor)
        {
            await _db.Autores.AddAsync(autor);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetAutor", new { id = autor.ID }, autor); //Status Code = 201
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAutor(int id, [FromBody] Autor autor)
        {
            if (id != autor.ID)
            {
                return BadRequest("Id Autor no coincide");
            }
            _db.Update(autor);
            await _db.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutor(int id)
        {
            var autor = await _db.Autores.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }
            _db.Autores.Remove(autor);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}