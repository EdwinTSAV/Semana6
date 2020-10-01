using Finanzas.Models.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Models
{
    public class ContextoFinanzas : DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Tipos> Types { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        public ContextoFinanzas(DbContextOptions<ContextoFinanzas> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TCuentaMap());
            modelBuilder.ApplyConfiguration(new TTipoMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TransaccionMap());
        }
    }
}
