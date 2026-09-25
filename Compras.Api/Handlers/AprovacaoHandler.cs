using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Aprovacoes;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class AprovacaoHandler(AppDbContext context) : IAprovacaoHandler
{
    public async Task<Response<Aprovacao?>> CreateAsync(CreateAprovacaoRequest request)
    {
        try
        {
            var aprovacao = new Aprovacao()
            {
                UserId = request.UserId,
                Nome = request.Nome
            };

            await context.AddAsync(aprovacao);
            await context.SaveChangesAsync();
            
            return new Response<Aprovacao?>(aprovacao, 201, "Aprovação criada com sucesso.");
        }
        catch
        {
            return new Response<Aprovacao?>(null, 500, "Não foi possivel criar a aprovação.");
        }
    }

    public async Task<Response<Aprovacao?>> UpdateAsync(UpdateAprovacaoRequest request)
    {
        try
        {
            var aprovacao = await context
                .Aprovacoes.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (aprovacao == null)
                return new Response<Aprovacao?>(null, 404, "Aprovação não encontrada");
            
            aprovacao.Nome = request.Nome;

            await context.SaveChangesAsync();
            
            return new Response<Aprovacao?>(aprovacao, message: "Aprovação atualizada com sucesso.");
            
        }
        catch
        {
            return new Response<Aprovacao?>(null, 500, "Não foi possivel atualizar a aprovação.");
        }
    }

    public async Task<Response<Aprovacao?>> DeleteAsync(DeleteAprovacaoRequest request)
    {
        try
        {
            var aprovacao = await context
                .Aprovacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (aprovacao == null)
                return new Response<Aprovacao?>(null, 404, "Aprovação não encontrada");

            context.Aprovacoes.Remove(aprovacao);
            await context.SaveChangesAsync();
            
            return new Response<Aprovacao?>(aprovacao, 200, "Aprovação excluida com sucesso.");
        }
        catch
        {
            return new Response<Aprovacao?>(null, 500, "Não foi possivel deletar a aprovação.");
        }
    }

    public async Task<Response<Aprovacao?>> GetByIdAsync(GetAprovacaoByIdRequest request)
    {
        try
        {
            var aprovacao = await context
                .Aprovacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return aprovacao is null
                ? new Response<Aprovacao?>(null, 404, "Aprovação não encontrada")
                : new Response<Aprovacao?>(aprovacao);
        }
        catch 
        {
            return new Response<Aprovacao?>(null, 500, "Não foi possivel retornar a aprovação.");
        }
    }

    public async Task<PagedResponse<List<Aprovacao>>> GetAllAsync(GetAllAprovacoesRequest request)
    {
        try
        {
            var query = context
                .Aprovacoes
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Nome);

            var aprovacoes = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Aprovacao>>(
                aprovacoes,
                count, 
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Aprovacao>>(null, 500, "Não foi possivel consultar as aprovações.");
        }
    }
}