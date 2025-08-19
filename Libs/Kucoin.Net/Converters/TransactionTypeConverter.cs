﻿using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Converters
{
    internal class TransactionTypeConverter : BaseConverter<TransactionType>
    {
        public TransactionTypeConverter() : this(true) { }
        public TransactionTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<TransactionType, string>> Mapping => new List<KeyValuePair<TransactionType, string>>
        {
            new KeyValuePair<TransactionType, string>(TransactionType.RealisedPnl, "RealisedPNL"),
            new KeyValuePair<TransactionType, string>(TransactionType.Deposit, "Deposit"),
            new KeyValuePair<TransactionType, string>(TransactionType.Withdrawal, "Withdrawal"),
            new KeyValuePair<TransactionType, string>(TransactionType.TransferIn, "Transferin"),
            new KeyValuePair<TransactionType, string>(TransactionType.TransferOut, "Transferout")
        };
    }
}
