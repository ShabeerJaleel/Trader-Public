﻿using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Futures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Futures.Socket;
using Kucoin.Net.Objects.Socket;
using Kucoin.Net.Objects.Spot.Socket;
using System.Linq;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.SubClients;

namespace Kucoin.Net.SocketSubClients
{
    /// <summary>
    /// Futures subscriptions
    /// </summary>
    public class KucoinSocketClientFutures: SocketClient, IKucoinSocketClientFutures
    {

        internal KucoinSocketClientFutures(KucoinSocketClientOptions options): base("Kucoin[Futures]", options, options.FuturesApiCredentials == null ? null : new KucoinAuthenticationProvider(options.FuturesApiCredentials))
        {
            MaxSocketConnections = 10;

            SendPeriodic(TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (messageEvent) => { });
            AddGenericHandler("Welcome", (messageEvent) => { });
        }

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                InvokeHandler(tokenData.As(GetData<KucoinStreamFuturesMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/execution:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to ticker updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                InvokeHandler(tokenData.As(GetData<KucoinStreamFuturesTick>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/tickerV2:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<JToken>(tokenData);
                var change = data["change"]?.ToString();
                if (string.IsNullOrEmpty(change))
                    return;

                var items = change!.Split(',');
                var result = new KucoinFuturesOrderBookChange
                {
                    Sequence = long.Parse(data["sequence"].ToString()),
                    Price = decimal.Parse(items[0], CultureInfo.InvariantCulture),
                    Side = items[1] == "sell" ? KucoinOrderSide.Sell: KucoinOrderSide.Buy,
                    Quantity = decimal.Parse(items[2], CultureInfo.InvariantCulture)
                };

                InvokeHandler(tokenData.As(result), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to partial order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var book = GetData<KucoinStreamOrderBookChanged>(tokenData);
                InvokeHandler(tokenData.As(book), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2Depth{limit}:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to market data updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol, 
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                if (tokenData.Data["subject"]?.ToString() == "mark.index.price")
                {
                    var data = GetData<KucoinStreamFuturesMarkIndexPrice>(tokenData);
                    InvokeHandler(tokenData.As(data), onMarkIndexPriceUpdate);
                }
                else
                {
                    var data = GetData<KucoinStreamFuturesFundingRate>(tokenData);
                    InvokeHandler(tokenData.As(data), onFundingRateUpdate);
                }
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/instrument:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe system announcement
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinContractAnnouncement>(tokenData);
                data.Event = tokenData.Data["subject"]?.ToString() ?? "";
                InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/announcement", false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to order updates
        /// </summary>
        /// <param name="symbol">[Optional] Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol, 
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamFuturesOrderUpdate>(tokenData);
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/tradeOrders" + (symbol == null ? "": ":" +symbol), true);
            return await SubscribeAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to stop order updates
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamFuturesStopOrderUpdate>(tokenData);
                InvokeHandler(tokenData.As((KucoinStreamStopOrderUpdateBase)data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/advancedOrders", false);
            return await SubscribeAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to snapshot updates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinStreamTransactionStatisticsUpdate>(tokenData);
                InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/snapshot:"+symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to wallet updates
        /// </summary>
        /// <param name="onOrderMarginUpdate">Data handler for order margin updates</param>
        /// <param name="onBalanceUpdate">Data handler for balance updates</param>
        /// <param name="onWithdrawableUpdate">Data handler for withdrawable funds updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync( 
            Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var subject = tokenData.Data["subject"]?.ToString();
                if (subject == "orderMargin.change")
                {
                    var data = GetData<KucoinStreamOrderMarginUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data), onOrderMarginUpdate);
                }
                else if (subject == "availableBalance.change")
                {
                    var data = GetData<KucoinStreamFuturesBalanceUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data), onBalanceUpdate);
                }
                else if (subject == "withdrawHold.change")
                {
                    var data = GetData<KucoinStreamFuturesWithdrawableUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data), onWithdrawableUpdate);
                }
                else
                    log.Write(LogLevel.Warning, "Unknown update: " + subject);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractAccount/wallet", false);
            return await SubscribeAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to position updates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string symbol, Action<DataEvent<KucoinPosition>> onData)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var data = GetData<KucoinPosition>(tokenData);
                InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/position:" + symbol, false);
            return await SubscribeAsync("futures", request, null, true, innerHandler).ConfigureAwait(false);
        }


        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler)
        {
            SocketConnection? socketConnection;
            SocketSubscription subscription;
            var released = false;
            await semaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                socketConnection = GetSocketConnection(url, authenticated);
                if (socketConnection == null)
                {
                    KucoinToken token;
                    var clientOptions = KucoinClient.DefaultOptions.Copy();
                    KucoinApiCredentials? thisCredentials = (KucoinApiCredentials?)authProvider?.Credentials;
                    if (thisCredentials != null)
                    {
                        clientOptions.FuturesApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                            thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                    }

                    // Create new socket
                    IWebsocket socket;
                    if (SocketFactory is WebsocketFactory)
                    {
                        using (var restClient = new KucoinClient(clientOptions))
                        {
                            WebCallResult<KucoinToken> tokenResult = await ((KucoinClientFutures)restClient.Futures).GetWebsocketToken(authenticated).ConfigureAwait(false);
                            if (!tokenResult)
                                return new CallResult<UpdateSubscription>(null, tokenResult.Error);
                            token = tokenResult.Data;
                        }

                        socket = CreateSocket(token.Servers.First().Endpoint + "?token=" + token.Token);
                    }
                    else
                        socket = CreateSocket("test");

                    socketConnection = new SocketConnection(this, socket);
                    foreach (var kvp in genericHandlers)
                        socketConnection.AddSubscription(SocketSubscription.CreateForIdentifier(NextId(), kvp.Key, false, kvp.Value));
                }

                subscription = AddSubscription(request, identifier, true, socketConnection, dataHandler);
                if (SocketCombineTarget == 1)
                {
                    // Can release early when only a single sub per connection
                    semaphoreSlim.Release();
                    released = true;
                }

                var connectResult = await ConnectIfNeededAsync(socketConnection, authenticated).ConfigureAwait(false);
                if (!connectResult)
                    return new CallResult<UpdateSubscription>(null, connectResult.Error);
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                if (!released)
                    semaphoreSlim.Release();
            }


            if (request != null)
            {
                var subResult = await SubscribeAndWaitAsync(socketConnection, request, subscription).ConfigureAwait(false);
                if (!subResult)
                {
                    await socketConnection.CloseAsync(subscription).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, subResult.Error);
                }

            }
            else
            {
                subscription.Confirmed = true;
            }

            socketConnection.ShouldReconnect = true;
            return new CallResult<UpdateSubscription>(new UpdateSubscription(socketConnection, subscription), null);
        }

        /// <inheritdoc />
        protected override SocketConnection GetSocketConnection(string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.SubscriptionCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            return null!;
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var kRequest = (KucoinRequest)request;
            if ((string)id != kRequest.Id)
                return false;

            var result = Deserialize<KucoinSubscribeResponse>(message, false);
            if (!result)
            {
                callResult = new CallResult<object>(null, result.Error);
                return true;
            }

            if (result.Data.Type != "ack")
            {
                callResult = new CallResult<object>(null, new ServerError(result.Data.Code, result.Data.Data));
                return true;
            }

            callResult = new CallResult<object>(result.Data, null);
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            if (message["type"] == null || (string)message["type"] != "message")
                return false;

            if (message["topic"] == null)
                return false;

            var kRequest = (KucoinRequest)request;
            var topic = (string)message["topic"];
            if (kRequest.Topic == topic)
                return true;

            if ((kRequest.Topic.StartsWith("/market/ticker:") && message["subject"] != null && (string)message["subject"] == "trade.ticker")
            || (kRequest.Topic.StartsWith("/market/level2:") && ((string)message["topic"]).StartsWith("/market/level2"))
            || (kRequest.Topic.StartsWith("/spotMarket/level3:") && ((string)message["topic"]).StartsWith("/spotMarket/level3"))
            || (kRequest.Topic.StartsWith("/spotMarket/level2Depth5:") && ((string)message["topic"]).StartsWith("/spotMarket/level2Depth5"))
            || (kRequest.Topic.StartsWith("/spotMarket/level2Depth20:") && ((string)message["topic"]).StartsWith("/spotMarket/level2Depth20"))
            || (kRequest.Topic.StartsWith("/indicator/index:") && ((string)message["topic"]).StartsWith("/indicator/index"))
            || (kRequest.Topic.StartsWith("/indicator/markPrice:") && ((string)message["topic"]).StartsWith("/indicator/markPrice"))
            || (kRequest.Topic.StartsWith("/market/snapshot:") && ((string)message["topic"]).StartsWith("/market/snapshot")))
            {
                var marketSplit = topic.Split(':');
                if (marketSplit.Length > 1)
                {
                    var market = marketSplit[1];
                    var subMarkets = kRequest.Topic.Split(':').Last().Split(',');
                    if (subMarkets.Contains(market))
                        return true;
                }
            }

            if (kRequest.Topic == "/account/balance" && message["subject"] != null && (string)message["subject"] == "account.balance")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message["type"] != null)
            {
                var type = (string)message["type"];
                if (type == "pong" && identifier == "Ping")
                    return true;

                if (type == "welcome" && identifier == "Welcome")
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            return Task.FromResult(new CallResult<bool>(true, null));
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            var kRequest = (KucoinRequest)s.Request!;
            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "unsubscribe", kRequest.Topic, false);

            var success = false;
            await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(5), message =>
            {
                var id = message["id"];
                if (id == null)
                    return false;

                if ((string)id != request.Id)
                    return false;

                var result = Deserialize<KucoinSubscribeResponse>(message, false);
                if (!result)
                {
                    log.Write(LogLevel.Warning, "Failed to unsubscribe: " + result.Error);
                    success = false;
                    return true;
                }

                if (result.Data.Type != "ack")
                {
                    log.Write(LogLevel.Warning, "Failed to unsubscribe: " + new ServerError(result.Data.Code, result.Data.Data));
                    success = false;
                    return true;
                }

                success = true;
                return true;
            }).ConfigureAwait(false);

            return success;
        }

        internal void InvokeHandler<T>(T data, Action<T> handler)
        {
            if (Equals(data, default(T)!))
                return;

            handler?.Invoke(data!);
        }

        internal T GetData<T>(DataEvent<JToken> tokenData)
        {
            var desResult = Deserialize<KucoinUpdateMessage<T>>(tokenData.Data, false);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
                return default!;
            }
            return desResult.Data.Data;
        }
    }
}
