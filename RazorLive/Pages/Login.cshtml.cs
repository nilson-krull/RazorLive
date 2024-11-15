using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorLive.Data;

public class LoginModel : PageModel
{
    private readonly UserService _userService = new UserService();

    [BindProperty]
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync(string username, string password)
    {
        var user = await _userService.AuthenticateUserAsync(username, password);
        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToPage("/Camera");  // Redireciona para a página principal
        }

        Message = "Usuário ou senha inválidos.";
        return Page();
    }
}
