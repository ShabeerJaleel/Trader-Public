﻿using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class SelfTradePreventionConverter : BaseConverter<KucoinSelfTradePrevention>
    {
        public SelfTradePreventionConverter() : this(true) { }
        public SelfTradePreventionConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinSelfTradePrevention, string>> Mapping => new List<KeyValuePair<KucoinSelfTradePrevention, string>>
        {
            new KeyValuePair<KucoinSelfTradePrevention, string>(KucoinSelfTradePrevention.None, ""),
            new KeyValuePair<KucoinSelfTradePrevention, string>(KucoinSelfTradePrevention.DecreaseAndCancel, "DC"),
            new KeyValuePair<KucoinSelfTradePrevention, string>(KucoinSelfTradePrevention.CancelOldest, "CO"),
            new KeyValuePair<KucoinSelfTradePrevention, string>(KucoinSelfTradePrevention.CancelNewest, "CN"),
            new KeyValuePair<KucoinSelfTradePrevention, string>(KucoinSelfTradePrevention.CancelBoth, "CB"),
        };
    }
}
