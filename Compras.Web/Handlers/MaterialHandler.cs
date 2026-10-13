using System.Net.Http.Json;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Responses;

namespace Compras.Web.Handlers;

public class MaterialHandler(IHttpClientFactory httpClientFactory) : IMaterialHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Material?>> CreateAsync(CreateMaterialRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/materiais", request);
        return await result.Content.ReadFromJsonAsync<Response<Material?>>()
               ?? new Response<Material?>(null, 400, "Falha ao criar o material.");
    }

    public async Task<Response<Material?>> UpdateAsync(UpdateMaterialRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/materiais/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Material?>>()
               ?? new Response<Material?>(null, 400, "Falha ao atualizar o material.");
    }

    public async Task<Response<Material?>> DeleteAsync(DeleteMaterialRequest request)
    {
        var result = await _client.DeleteAsync($"v1/materiais/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Material?>>()
               ?? new Response<Material?>(null, 400, "Falha ao excluir o material.");
    }

    public async Task<Response<Material?>> GetByIdAsync(GetMaterialByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Material?>>($"v1/materiais/{request.Id}")
           ?? new Response<Material?>(null, 400, "Não foi possivel recuperar o Material.");

    public async Task<PagedResponse<List<Material>>> GetAllAsync(GetAllMateriaisRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Material>>>($"v1/materias")
           ?? new PagedResponse<List<Material>>(null, 400, "Não foi possivel recuperar os materiais.");
}