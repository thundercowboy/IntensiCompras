using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.Itens;

public class UpdateItemRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Id do material inválido")]   
    public int IdMaterial { get; set; }
    
    [Required(ErrorMessage = "Quantidade inválida")]
    public decimal Quantidade { get; set; }
    
    [Required(ErrorMessage = "Unidade de Medida inválida")]
    public int IdUnidade { get; set; }
    
    [Required(ErrorMessage = "Fornecedor inválido")]
    public int IdFornecedor { get; set; }
    
    [Required(ErrorMessage = "Solicitação inválida")]
    public int IdSolicitacao { get; set; }
}