using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.Models.Dto;
using AppBiblioteca.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBiblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        public CategoriaController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var lista = await _db.Categorias.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de categorias";
            return Ok(_response);
            //return Ok(lista); // Status code = 200
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias(int id)
        {
            var categoria = await _db.Categorias.FindAsync(id);
            _response.Resultado = categoria;
            _response.Mensaje = "Datos de la compañia" + categoria.ID;
            return Ok(_response);

            //return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody] Categoria categoria)
        {
            await _db.Categorias.AddAsync(categoria);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetCategoria", new { id = categoria.ID }, categoria); //Status Code = 201
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.ID)
            {
                return BadRequest("Id Categoria no coincide");
            }
            _db.Update(categoria);
            await _db.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            var categoria = await _db.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }
            _db.Categorias.Remove(categoria);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}