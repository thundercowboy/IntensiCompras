using Compras.Core.Handlers;
using Compras.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Compras.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IAccountHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await Handler.LogoutAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
            NavigationManager.NavigateTo("/login", replace: true);
        }
        catch (Exception)
        {
            ErrorMessage = "Não foi possível encerrar a sessão. Tente novamente.";
            Snackbar.Add(ErrorMessage, Severity.Error);
        }
    }
}