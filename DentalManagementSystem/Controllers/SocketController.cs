using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace DentalManagementSystem.Controllers
{
    public class SocketController : ControllerBase
    {
        private static Dictionary<int, WebSocket> ClientDictionary = new Dictionary<int, WebSocket>();
        [Route("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                buffer = new byte[1024 * 4];
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                string Message = Encoding.UTF8.GetString(buffer);
                if (Message.Contains("Setup:"))
                {
                    int Id = int.Parse(Message.Split(':')[1]);
                    ClientDictionary[Id] = webSocket;
                }
                else
                {
                    foreach (var client in ClientDictionary.Keys)
                    {
                        await ClientDictionary[client].SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);
                    }
                }

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
