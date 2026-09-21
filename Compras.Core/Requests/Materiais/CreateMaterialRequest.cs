using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.Materiais;

public class CreateMaterialRequest : Request
{
    [Required(ErrorMessage = "Código do Material Inválido")]
    public int CodigoMaterial { get; set; }
    
    [Required(ErrorMessage = "Nome do material inválido")]
    [MaxLength(150, ErrorMessage = "O nome deve conter até 150 caracteres")]
    public string Nome { get; set; } = string.Empty;
}