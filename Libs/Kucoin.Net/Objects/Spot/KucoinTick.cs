﻿using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Tick info
    /// </summary>
    public class KucoinTick
    {
        /// <summary>
        /// The sequence of the tick
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The price of the last trade
        /// </summary>
        [JsonProperty("price")]
        public decimal? LastTradePrice { get; set; }
        /// <summary>
        /// The quantity of the last trade
        /// </summary>
        [JsonProperty("size")]
        public decimal? LastTradeQuantity { get; set; }
        /// <summary>
        /// The best ask price
        /// </summary>
        public decimal? BestAsk { get; set; }
        /// <summary>
        /// The quantity of the best ask price
        /// </summary>
        [JsonProperty("bestAskSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// The best bid price
        /// </summary>
        public decimal? BestBid { get; set; }
        /// <summary>
        /// The quantity of the best bid
        /// </summary>
        [JsonProperty("bestBidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
