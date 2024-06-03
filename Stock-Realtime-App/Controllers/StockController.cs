using Microsoft.AspNetCore.Mvc;
using Stock_Realtime_App.Services;

namespace Stock_Realtime_App.Controllers
{
    public class StockController : Controller
    {
        private readonly IStock _stock;

        public StockController(IStock stock)
        {
            _stock = stock;
        }

    public async Task <IActionResult> Index (string symbol = "AAPL")
        {
            var result = await _stock.GetStockDataAsync(symbol);
            return View(result);
        }
    }
}
