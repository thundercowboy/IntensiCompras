using Compras.Core.Handlers;
using Compras.Core.Requests.Fornecedores;
using Compras.Core.Requests.Materiais;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.Fornecedor;

public partial class ListFornecedorPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    
    public List<Core.Models.Fornecedor> Fornecedores { get; set; } = [];
    
    public string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Services

    public ISnackbar Snackbar { get; set; } = null!;
    
    public IDialogService DialogService { get; set; } = null!;
    
    public IFornecedorHandler Handler { get; set; } = null!;
    
    #endregion
    
    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllFornecedoresRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Fornecedores = result.Data ?? [];
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    #endregion

    public async void OnDeleteButtonClickedAsync(int id, string nome)
    {
        var result = await DialogService.ShowMessageBoxAsync(
            "ATENÇÃO",
            $"Ao prosseguir o fornecedor {nome} será excluido. Esta é uma ação irreversivel, deseja continuar?",
            yesText: "EXCLUIR",
            cancelText: "CANCELAR");
        
        if (result is true)
            await OnDeleteAsync(id, nome);
        
        StateHasChanged();
    }

    public async Task OnDeleteAsync(int id, string nome)
    {
        try
        {
            var request = new DeleteFornecedorRequest() { Id = id };
            await Handler.DeleteAsync(request);
            Fornecedores.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Fornecedor {nome} removido com sucesso", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public Func<Core.Models.Fornecedor, bool> Filter => fornecedor =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (fornecedor.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (fornecedor.Nome is not null &&
            fornecedor.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
}