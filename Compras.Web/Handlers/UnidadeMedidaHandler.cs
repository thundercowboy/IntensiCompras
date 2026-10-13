using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class UnidadeMedidaHandler(IHttpClientFactory httpClientFactory) : IUnidadeMedidaHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<UnidadeMedida?>> CreateAsync(CreateUnidadeMedidaRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/unidades", request);
        return await result.Content.ReadFromJsonAsync<Response<UnidadeMedida?>>()
               ?? new Response<UnidadeMedida?>(null, 400, "Falha ao criar a unidade.");
    }

    public async Task<Response<UnidadeMedida?>> UpdateAsync(UpdateUnidadeMedidaRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/unidades/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<UnidadeMedida?>>()
               ?? new Response<UnidadeMedida?>(null, 400, "Falha ao atualizar a unidade.");
    }

    public async Task<Response<UnidadeMedida?>> DeleteAsync(DeleteUnidadeMedidaRequest request)
    {
        var result = await _client.DeleteAsync($"v1/unidades/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<UnidadeMedida?>>()
               ?? new Response<UnidadeMedida?>(null, 400, "Falha ao excluir a unidade.");
    }

    public async Task<Response<UnidadeMedida?>> GetByIdAsync(GetUnidadeMedidaByIdRequest request)
        => await _client.GetFromJsonAsync<Response<UnidadeMedida?>>($"v1/unidades/{request.Id}")
           ?? new Response<UnidadeMedida?>(null, 400, "Não foi possivel recuperar a unidade.");


    public async Task<PagedResponse<List<UnidadeMedida>>> GetAllAsync(GetAllUnidadesMedidaRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<UnidadeMedida>>>($"v1/unidades")
           ?? new PagedResponse<List<UnidadeMedida>>(null, 400, "Não foi possivel recuperar as unidades.");

}
