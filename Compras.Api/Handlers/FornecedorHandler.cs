using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class FornecedorHandler(AppDbContext context) : IFornecedorHandler
{
    public async Task<Response<Fornecedor?>> CreateAsync(CreateFornecedorRequest request)
    {
        try
        {
            var fornecedor = new Fornecedor()
            {
                UserId = request.UserId,
                Nome = request.Nome
            };

            await context.AddAsync(fornecedor);
            await context.SaveChangesAsync();
            
            return new Response<Fornecedor?>(fornecedor, 201, "Fornecedor criada com sucesso.");
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possivel criar o fornecedor.");
        }
    }

    public async Task<Response<Fornecedor?>> UpdateAsync(UpdateFornecedorRequest request)
    {
        try
        {
            var fornecedor = await context
                .Fornecedores.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (fornecedor == null)
                return new Response<Fornecedor?>(null, 404, "Fornecedor não encontrado");
            
            fornecedor.Nome = request.Nome;

            await context.SaveChangesAsync();
            
            return new Response<Fornecedor?>(fornecedor, message: "Fornecedor atualizado com sucesso.");
            
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possivel  atualizar o fornecedor.");
        }
    }

    public async Task<Response<Fornecedor?>> DeleteAsync(DeleteFornecedorRequest request)
    {
        try
        {
            var fornecedor = await context
                .Fornecedores
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (fornecedor == null)
                return new Response<Fornecedor?>(null, 404, "Fornecedor não encontrado");

            context.Fornecedores.Remove(fornecedor);
            await context.SaveChangesAsync();
            
            return new Response<Fornecedor?>(fornecedor, 200, "Fornecedor excluido com sucesso.");
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possivel deletar o fornecedor.");
        }
    }

    public async Task<Response<Fornecedor?>> GetByIdAsync(GetFornecedorByIdRequest request)
    {
        try
        {
            var fornecedor = await context
                .Fornecedores
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return fornecedor is null
                ? new Response<Fornecedor?>(null, 404, "Fornecedor não encontrado")
                : new Response<Fornecedor?>(fornecedor);
        }
        catch 
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possivel retornar o fornecedor.");
        }
    }

    public async Task<PagedResponse<List<Fornecedor>>> GetAllAsync(GetAllFornecedoresRequest request)
    {
        try
        {
            var query = context
                .Fornecedores
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Nome);

            var fornecedores = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Fornecedor>>(
                fornecedores,
                count, 
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Fornecedor>>(null, 500, "Não foi possivel consultar os fornecedores");
        }
    }
}