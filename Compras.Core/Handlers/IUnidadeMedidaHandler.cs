using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface IUnidadeMedidaHandler
{
    Task<Response<UnidadeMedida?>> CreateAsync(CreateUnidadeMedidaRequest request);
    Task<Response<UnidadeMedida?>> UpdateAsync(UpdateUnidadeMedidaRequest request);
    Task<Response<UnidadeMedida?>> DeleteAsync(DeleteUnidadeMedidaRequest request);
    Task<Response<UnidadeMedida?>> GetByIdAsync(GetUnidadeMedidaByIdRequest request);
    Task<PagedResponse<List<UnidadeMedida>>> GetAllAsync(GetAllUnidadesMedidaRequest request);
}