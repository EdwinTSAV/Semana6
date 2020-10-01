using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Models.Maps
{
    public class TCuentaMap : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("TCuenta");
            builder.HasKey(o => o.Id);

            // por cada realcion se hace esto:
            builder.HasOne(o => o.Tipo).
                WithMany(). //analizar si se va a usar el siguiente mencionado
                HasForeignKey(o => o.TypeId);
        }
    }
}
