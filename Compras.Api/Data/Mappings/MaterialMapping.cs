using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.Api.Data.Mappings;

public class MaterialMapping : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materiais");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CodigoMaterial)
            .IsRequired(true)
            .HasColumnType("INT");
        
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