using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppBiblioteca.Models.Models;

namespace AppBiblioteca.DataAccess.Data.Configuration
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {

            builder.HasKey(e => e.ID);
            builder.Property(e => e.Nombre).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Apellido).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Nacionalidad).IsRequired().HasMaxLength(255);
        }
    }
}