
using System.Reflection;
using Compras.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Solicitacao> Solicitacoes { get; set; } = null!;
    public DbSet<Aprovacao> Aprovacoes { get; set; } = null!;
    public DbSet<CategoriaItem> CategoriasItem { get; set; } = null!;
    public DbSet<Fornecedor> Fornecedores { get; set; } = null!;
    public DbSet<Item> Itens { get; set; } = null!;
    public DbSet<Material> Materiais { get; set; } = null!;
    public DbSet<UnidadeMedida> UnidadesMedida { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
}