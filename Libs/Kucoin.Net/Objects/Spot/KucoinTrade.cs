﻿using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class KucoinTrade: ICommonRecentTrade
    {
        /// <summary>
        /// The sequence number of the trade
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The side of the trade
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonConverter(typeof(TimestampNanoSecondsConverter)), JsonProperty("time")]
        public DateTime Timestamp { get; set; }

        decimal ICommonRecentTrade.CommonPrice => Price;
        decimal ICommonRecentTrade.CommonQuantity => Quantity;
        DateTime ICommonRecentTrade.CommonTradeTime => Timestamp;
    }
}
