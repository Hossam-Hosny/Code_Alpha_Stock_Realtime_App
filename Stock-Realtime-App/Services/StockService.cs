using Stock_Realtime_App.Models;

namespace Stock_Realtime_App.Services
{
    public class StockService:IStock
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly AppDbContext _db;

        public StockService(HttpClient httpClient, IConfiguration configuration, AppDbContext db)
        {
            _httpClient = httpClient;
            _apiKey = configuration["FinnhubApiKey"];
            _db = db;
        }



        public async Task<Stock> GetStockDataAsync(string symbol)
        {
            var response = await _httpClient.GetFromJsonAsync<FinnhubResponse>($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_apiKey}");

            var stock = new Stock {

                Symbol = symbol,
                CurrentPrice = response.c,
                HighPrice = response.h,
                LowPrice = response.l,
                OpenPrice = response.o,
                PreviousClosePrice = response.pc,
                StockTime = DateTime.UtcNow
            
            };

            _db.Stocks.Add(stock);
            await _db.SaveChangesAsync();
            return stock;
     


        }



    }
}
