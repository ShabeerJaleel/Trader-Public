﻿using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using Kucoin.Net.Converters;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public class KucoinTransfer
    {
        /// <summary>
        /// Apply id
        /// </summary>
        public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Status of the transfer
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public KucoinDepositStatus Status { get; set; }
        /// <summary>
        /// Amount of the transfer
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Reason if failed
        /// </summary>
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Offset
        /// </summary>
        public long Offset { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
