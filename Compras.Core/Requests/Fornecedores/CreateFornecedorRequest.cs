using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.Fornecedores;

public class CreateFornecedorRequest : Request
{
    [Required(ErrorMessage = "Nome do fornecedor inválido")]
    [MaxLength(150, ErrorMessage = "O nome deve conter até 150 caracteres")]
    public string Nome { get; set; } = string.Empty;
}