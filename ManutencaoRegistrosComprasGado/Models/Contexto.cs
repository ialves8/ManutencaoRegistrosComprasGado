using ManutencaoRegistrosComprasGado.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManutencaoRegistrosComprasGado.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Pecuarista> Pecuaristas { get; set; }
        public DbSet<CompraGado> CompraGados { get; set; }
        public DbSet<CompraGadoItem> CompraGadoItens { get; set; }
        public DbSet<Animal> Animais { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new PecuaristaMap());
            modelBuilder.ApplyConfiguration(new CompraGadoMap());
            modelBuilder.ApplyConfiguration(new CompraGadoItemMap());
            modelBuilder.ApplyConfiguration(new AnimalMap());
        } 
    }
}
