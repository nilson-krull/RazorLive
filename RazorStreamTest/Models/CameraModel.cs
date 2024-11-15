using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorStreamTest.Data;

public class CameraModel : PageModel
{
    private readonly UserService _userService;

    public bool IsAdmin { get; set; }

    public CameraModel(UserService userService)
    {
        _userService = userService;
    }

    public async Task OnGet()
    {
        // Recupera o nome de usuário da sessão
        var username = HttpContext.Session.GetString("Username");

        if (!string.IsNullOrEmpty(username))
        {
            // Busca o usuário no MongoDB
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user != null)
            {
                // Verifica se o usuário é administrador
                IsAdmin = user.Type == "adm";
            }
        }

        // Passa a informação para a View
        ViewData["IsAdmin"] = IsAdmin;
    }
}
