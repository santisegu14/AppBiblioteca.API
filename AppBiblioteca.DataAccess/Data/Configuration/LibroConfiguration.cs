using AppBiblioteca.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBiblioteca.DataAccess.Data.Configuration
{
    public class LibroConfiguration : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {

            builder.HasKey(e => e.ID);
            builder.Property(e => e.Titulo).IsRequired().HasMaxLength(255);
            builder.Property(e => e.AnioPublicacion).IsRequired();
            builder.Property(e => e.AutorID).IsRequired();
            builder.Property(e => e.CategoriaID).IsRequired();

            builder.HasOne(e => e.Autor).WithMany().HasForeignKey(e => e.AutorID);
            builder.HasOne(e => e.Categoria).WithMany().HasForeignKey(e => e.CategoriaID);
        }
    }
}
