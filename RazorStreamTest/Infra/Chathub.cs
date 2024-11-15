namespace RazorStreamTest.Infra
{
    using Microsoft.AspNetCore.SignalR;
    using RazorStreamTest.Data;
    using RazorStreamTest.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using System;

    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            _chatService = new ChatService();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendMessage(string message)
        {
            // Obtém o nome do usuário da sessão
            var userName = _httpContextAccessor.HttpContext.Session.GetString("Username");

            // Verifica se o usuário está autenticado
            if (string.IsNullOrEmpty(userName))
            {
                // Redireciona ou envia uma mensagem de erro se o usuário não estiver logado
                await Clients.Caller.SendAsync("Error", "Usuário não autenticado.");
                return;
            }

            var chatMessage = new ChatMessage
            {
                UserName = userName,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            // Salva a mensagem no MongoDB
            await _chatService.SaveMessageAsync(chatMessage);

            // Envia a mensagem para todos os clientes conectados
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }

        public override async Task OnConnectedAsync()
        {
            var messages = await _chatService.GetMessagesAsync();
            foreach (var msg in messages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", msg.UserName, msg.Message);
            }

            await base.OnConnectedAsync();
        }
    }
}
