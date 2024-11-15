using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorLive.Data;

public class RegisterModel : PageModel
{
    private readonly UserService _userService = new UserService();

    [BindProperty]
    public string Message { get; set; }

    public async Task OnPostAsync(string username, string password)
    {
        bool success = await _userService.RegisterUserAsync(username, password);
        Message = success ? "Usuário registrado com sucesso!" : "Usuário já existe.";
    }
}
