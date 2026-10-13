using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class CategoriaItemHandler(IHttpClientFactory httpClientFactory) : ICategoriaItemHandler 
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<CategoriaItem?>> CreateAsync(CreateCategoriaItemRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/categorias", request);
        return await result.Content.ReadFromJsonAsync<Response<CategoriaItem?>>()
               ?? new Response<CategoriaItem?>(null, 400, "Falha ao criar a categoria.");
    }

    public async Task<Response<CategoriaItem?>> UpdateAsync(UpdateCategoriaItemRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/categorias/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<CategoriaItem?>>()
               ?? new Response<CategoriaItem?>(null, 400, "Falha ao atualizar a categoria.");
    }

    public async Task<Response<CategoriaItem?>> DeleteAsync(DeleteCategoriaItemRequest request)
    {
        var result = await _client.DeleteAsync($"v1/categorias/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<CategoriaItem?>>()
               ?? new Response<CategoriaItem?>(null, 400, "Falha ao excluir a categoria.");
    }

    public async Task<Response<CategoriaItem?>> GetByIdAsync(GetCategoriaItemByIdRequest request)
        => await _client.GetFromJsonAsync<Response<CategoriaItem?>>($"v1/categorias/{request.Id}")
           ?? new Response<CategoriaItem?>(null, 400, "Não foi possivel recuperar a categoria.");

    public async Task<PagedResponse<List<CategoriaItem>>> GetAllAsync(GetAllCategoriasItemRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<CategoriaItem>>>($"v1/categorias")
           ?? new PagedResponse<List<CategoriaItem>>(null, 400, "Não foi possivel recuperar as categorias.");
}