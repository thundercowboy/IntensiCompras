using Compras.Core.Models;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface ICategoriaItemHandler
{
    Task<Response<CategoriaItem?>> CreateAsync(CreateCategoriaItemRequest request);
    Task<Response<CategoriaItem?>> UpdateAsync(UpdateCategoriaItemRequest request);
    Task<Response<CategoriaItem?>> DeleteAsync(DeleteCategoriaItemRequest request);
    Task<Response<CategoriaItem?>> GetByIdAsync(GetCategoriaItemByIdRequest request);
    Task<PagedResponse<List<CategoriaItem>>> GetAllAsync(GetAllCategoriasItemRequest request);
}