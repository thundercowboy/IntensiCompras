namespace Compras.Core.Models;

public class Material
{
    public int Id { get; set; }
    public int CodigoMaterial { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}