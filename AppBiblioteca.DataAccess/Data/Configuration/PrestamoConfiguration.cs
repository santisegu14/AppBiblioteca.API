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
    public class PrestamoConfiguration : IEntityTypeConfiguration<Prestamo>
    {
        public void Configure(EntityTypeBuilder<Prestamo> builder)
        {

            builder.HasKey(e => e.ID);
            builder.Property(e => e.LibroID).IsRequired();
            builder.Property(e => e.FechaPrestamo).IsRequired();
            builder.Property(e => e.FechaDevolucion).IsRequired();
            
            builder.HasOne(e => e.Libro).WithMany().HasForeignKey(e => e.LibroID);

        }
    }
}
