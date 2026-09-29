using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.Api.Data.Mappings;

public class UnidadeMapping : IEntityTypeConfiguration<UnidadeMedida>
{
    public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
    {
        builder.ToTable("Unidades");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Sigla)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Nome)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}