using AppBiblioteca.DataAccess.Data.Configuration;
using AppBiblioteca.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AppBiblioteca.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new AutorConfiguration());
            modelbuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelbuilder.ApplyConfiguration(new LibroConfiguration());
            modelbuilder.ApplyConfiguration(new PrestamoConfiguration());
        }



    }
}


