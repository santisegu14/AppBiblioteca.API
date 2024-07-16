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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.HasKey(e => e.ID);
            builder.Property(e => e.Nombre).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Correo).IsRequired().HasMaxLength(255);
           
        }
    }
}
