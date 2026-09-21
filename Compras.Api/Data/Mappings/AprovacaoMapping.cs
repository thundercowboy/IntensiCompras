using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.Api.Data.Mappings;

public class AprovacaoMapping : IEntityTypeConfiguration<Aprovacao>
{
    public void Configure(
        EntityTypeBuilder<Aprovacao> builder)
    {
        builder.ToTable("Aprovacoes");
        
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