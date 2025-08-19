﻿namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Futures account overview
    /// </summary>
    public class KucoinAccountOverview
    {
        /// <summary>
        /// Account equity = marginBalance + Unrealised PNL 
        /// </summary>
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// Unrealised profit and loss
        /// </summary>
        public decimal UnrealisedPnl { get; set; }
        /// <summary>
        /// Margin balance = positionMargin + orderMargin + frozenFunds + availableBalance
        /// </summary>
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Order margin
        /// </summary>
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// Frozen funds for withdrawal and out-transfer
        /// </summary>
        public decimal FrozenFunds { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
    }
}
