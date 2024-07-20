using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppBiblioteca.DataAccess.Inicializador
{
    public class DbInicializador : IDbInicializador
    {
        private readonly ApplicationDbContext _db;
        public DbInicializador(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();  // Ejecuta las migraciones pendientes
                }
            }
            catch (Exception)
            {

                throw;
            }

            // Datos Iniciales
            if (!_db.Autores.Any())
            {
                var autorData = File.ReadAllText("../AppBiblioteca.DataAccess/Data/SeedData/Autor.Json");
                var autores = JsonSerializer.Deserialize<List<Autor>>(autorData);
                _db.Autores.AddRange(autores);
            }
            if (!_db.Categorias.Any())
            {
                var categoriaData = File.ReadAllText("../AppBiblioteca.DataAccess/Data/SeedData/Categoria.Json");
                var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData);
                _db.Categorias.AddRange(categorias);
            }
            if (!_db.Libros.Any())
            {
                var libroData = File.ReadAllText("../AppBiblioteca.DataAccess/Data/SeedData/Libro.Json");
                var libros = JsonSerializer.Deserialize<List<Libro>>(libroData);
                _db.Libros.AddRange(libros);
            }
            if (!_db.Prestamos.Any())
            {
                var prestamoData = File.ReadAllText("../AppBiblioteca.DataAccess/Data/SeedData/Prestamo.Json");
                var prestamos = JsonSerializer.Deserialize<List<Prestamo>>(prestamoData);
                _db.Prestamos.AddRange(prestamos);
            }
            if (!_db.Prestamos.Any())
            {
                var prestamoData = File.ReadAllText("../AppBiblioteca.DataAccess/Data/SeedData/Prestamo.Json");
                var prestamos = JsonSerializer.Deserialize<List<Prestamo>>(prestamoData);
                _db.Prestamos.AddRange(prestamos);
            }
            if (!_db.Usuarios.Any())
            {
                var usuarioData = File.ReadAllText("../AppBiblioteca.DataAccess/Data/SeedData/Usuario.Json");
                var usuarios = JsonSerializer.Deserialize<List<Usuario>>(usuarioData);
                _db.Usuarios.AddRange(usuarios);
            }



            if (_db.ChangeTracker.HasChanges()) _db.SaveChanges();

        }

    }
}
