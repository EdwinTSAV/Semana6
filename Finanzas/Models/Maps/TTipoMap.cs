using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Models.Maps
{
    public class TTipoMap : IEntityTypeConfiguration<Tipos>
    {
        public void Configure(EntityTypeBuilder<Tipos> builder)
        {
            builder.ToTable("TCType");
            builder.HasKey(o => o.Id);
        }
    }
}
