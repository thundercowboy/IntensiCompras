using System.ComponentModel.DataAnnotations;
using Compras.Core.Models;

namespace Compras.Core.Requests.Solicitacoes;

public class CreateSolicitacaoRequest : Request
{
    [Required(ErrorMessage = "Nome da solicitação inválido.")]
    [MaxLength(80, ErrorMessage = "O nome deve conter até 80 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    public DateTime? DataAtualizacao { get; set; }

    [Required(ErrorMessage = "Descrição inválida.")]
    [MaxLength(260, ErrorMessage = "A descrição deve conter até 260 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [EnumDataType(typeof(ESetor), ErrorMessage = "Setor inválido.")]
    public ESetor Setor { get; set; } = ESetor.Compras;

    [EnumDataType(typeof(EStatusSolicitacao), ErrorMessage = "Status da solicitação inválido.")]
    public EStatusSolicitacao StatusSolicitacao { get; set; } = EStatusSolicitacao.Pendente;

    [Range(1, int.MaxValue, ErrorMessage = "Id da aprovação deve ser maior que zero.")]
    public int? IdAprovacao { get; set; }

}
