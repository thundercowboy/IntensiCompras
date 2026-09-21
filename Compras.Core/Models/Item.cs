namespace Compras.Core.Models;

public class Item
{
    public int Id { get; set; }
    
    public int IdMaterial { get; set; }
    public Material Material { get; set; } = null!;
    
    public decimal Quantidade { get; set; }
    
    public int IdUnidade { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; } = null!;
    
    public int IdFornecedor { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;
    
    public int IdSolicitacao { get; set; }
    public Solicitacao Solicitacao { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
}