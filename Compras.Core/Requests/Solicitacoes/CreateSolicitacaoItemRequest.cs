using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.Solicitacoes;

/// <summary>
/// Item anexado na criação: referencia um material já cadastrado.
/// O vínculo com a solicitação e o usuário deve ser definido pelo servidor.
/// </summary>
public class CreateSolicitacaoItemRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Id do material deve ser maior que zero.")]
    public int IdMaterial { get; set; }

    // Limites e escala compatíveis com a coluna MONEY usada em ItemMapping.
    [Range(typeof(decimal), "0.0001", "922337203685477.5807",
        ParseLimitsInInvariantCulture = true,
        ErrorMessage = "Quantidade deve estar entre 0,0001 e 922337203685477,5807.")]
    public decimal Quantidade { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Id da unidade de medida deve ser maior que zero.")]
    public int IdUnidade { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Id do fornecedor deve ser maior que zero.")]
    public int IdFornecedor { get; set; }
}
