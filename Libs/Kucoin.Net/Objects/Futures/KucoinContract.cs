﻿using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Contract info
    /// </summary>
    public class KucoinContract
    {
        /// <summary>
        /// Base currency
        /// </summary>
        public string BaseCurrency { get; set; } = string.Empty;
        /// <summary>
        /// Fair method
        /// </summary>
        public string FairMethod { get; set; } = string.Empty;
        /// <summary>
        /// Funding base symbol
        /// </summary>
        public string FundingBaseSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding quote symbol
        /// </summary>
        public string FundingQuoteSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate symbol
        /// </summary>
        public string FundingRateSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Index symbol
        /// </summary>
        public string IndexSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Initial margin
        /// </summary>
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Enabled ADL or not
        /// </summary>
        public bool IsDeleverage { get; set; }
        /// <summary>
        /// Reverse contract or not
        /// </summary>
        public bool IsInverse { get; set; }
        /// <summary>
        /// Quanto or not
        /// </summary>
        public bool IsQuanto { get; set; }
        /// <summary>
        /// Minimum lot size
        /// </summary>
        public decimal LotSize { get; set; }
        /// <summary>
        /// Maintenance margin requirement
        /// </summary>
        public decimal MaintainMargin { get; set; }
        /// <summary>
        /// Maker fee
        /// </summary>
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Fixed maker fee
        /// </summary>
        public decimal MakerFixFee { get; set; }
        /// <summary>
        /// Marking method
        /// </summary>
        public string MarkMethod { get; set; } = string.Empty;
        /// <summary>
        /// Maximum order quantity
        /// </summary>
        [JsonProperty("maxOrderQty")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Maximum price
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// Maximum risk limit
        /// </summary>
        public decimal MaxRiskLimit { get; set; }
        /// <summary>
        /// Minimum risk limit
        /// </summary>
        public decimal MinRiskLimit { get; set; }
        /// <summary>
        /// Contract multiplier
        /// </summary>
        public decimal Multiplier { get; set; }
        /// <summary>
        /// Quote currency
        /// </summary>
        public string QuoteCurrency { get; set; } = string.Empty;
        /// <summary>
        /// Risk limit increment
        /// </summary>
        public decimal RiskStep { get; set; }
        /// <summary>
        /// Contract group
        /// </summary>
        public string RootSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Taker fee
        /// </summary>
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Taker fixed fee
        /// </summary>
        public decimal TakerFixFee { get; set; }
        /// <summary>
        /// Minimum price change
        /// </summary>
        public decimal TickSize { get; set; }
        /// <summary>
        /// Type of contract
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Maximum leverage
        /// </summary>
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// 24 hour volume
        /// </summary>
        [JsonProperty("volumeOf24h")]
        public decimal Volume24H { get; set; }
        /// <summary>
        /// 24 hour turnover
        /// </summary>
        [JsonProperty("turnoverOf24h")]
        public decimal Turnover24H { get; set; }
        /// <summary>
        /// Open interest
        /// </summary>
        public decimal OpenInterest { get; set; }//TODO
        /// <summary>
        /// 24 hour low price
        /// </summary>
        public decimal LowPrice { get; set; }
        /// <summary>
        /// 24 hour high price
        /// </summary>
        public decimal HighPrice { get; set; }
        /// <summary>
        /// 24 hour change percentage
        /// </summary>
        [JsonProperty("priceChgPct")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// 24 hour change
        /// </summary>
        [JsonProperty("priceChg")]
        public decimal PriceChange { get; set; }
    }
}
