using Microsoft.AspNetCore.SignalR;

namespace RazorLive.Infra
{
    public class VideoHub : Hub
    {
        public async Task SendAdminStream(string streamData)
        {
            // Envia a stream do administrador para todos os clientes conectados
            await Clients.Others.SendAsync("ReceiveAdminStream", streamData);
        }
    }
}
