using Compras.Core.Handlers;
using Compras.Core.Requests.UnidadesMedida;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.UnidadesMedida;

public partial class CreateUnidadesMedidaPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateUnidadeMedidaRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject] public IUnidadeMedidaHandler Handler { get; set; } = null!;

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/unidades");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
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
}

