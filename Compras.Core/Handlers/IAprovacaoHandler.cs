using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface IAprovacaoHandler
{
    Task<Response<Aprovacao?>> CreateAsync(CreateAprovacaoRequest request);
    Task<Response<Aprovacao?>> UpdateAsync(UpdateAprovacaoRequest request);
    Task<Response<Aprovacao?>> DeleteAsync(DeleteAprovacaoRequest request);
    Task<Response<Aprovacao?>> GetByIdAsync(GetAprovacaoByIdRequest request);
    Task<PagedResponse<List<Aprovacao>>> GetAllAsync(GetAllAprovacoesRequest request);

}