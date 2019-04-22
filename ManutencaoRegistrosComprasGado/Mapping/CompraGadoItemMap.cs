using ManutencaoRegistrosComprasGado.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManutencaoRegistrosComprasGado.Mapping
{
    public class CompraGadoItemMap : IEntityTypeConfiguration<CompraGadoItem>
    {
        public void Configure(EntityTypeBuilder<CompraGadoItem> builder)
        {
            builder.HasKey(cgi => cgi.CompraGadoItemId);

            builder.Property(cgi => cgi.Quantidade).IsRequired();

            builder.HasOne(cgi => cgi.CompraGado).WithMany(cgi => cgi.CompraGadoItens).HasForeignKey(cgi => cgi.CompraGadoId);
            builder.HasOne(cgi => cgi.Animal).WithMany(cgi => cgi.CompraGadoItens).HasForeignKey(cgi => cgi.AnimalId);

            builder.ToTable("CompraGadoItem");
        }
    }
}
