using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.Api.Data.Mappings;

public class ItemMapping : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Itens");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Quantidade)
            .IsRequired(true)
            .HasColumnType("MONEY");
        
        builder.HasOne(x => x.Material)
            .WithMany()
            .HasForeignKey(x => x.IdMaterial)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UnidadeMedida)
            .WithMany()
            .HasForeignKey(x => x.IdUnidade)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Fornecedor)
            .WithMany()
            .HasForeignKey(x => x.IdFornecedor)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}