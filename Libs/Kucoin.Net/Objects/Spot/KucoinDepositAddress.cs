﻿namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public class KucoinDepositAddress
    {
        /// <summary>
        /// The address
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// A memo for the address
        /// </summary>
        public string Memo { get; set; } = string.Empty;

        /// <summary>
        /// The chain of the address
        /// </summary>
        public string Chain { get; set; } = string.Empty;

        /// <summary>
        /// The token contract address
        /// </summary>
        public string ContractAddress { get; set; } = string.Empty;
    }
}
