using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Itens;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class ItemHandler(IHttpClientFactory httpClientFactory) : IItemHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Item?>> CreateAsync(CreateItemRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/itens", request);
        return await result.Content.ReadFromJsonAsync<Response<Item?>>()
               ?? new Response<Item?>(null, 400, "Falha ao criar o item.");
    }

    public async Task<Response<Item?>> UpdateAsync(UpdateItemRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/itens/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Item?>>()
               ?? new Response<Item?>(null, 400, "Falha ao atualizar o item.");
    }

    public async Task<Response<Item?>> DeleteAsync(DeleteItemRequest request)
    {
        var result = await _client.DeleteAsync($"v1/itens/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Item?>>()
               ?? new Response<Item?>(null, 400, "Falha ao excluir o item.");
    }

    public async Task<Response<Item?>> GetByIdAsync(GetItemByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Item?>>($"v1/itens/{request.Id}")
           ?? new Response<Item?>(null, 400, "Não foi possivel recuperar o item.");

    public async Task<PagedResponse<List<Item>>> GetAllAsync(GetAllItensRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Item>>>($"v1/itens")
            ?? new PagedResponse<List<Item>>(null, 400, "Não foi possivel recuperar os itens.");
}