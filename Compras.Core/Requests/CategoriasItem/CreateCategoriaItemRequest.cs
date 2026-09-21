using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.CategoriasItem;

public class CreateCategoriaItemRequest : Request
{
    [Required(ErrorMessage = "Nome do material inválido")]
    [MaxLength(150, ErrorMessage = "O nome deve conter até 150 caracteres")]
    public string Nome { get; set; } = string.Empty;
}