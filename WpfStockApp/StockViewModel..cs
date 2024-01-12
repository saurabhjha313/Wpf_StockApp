using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using Newtonsoft.Json;
using System.Linq;



namespace WpfStockApp
{
    public class StockViewModel
    {
        public ObservableCollection<StockData> Stocks { get; set; } = new ObservableCollection<StockData>();

        private ClientWebSocket webSocket;

        public async Task ConnectToWebSocketAsync()
        {
            webSocket = new ClientWebSocket();
            try
            {
                await webSocket.ConnectAsync(new Uri("wss://example.com/websocket"), CancellationToken.None);

                while (webSocket.State == WebSocketState.Open)
                {
                    var buffer = new byte[1024 * 4];
                    WebSocketReceiveResult result;
                    while ((result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None)).Count > 0)
                    {
                        string jsonString = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        // Process the jsonString to update Stocks
                        UpdateStocksCollection(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"WebSocket error: {ex.Message}");
            }
            finally
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    // Properly close the WebSocket
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", CancellationToken.None);
                }
            }
        }

        public void ExecuteTrade()
        {
           
            Console.WriteLine("Executing a trade in StockViewModel.");
        }

        private void UpdateStocksCollection(string jsonString)
        {
            
            var stockUpdate = JsonConvert.DeserializeObject<StockData>(jsonString);

           
            Application.Current.Dispatcher.Invoke(() =>
            {
                
                var stock = Stocks.FirstOrDefault(s => s.Symbol == stockUpdate.Symbol);
                if (stock != null)
                {
                   
                    stock.Price = stockUpdate.Price;
                    stock.Change = stockUpdate.Change;
                    
                }
                else
                {
                    
                    Stocks.Add(stockUpdate);
                }
            });
        }
    }
}


