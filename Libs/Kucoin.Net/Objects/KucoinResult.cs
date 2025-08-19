﻿using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects
{
    internal class KucoinResult<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg"), JsonOptionalProperty]
        public string? Message { get; set; }

        public T Data { get; set; } = default!;
    }
}
