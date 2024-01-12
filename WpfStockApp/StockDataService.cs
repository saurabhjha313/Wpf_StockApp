using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class StockDataService
{
    private readonly string _apiKey;
    private const string BaseUrl = "https://www.alphavantage.co/query";

    public StockDataService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<List<StockDataPoint>> GetHistoricalDataAsync(string symbol, string interval)
    {
        var url = $"{BaseUrl}?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={interval}&apikey={_apiKey}";

        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync(url);
            var data = JObject.Parse(response)["Time Series (5min)"].Children();

            var stockDataPoints = new List<StockDataPoint>(); // Initialize the list here

            foreach (var item in data)
            {
                var time = DateTime.Parse(item.Path);
                var info = item.First;
                stockDataPoints.Add(new StockDataPoint
                {
                    Date = time,
                    Open = (double)info["1. open"],
                    High = (double)info["2. high"],
                    Low = (double)info["3. low"],
                    Close = (double)info["4. close"],
                    Volume = (double)info["5. volume"]
                });
            }

            return stockDataPoints; // Add this return statement
        }
    }

}

