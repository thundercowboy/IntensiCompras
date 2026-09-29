using Compras.Core.Models;
using Compras.Core.Requests.Solicitacoes;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface ISolicitacaoHandler
{
    Task<Response<Solicitacao?>> CreateAsync(CreateSolicitacaoRequest request);
    Task<Response<Solicitacao?>> UpdateAsync(UpdateSolicitacaoRequest request);
    Task<Response<Solicitacao?>> DeleteAsync(DeleteSolicitacaoRequest request);
    Task<Response<Solicitacao?>> GetByIdAsync(GetSolicitacaoByIdRequest request);
    Task<PagedResponse<List<Solicitacao>>> GetAllAsync(GetAllSolicitacoesRequest request);
}