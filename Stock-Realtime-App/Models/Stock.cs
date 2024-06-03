﻿using System.ComponentModel.DataAnnotations;

namespace Stock_Realtime_App.Models
{
    public class Stock
    {

            [Key]
            public int Id { get; set; } 
            public string Symbol { get; set; }
            public decimal CurrentPrice { get; set; }
            public decimal HighPrice { get; set; }
            public decimal LowPrice { get; set; }
            public decimal OpenPrice { get; set; }
            public decimal PreviousClosePrice { get; set; }
            public DateTime StockTime { get; set; } 
        





    }
}
