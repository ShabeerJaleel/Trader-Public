﻿using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Historical withdrawal info
    /// </summary>
    public class KucoinHistoricalWithdrawal
    {
        /// <summary>
        /// The currency of the withdrawal
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The address the withdrawal was to
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public KucoinWithdrawalStatus Status { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The time the withdrawal was created
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// Whether it was an internal withdrawal
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// The quantity of the withdrawal
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
