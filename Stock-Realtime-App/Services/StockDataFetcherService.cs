using Microsoft.AspNetCore.SignalR;
using Stock_Realtime_App.Hubs;

namespace Stock_Realtime_App.Services
{
    public class StockDataFetcherService : BackgroundService
    {
        private readonly ILogger<StockDataFetcherService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHubContext<StockHub> _hubContext;
        private readonly string _apiKey;
        private readonly IServiceProvider _services;

        public StockDataFetcherService(ILogger<StockDataFetcherService> logger, IHttpClientFactory httpClientFactory, IHubContext<StockHub> hubContext, IConfiguration configuration, IServiceProvider services)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _hubContext = hubContext;
            _apiKey = configuration["FinnhubApiKey"];
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var symbol = "AAPL";
                    using (var scope = _services.CreateScope())
                    {
                        var stockService = scope.ServiceProvider.GetRequiredService<IStock>();
                        var stockData = await stockService.GetStockDataAsync(symbol);
                        await _hubContext.Clients.All.SendAsync("ReceiveStockUpdate", stockData.Symbol, stockData.CurrentPrice);
                    }

                    // Wait for a specific interval before fetching data again (e.g., every 10 seconds)
                    await Task.Delay(TimeSpan.FromSeconds(4), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching stock data.");
                }
            }
        }

    }
}
