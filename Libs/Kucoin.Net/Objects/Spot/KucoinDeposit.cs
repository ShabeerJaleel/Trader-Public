﻿using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Deposit info
    /// </summary>
    public class KucoinDeposit
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// A memo for this deposit
        /// </summary>
        public string Memo { get; set; } = string.Empty;
        /// <summary>
        /// The amount of the deposit
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The fee of the deposit
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The currency of the deposit
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Whether it is an internal deposit
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The deposit status
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public KucoinDepositStatus Status { get; set; }
        /// <summary>
        /// When the deposit was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// When the deposit was last updated
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
