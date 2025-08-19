using System;

namespace Trader.Service.Kucoin
{
    public class KucoinOrderInfo
    {
        public string Symbol { get; set; }

        public decimal LastPrice { get; set; }

        public decimal MaxBuyPrice { get; set; }

        public decimal BuyAmount { get; set; }
    }
}
