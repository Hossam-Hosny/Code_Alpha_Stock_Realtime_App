using Microsoft.AspNetCore.SignalR;
using Stock_Realtime_App.Models;
using Stock_Realtime_App.Services;

namespace Stock_Realtime_App.Hubs
{

    public class StockHub: Hub
    {
        private readonly IStock _stock;

        public StockHub(IStock stock)
        {
            _stock = stock;
        }

        public async Task SendStockUpdateAsync(string symbol )
        {
          var stockData =  await _stock.GetStockDataAsync(symbol);
            await Clients.All.SendAsync("ReceiveStockUpdate", symbol, stockData);



            //var stockService = Context.GetHttpContext().RequestServices.GetService<IStock>();
            //await stockService.GetStockDataAsync(symbol);
            //await Clients.All.SendAsync("ReceiveStockUpdate", symbol, price);
        }
    }
}
