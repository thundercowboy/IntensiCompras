using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface IItemHandler
{
    Task<Response<Item?>> CreateAsync(CreateItemRequest request);
    Task<Response<Item?>> UpdateAsync(UpdateItemRequest request);
    Task<Response<Item?>> DeleteAsync(DeleteItemRequest request);
    Task<Response<Item?>> GetByIdAsync(GetItemByIdRequest request);
    Task<PagedResponse<List<Item>>> GetAllAsync(GetAllItensRequest request);
}