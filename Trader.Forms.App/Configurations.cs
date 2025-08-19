using Microsoft.Extensions.Configuration;
using Trader.Service.Settings;

namespace Trader.Forms.App
{
    public class Configurations
    {
        private readonly IConfigurationRoot _config;
        private readonly GateSettings _gateSettings = new GateSettings();
        private readonly KucoinSettings _kucoinSettings = new KucoinSettings();
        private static Configurations _instance;

        static Configurations()
        {
            _instance = new Configurations();
        }

        private Configurations()
        {
            _config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();
            _config.GetSection("GateSettings").Bind(_gateSettings);
            _config.GetSection("KucoinSettings").Bind(_kucoinSettings);
        }

        public GateSettings GateSettings => _gateSettings;

        public KucoinSettings KucoinSettings => _kucoinSettings;

        public static Configurations Instance => _instance;
    }
}
