using Stock_Realtime_App.Models;

namespace Stock_Realtime_App.Services
{
    public interface IStock
    {
        Task<Stock> GetStockDataAsync(string symbol);
    }
}
