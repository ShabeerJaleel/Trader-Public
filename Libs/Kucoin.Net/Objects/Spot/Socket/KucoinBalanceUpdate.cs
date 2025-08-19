﻿using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot.Socket
{
    /// <summary>
    /// Balance update info
    /// </summary>
    public class KucoinBalanceUpdate
    {
        /// <summary>
        /// The total balance
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// The available balance
        /// </summary>
        public decimal Available { get; set; }
        /// <summary>
        /// The change in available balance
        /// </summary>
        public decimal AvailableChange { get; set; }
        /// <summary>
        /// The currency this update is for
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The amount currently in hold
        /// </summary>
        public decimal Hold { get; set; }
        /// <summary>
        /// The change in holding
        /// </summary>
        public decimal HoldChange { get; set; }
        /// <summary>
        /// The event that caused the change
        /// </summary>
        public string RelationEvent { get; set; } = string.Empty;
        /// <summary>
        /// The cause id
        /// </summary>
        public string RelationEventId { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the update
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The id of the changed account
        /// </summary>
        public string AccountId { get; set; } = string.Empty;
    }
}
