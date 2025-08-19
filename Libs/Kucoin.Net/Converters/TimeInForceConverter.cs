﻿using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class TimeInForceConverter : BaseConverter<KucoinTimeInForce>
    {
        public TimeInForceConverter() : this(true) { }
        public TimeInForceConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinTimeInForce, string>> Mapping => new List<KeyValuePair<KucoinTimeInForce, string>>
        {
            new KeyValuePair<KucoinTimeInForce, string>(KucoinTimeInForce.GoodTillCancelled, "GTC"),
            new KeyValuePair<KucoinTimeInForce, string>(KucoinTimeInForce.GoodTillTime, "GTT"),
            new KeyValuePair<KucoinTimeInForce, string>(KucoinTimeInForce.ImmediateOrCancel, "IOC"),
            new KeyValuePair<KucoinTimeInForce, string>(KucoinTimeInForce.FillOrKill, "FOK"),
        };
    }
}
