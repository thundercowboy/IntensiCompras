namespace Compras.Core.Models;

public class Solicitacao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataAtualizacao { get; set; }
    
    public string Descricao  { get; set; } = string.Empty;
    
    public ICollection<Item> Itens { get; set; } = new List<Item>();
    
    public ESetor Setor { get; set; }

    public EStatusSolicitacao StatusSolicitacao { get; set; } = EStatusSolicitacao.Pendente;
    
    public int IdAprovacao { get; set; }
    public Aprovacao Aprovacao { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
}