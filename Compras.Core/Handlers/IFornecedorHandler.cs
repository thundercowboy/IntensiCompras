using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;

namespace Compras.Core.Handlers;

public interface IFornecedorHandler
{
    Task<Response<Fornecedor?>> CreateAsync(CreateFornecedorRequest request);
    Task<Response<Fornecedor?>> UpdateAsync(UpdateFornecedorRequest request);
    Task<Response<Fornecedor?>> DeleteAsync(DeleteFornecedorRequest request);
    Task<Response<Fornecedor?>> GetByIdAsync(GetFornecedorByIdRequest request);
    Task<PagedResponse<List<Fornecedor>>> GetAllAsync(GetAllFornecedoresRequest request);
    
}