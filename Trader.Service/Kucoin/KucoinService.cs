using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Spot;
using Trader.Service.Common;
using Trader.Service.Settings;

namespace Trader.Service.Kucoin
{
    public class KucoinService
    {
        private KucoinClient _client;
        private KucoinSocketClient _socketClient;
        private readonly ITradeLogger _logger;
        private volatile bool _isRunning;

        private KucoinOrderInfo _orderInfo;

        public KucoinService(KucoinSettings kucoinSettings, ITradeLogger tradeLogger)
        {
            _client = new KucoinClient(new KucoinClientOptions()
            {
                ApiCredentials = new KucoinApiCredentials(kucoinSettings.ApiKey, kucoinSettings.ApiSecret, kucoinSettings.PassPhrase),
            });

            _socketClient = new KucoinSocketClient(new KucoinSocketClientOptions()
            {
                ApiCredentials = new KucoinApiCredentials(kucoinSettings.ApiKey, kucoinSettings.ApiSecret, kucoinSettings.PassPhrase),
            });
            _logger = tradeLogger;
        }

        public bool IsRunning => _isRunning;

        public async Task GrabListing(KucoinOrderInfo orderInfo)
        {
            _orderInfo = orderInfo;
            _isRunning = true;
            await Subscribe();

            var tasks = new List<Task>();
            var i = 0;
            while (_isRunning)
            {
                var j = ++i;
                tasks.Add(PlaceOrder(j));
                Thread.Sleep(60);
            }

            await Task.WhenAll(tasks);
            _logger.Log($"Completed");
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private async Task Subscribe()
        {
            var result = await _socketClient.Spot.SubscribeToTickerUpdatesAsync(_orderInfo.Symbol, data => {
                if (data.Data.LastTradePrice != null && data.Data.LastTradePrice.Value > 0 &&
                    data.Data.LastTradePrice.Value != _orderInfo.LastPrice)
                {
                    var lp = data.Data.LastTradePrice.Value;
                    lock (_orderInfo)
                    {

                        var p = Math.Round(lp + (lp * 0.1M), 2);
                        _orderInfo.LastPrice = Math.Min(p, _orderInfo.MaxBuyPrice);
                    }
                    _logger.Log($"Last price event. Price: {_orderInfo.LastPrice}");
                }
            });
        }



        private async Task PlaceOrder(int index)
        {
            if (!_isRunning)
            {
                return;
            }

            var orderPrice = _orderInfo.LastPrice;
            var qty = decimal.ToInt32(_orderInfo.BuyAmount / orderPrice);

            if (qty < 1)
            {
                _logger.Log($"Cant buy 0 quantity");
                return;
            }

            var watch = new Stopwatch();
            watch.Start();
            _logger.Log($"{index}. Placing order. Price: {orderPrice}, Qty: {qty}");

            try
            {
                var order = await _client.Spot.PlaceOrderAsync(_orderInfo.Symbol,
                   Guid.NewGuid().ToString(),
                   KucoinOrderSide.Buy,
                   KucoinNewOrderType.Limit,
                   quantity: qty, price: orderPrice, timeInForce: KucoinTimeInForce.GoodTillCancelled);

                if (!string.IsNullOrWhiteSpace(order.Data?.OrderId))
                {
                    _isRunning = false;
                    watch.Stop();
                    _logger.Log($"{index}. Success: {order.Data.OrderId}. Duration: {watch.ElapsedMilliseconds}");
                    await VerifyOrderSuccess(order.Data.OrderId);
                }
                else
                {
                    _logger.Log($"{index}. Error: {order.Error.Message}. Duration: {watch.ElapsedMilliseconds}");
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"{index}. Error: {ex.Message}. Duration: {watch.ElapsedMilliseconds}");
            }
        }


        private async Task VerifyOrderSuccess(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return;
            }

            try
            {

                var account = await GetAccount("USDT");
                if (account != null)
                {
                    _logger.Log($"USDT - balance: {account.Balance}, available: {account.Available}");
                    _orderInfo.BuyAmount = account.Balance - 20;
                }

                var result = await _client.Spot.GetUserTradesAsync(orderId: orderId);
                if (result.Success && result.Data != null && result.Data.TotalItems > 0)
                {
                    _logger.Log($"!!ORDER SUCCESS {orderId}!!");
                }

                _ = _client.Spot.CancelAllOrdersAsync();
                _logger.Log($"Cancellation sent for {orderId}");
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public async Task<KucoinAccount> GetAccount(string currency)
        {
            var result = await _client.Spot.GetAccountsAsync(currency, KucoinAccountType.Trade);
            if (result.Success && result.Data?.Count() > 0)
            {
                return result.Data.Single();
            }

            return null;
        }

    }
}