using System.ComponentModel.DataAnnotations;

namespace Compras.Core.Requests.Account;

public class RegisterRequest : Request
{
    [Required(ErrorMessage = "Email")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } =  string.Empty;
    
    [Required(ErrorMessage = "Senha invalida")]
    public string Password { get; set; } =  string.Empty;
}