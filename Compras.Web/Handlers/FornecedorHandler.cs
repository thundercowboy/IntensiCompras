using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class FornecedorHandler(IHttpClientFactory httpClientFactory) : IFornecedorHandler
{
    private readonly HttpClient _client =  httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Fornecedor?>> CreateAsync(CreateFornecedorRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/fornecedores", request);
        return await result.Content.ReadFromJsonAsync<Response<Fornecedor?>>()
               ?? new Response<Fornecedor?>(null, 400, "Falha ao criar o forneceodr.");

    }

    public async Task<Response<Fornecedor?>> UpdateAsync(UpdateFornecedorRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/fornecedores/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Fornecedor?>>()
               ?? new Response<Fornecedor?>(null, 400, "Falha ao atualizar o fornecedor.");
    }

    public async Task<Response<Fornecedor?>> DeleteAsync(DeleteFornecedorRequest request)
    {
        var result = await _client.DeleteAsync($"v1/fornecedores/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Fornecedor?>>()
               ?? new Response<Fornecedor?>(null, 400, "Falha ao excluir o fornecedor.");
    }

    public async Task<Response<Fornecedor?>> GetByIdAsync(GetFornecedorByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Fornecedor?>>($"v1/fornecedores/{request.Id}")
           ?? new Response<Fornecedor?>(null, 400, "Não foi possivel recuperar o fornecedor.");
    

    public async Task<PagedResponse<List<Fornecedor>>> GetAllAsync(GetAllFornecedoresRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Fornecedor>>>($"v1/fornecedores")
           ?? new PagedResponse<List<Fornecedor>>(null, 400, "Não foi possivel recuperar os fornecedores.");
}
