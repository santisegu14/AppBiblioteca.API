using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.Models.Dto;
using AppBiblioteca.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBiblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        public UsuarioController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var lista = await _db.Usuarios.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Usuarioes";
            return Ok(_response);
            //return Ok(lista); // Status code = 200
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioes(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);
            _response.Resultado = usuario;
            _response.Mensaje = "Datos del Usuario" + usuario.ID;
            return Ok(_response);

            //return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] Usuario usuario)
        {
            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetUsuario", new { id = usuario.ID }, usuario); //Status Code = 201
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return BadRequest("Id Usuario no coincide");
            }
            _db.Update(usuario);
            await _db.SaveChangesAsync();
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }
            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}