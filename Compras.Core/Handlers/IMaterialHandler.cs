using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface IMaterialHandler
{
    Task<Response<Material?>> CreateAsync(CreateMaterialRequest request);
    Task<Response<Material?>> UpdateAsync(UpdateMaterialRequest request);
    Task<Response<Material?>> DeleteAsync(DeleteMaterialRequest request);
    Task<Response<Material?>> GetByIdAsync(GetMaterialByIdRequest request);
    Task<PagedResponse<List<Material>>> GetAllAsync(GetAllMateriaisRequest request);
}