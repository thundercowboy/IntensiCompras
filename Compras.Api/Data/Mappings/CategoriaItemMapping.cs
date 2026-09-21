using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.Api.Data.Mappings;

public class CategoriaItemMapping : IEntityTypeConfiguration<CategoriaItem>
{
    public void Configure(
        EntityTypeBuilder<CategoriaItem> builder)
    {
        builder.ToTable("CategoriasItem");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Nome)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}