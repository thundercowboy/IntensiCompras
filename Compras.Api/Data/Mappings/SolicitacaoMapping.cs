using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.Api.Data.Mappings;

public class SolicitacaoMapping : IEntityTypeConfiguration<Solicitacao>
{
    public void Configure(EntityTypeBuilder<Solicitacao> builder)
    {
        builder.ToTable("Solicitacoes");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Nome)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.DataCriacao)
            .IsRequired(true);
        builder.Property(x => x.DataAtualizacao)
            .IsRequired(false);
        
        builder.Property(x => x.Descricao)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(260);
        
        builder.HasMany(x => x.Itens)
            .WithOne(x => x.Solicitacao)
            .HasForeignKey(x => x.IdSolicitacao)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Aprovacao)
            .WithMany()
            .HasForeignKey(x => x.IdAprovacao)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.Setor)
            .IsRequired(true)
            .HasColumnType("SMALLINT");
        
        builder.Property(x => x.StatusSolicitacao)
            .IsRequired(true)
            .HasColumnType("SMALLINT");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}