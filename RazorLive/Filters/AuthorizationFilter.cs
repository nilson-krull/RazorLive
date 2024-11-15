using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class AuthorizationFilter : IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var path = context.HttpContext.Request.Path.Value;
        // Verifique se o caminho da URL é Login ou Register
        if (path == "/Login" || path == "/Register" || path == "/register" || path == "/login")
        {
            return; // Permite acesso a essas páginas sem verificar a sessão
        }

        // Verifica se a sessão contém o usuário logado
        if (context.HttpContext.Session.GetString("Username") == null)
        {
            // Redireciona para a página de login
            context.Result = new RedirectToPageResult("/Login");
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
}
