namespace Trader.Service.Common
{
    public interface ITradeLogger
    {
        void Log(string message);

        void Stop();
    }
}
