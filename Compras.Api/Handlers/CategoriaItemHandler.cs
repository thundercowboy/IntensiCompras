using Compras.Api.Data;
using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.CategoriasItem;
using Compras.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Compras.Api.Handlers;

public class CategoriaItemHandler(AppDbContext context) : ICategoriaItemHandler
{
    public async Task<Response<CategoriaItem?>> CreateAsync(CreateCategoriaItemRequest request)
    {
        try
        {
            var categoria = new CategoriaItem()
            {
                UserId = request.UserId,
                Nome = request.Nome
            };

            await context.AddAsync(categoria);
            await context.SaveChangesAsync();
            
            return new Response<CategoriaItem?>(categoria, 201, "Categoria criada com sucesso.");
        }
        catch
        {
            return new Response<CategoriaItem?>(null, 500, "Não foi possivel criar a categoria.");
        }
    }

    public async Task<Response<CategoriaItem?>> UpdateAsync(UpdateCategoriaItemRequest request)
    {
        try
        {
            var categoria = await context
                .CategoriasItem.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (categoria == null)
                return new Response<CategoriaItem?>(null, 404, "Categoria não encontrada");
            
            categoria.Nome = request.Nome;

            await context.SaveChangesAsync();
            
            return new Response<CategoriaItem?>(categoria, message: "Categoria atualizada com sucesso.");
            
        }
        catch
        {
            return new Response<CategoriaItem?>(null, 500, "Não foi possivel  atualizar a categoria.");
        }
    }

    public async Task<Response<CategoriaItem?>> DeleteAsync(DeleteCategoriaItemRequest request)
    {
        try
        {
            var categoria = await context
                .CategoriasItem
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (categoria == null)
                return new Response<CategoriaItem?>(null, 404, "Categoria não encontrada");

            context.CategoriasItem.Remove(categoria);
            await context.SaveChangesAsync();
            
            return new Response<CategoriaItem?>(categoria, 200, "Categoria excluida com sucesso.");
        }
        catch
        {
            return new Response<CategoriaItem?>(null, 500, "Não foi possivel deletar a categoria.");
        }
    }

    public async Task<Response<CategoriaItem?>> GetByIdAsync(GetCategoriaItemByIdRequest request)
    {
        try
        {
            var categoria = await context
                .CategoriasItem
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return categoria is null
                ? new Response<CategoriaItem?>(null, 404, "Categoria não encontrada")
                : new Response<CategoriaItem?>(categoria);
        }
        catch 
        {
            return new Response<CategoriaItem?>(null, 500, "Não foi possivel retornar a categoria.");
        }
    }

    public async Task<PagedResponse<List<CategoriaItem>>> GetAllAsync(GetAllCategoriasItemRequest request)
    {
        try
        {
            var query = context
                .CategoriasItem
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Nome);

            var categorias = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<CategoriaItem>>(
                categorias,
                count, 
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<CategoriaItem>>(null, 500, "Não foi possivel consultar as categorias");
        }
    }
}
