using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.Aprovacoes;

public class CreateAprovacaoRequest : Request
{
    [Required(ErrorMessage = "Nome da aprovação inválida")]
    [MaxLength(150, ErrorMessage = "O nome deve conter até 150 caracteres")]
    public string Nome { get; set; } = string.Empty;
}