using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Io.Gate.GateApi.Api;
using Io.Gate.GateApi.Client;
using Io.Gate.GateApi.Model;
using Trader.Service.Common;
using Trader.Service.Settings;

namespace Trader.Service.Gate
{
    public class GateService
    {
        private readonly SpotApi _spotApi;
        private readonly Configuration config = new Configuration();
        private readonly ITradeLogger _logger;
        private volatile bool _isRunning;

        public GateService(GateSettings gateSettings, ITradeLogger tradeLogger)
        {
            config.SetGateApiV4KeyPair(gateSettings.ApiKey, gateSettings.ApiSecret);
            _spotApi = new SpotApi(config);
            _logger = tradeLogger;
        }
        
        public async Task GrabListing(string symbol, decimal price, decimal orderAmount)
        {
            _isRunning = true;
            var order = new Order(
                currencyPair: symbol,
                amount: orderAmount.ToString(CultureInfo.InvariantCulture),
                price: price.ToString(CultureInfo.InvariantCulture))
            {
                Account = Order.AccountEnum.Spot,
                Side = Order.SideEnum.Buy,
            };

            var tasks = new List<Task>();
            var i = 0;
            while (_isRunning)
            {
                var j = ++i;
                tasks.Add(PlaceOrder(order, j));
            }

            await Task.WhenAll(tasks);
            _logger.Log($"Completed");
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public bool IsRunning => _isRunning;

        private async Task PlaceOrder(Order order, int index)
        {
            if (!_isRunning)
            {
                return;
            }

            var watch = new Stopwatch();
            watch.Start();
            try
            {
                _logger.Log($"{index}. Placing order");
                var created = await _spotApi.CreateOrderAsync(order);
                _isRunning = false;
                watch.Stop();
                _logger.Log($"{index}. Success: {created.Id}. Duration: {watch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                watch.Stop();
                _logger.Log($"{index}. Error: {ex.Message}. Duration: {watch.ElapsedMilliseconds}");
            }
        }

        private void Test()
        {
            const string currencyPair = "MDF_USDT";
            string currency = currencyPair.Split('_')[1];

            CurrencyPair pair = _spotApi.GetCurrencyPair(currencyPair);
            Console.WriteLine("testing against currency pair: {0}", currencyPair);
            string minAmount = pair.MinQuoteAmount;

            List<Ticker> tickers = _spotApi.ListTickers(currencyPair);
            Debug.Assert(tickers.Count == 1);
            string lastPrice = tickers[0].Last;
            Debug.Assert(lastPrice != null);

            decimal orderAmount = Convert.ToDecimal(minAmount) * 2;
            List<SpotAccount> accounts = _spotApi.ListSpotAccounts(currency);
            Debug.Assert(accounts.Count == 1);
            decimal available = Convert.ToDecimal(accounts[0].Available);
            Console.WriteLine("Account available: {0} {1}", available, currency);
            if (available.CompareTo(orderAmount) < 0)
            {
                Console.Error.WriteLine("Account balance not enough");
                return;
            }

        }
    }
}