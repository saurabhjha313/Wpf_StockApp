using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketClient
{
    private ClientWebSocket webSocket = null;

    public async Task ConnectAsync(Uri serverUri)
    {
        webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(serverUri, CancellationToken.None);
        await ReceiveAsync();
    }

    private async Task ReceiveAsync()
    {
        byte[] buffer = new byte[2048];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                // Process the message and update the UI
                UpdateUI(message);
            }
        }
    }

    private void UpdateUI(string message)
    {
        // This method should marshal the call to the UI thread
        // and update the UI with the new data
        // Application.Current.Dispatcher.Invoke(() => { /* Update UI */ });
    }
}
