using Compras.Core.Handlers;
using Compras.Core.Models;
using Compras.Core.Requests.Materiais;
using Compras.Core.Requests.UnidadesMedida;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.UnidadesMedida;

public partial class ListUnidadesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    
    public List<UnidadeMedida> Unidades { get; set; } = [];
    
    public string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService DialogService { get; set; } = null!;
    [Inject]
    public IUnidadeMedidaHandler Handler { get; set; } = null!;
    
    #endregion
    
    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllUnidadesMedidaRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Unidades = result.Data ?? [];
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
            $"Ao prosseguir a unidade {nome} será excluida. Esta é uma ação irreversivel, deseja continuar?",
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
            var request = new DeleteUnidadeMedidaRequest() { Id = id };
            await Handler.DeleteAsync(request);
            Unidades.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Unidade {nome} removida com sucesso", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public Func<UnidadeMedida, bool> Filter => unidade =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (unidade.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (unidade.Nome is not null &&
            unidade.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
}