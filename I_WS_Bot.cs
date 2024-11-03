using Microsoft.Extensions.DependencyInjection;
using WhiteoutSurvival_Bot.DeviceControl;
using WhiteoutSurvival_Bot.GameAutomations;
using WhiteoutSurvival_Bot.Settings;
using WhiteoutSurvival_Bot.StableControl;
using WhiteoutSurvival_Bot.Log;


namespace WhiteoutSurvival_Bot
{
    internal class I_WS_Bot
    {
        // Diese Methode konfiguriert und erstellt den DI-Container und gibt ihn zurück
        public IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // Hinzufügen der Dienste zum DI-Container
            services.AddSingleton<Logging>();
            services.AddSingleton<Configuration>();
            services.AddSingleton<GameSettings>();
            services.AddSingleton<GameScore>();

            services.AddSingleton<AdbCommandExecutor>();
            services.AddSingleton<TextReader.TextRecogntion>();
            services.AddSingleton<AdbControl>();
            services.AddSingleton<NoxControl>();
            services.AddSingleton<PcControl>();
            services.AddSingleton<AppControl>();
            services.AddSingleton<GameControl>();

            services.AddSingleton<Stability>();
            services.AddSingleton<Geheimdienst>();
            services.AddSingleton<TruppenHeilen>();
            services.AddSingleton<Erkundung>();
            services.AddSingleton<Arena>();
            services.AddSingleton<Allianz>();
            services.AddSingleton<GuvenourBefehl>();
            services.AddSingleton<Helden>();
            services.AddSingleton<Jagt>();
            services.AddSingleton<LagerOnlineBelohnung>();
            services.AddSingleton<LebensBaum>();
            services.AddSingleton<TruppenTraining>();
            services.AddSingleton<VIP>();

            services.AddSingleton<Development.DevlopHelper>();


            return services.BuildServiceProvider();
        }
    }
}
