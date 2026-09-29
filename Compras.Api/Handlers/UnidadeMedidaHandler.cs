using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.UnidadesMedida;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class UnidadeMedidaHandler(AppDbContext context) : IUnidadeMedidaHandler
{
    public async Task<Response<UnidadeMedida?>> CreateAsync(CreateUnidadeMedidaRequest request)
    {
        try
        {
            var unidade = new UnidadeMedida()
            {
                UserId = request.UserId,
                Sigla = request.Sigla,
                Nome = request.Nome
            };

            await context.AddAsync(unidade);
            await context.SaveChangesAsync();
        
            return new Response<UnidadeMedida?>(unidade, 201, "Unidade criada com sucesso.");
        }
        catch
        {
            return new Response<UnidadeMedida?>(null, 500, "Não foi possivel criar a unidade.");
        }
    }

    public async Task<Response<UnidadeMedida?>> UpdateAsync(UpdateUnidadeMedidaRequest request)
    {
        try
        {
            var unidade = await context
                .UnidadesMedida.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (unidade == null)
                return new Response<UnidadeMedida?>(null, 404, "Unidade não encontrada");

            unidade.Sigla = request.Sigla;
            unidade.Nome = request.Nome;

            await context.SaveChangesAsync();
            
            return new Response<UnidadeMedida?>(unidade, message: "Unidade atualizada com sucesso.");
            
        }
        catch
        {
            return new Response<UnidadeMedida?>(null, 500, "Não foi possivel  atualizar a unidade.");
        }
    }

    public async Task<Response<UnidadeMedida?>> DeleteAsync(DeleteUnidadeMedidaRequest request)
    {
        try
        {
            var unidade = await context
                .UnidadesMedida
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (unidade == null)
                return new Response<UnidadeMedida?>(null, 404, "Unidade não encontrada");

            context.UnidadesMedida.Remove(unidade);
            await context.SaveChangesAsync();
            
            return new Response<UnidadeMedida?>(unidade, 200, "Unidade excluida com sucesso.");
        }
        catch
        {
            return new Response<UnidadeMedida?>(null, 500, "Não foi possivel deletar a unidade.");
        }
    }

    public async Task<Response<UnidadeMedida?>> GetByIdAsync(GetUnidadeMedidaByIdRequest request)
    {
        try
        {
            var unidade = await context
                .UnidadesMedida
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return unidade is null
                ? new Response<UnidadeMedida?>(null, 404, "Unidade não encontrada")
                : new Response<UnidadeMedida?>(unidade);
        }
        catch 
        {
            return new Response<UnidadeMedida?>(null, 500, "Não foi possivel retornar a unidade.");
        }
    }

    public async Task<PagedResponse<List<UnidadeMedida>>> GetAllAsync(GetAllUnidadesMedidaRequest request)
    {
        try
        {
            var query = context
                .UnidadesMedida
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Nome);

            var unidades = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<UnidadeMedida>>(
                unidades,
                count, 
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<UnidadeMedida>>(null, 500, "Não foi possivel consultar as unidades");
        }
    }
}