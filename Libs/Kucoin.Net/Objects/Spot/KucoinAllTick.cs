﻿using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    public class KucoinAllTick: ICommonTicker
    {
        /// <summary>
        /// The symbol of the tick
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The best ask price
        /// </summary>
        [JsonProperty("sell")]
        public decimal? BestAsk { get; set; }
        /// <summary>
        /// The best bid price
        /// </summary>
        [JsonProperty("buy")]
        public decimal? BestBid { get; set; }
        /// <summary>
        /// The percentage change
        /// </summary>
        [JsonProperty("changeRate")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// The price change
        /// </summary>
        public decimal? ChangePrice { get; set; }
        /// <summary>
        /// The highest price
        /// </summary>
        public decimal? High { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        public decimal? Low { get; set; }
        /// <summary>
        /// The volume in this tick
        /// </summary>
        [JsonProperty("vol")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The value of the volume in this tick
        /// </summary>
        [JsonProperty("volValue")]
        public decimal? VolumeValue { get; set; }
        /// <summary>
        /// The last trade price
        /// </summary>
        public decimal? Last { get; set; }

        string ICommonTicker.CommonSymbol => Symbol;
        decimal ICommonTicker.CommonHigh => High ?? 0;
        decimal ICommonTicker.CommonLow => Low ?? 0;
        decimal ICommonTicker.CommonVolume => Volume ?? 0;
    }
}
