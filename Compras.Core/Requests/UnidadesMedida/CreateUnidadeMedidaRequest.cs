using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.UnidadesMedida;

public class CreateUnidadeMedidaRequest : Request
{
    [Required(ErrorMessage = "Sigla da unidade inválido")]
    [MaxLength(150, ErrorMessage = "O nome deve conter até 150 caracteres")]
    public string Sigla { get; set; } = string.Empty;  
    
    [Required(ErrorMessage = "Nome do unidade inválida")]
    [MaxLength(150, ErrorMessage = "O nome deve conter até 150 caracteres")]
    public string Nome { get; set; } = string.Empty;    
}