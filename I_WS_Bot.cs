using Microsoft.Extensions.DependencyInjection;
using WhiteoutSurvival_Bot.DeviceControl;
using WhiteoutSurvival_Bot.GameAutomations;
using WhiteoutSurvival_Bot.Settings;
using WhiteoutSurvival_Bot.StableControl;
using WhiteoutSurvival_Bot.Log;
using WhiteoutSurvival_Bot.TextReader;
using WhiteoutSurvival_Bot.Development;


namespace WhiteoutSurvival_Bot
{
    public class ServicesInitializer
    {
        public Log.Logging Logging { get; private set; }
        public Settings.Configuration Configuration { get; private set; }
        public Settings.GameSettings GameSettings { get; private set; }
        public Settings.GameScore GameScore { get; private set; }
        public DeviceControl.AdbCommandExecutor AdbCommandExecutor { get; private set; }
        public TextReader.TextRecogntion TextRecogntion { get; private set; }
        public DeviceControl.AdbControl AdbControl { get; private set; }
        public DeviceControl.NoxControl NoxControl { get; private set; }
        public DeviceControl.PcControl PcControl { get; private set; }
        public DeviceControl.AppControl AppControl { get; private set; }
        public DeviceControl.GameControl GameControl { get; private set; }
        public StableControl.Stability Stability { get; private set; }
        public Development.DevlopHelper DevlopHelper { get; private set; }

        public GameAutomations.Geheimdienst Geheimdienst { get; private set; }
        public GameAutomations.TruppenHeilen TruppenHeilen { get; private set; }
        public GameAutomations.Erkundung Erkundung { get; private set; }
        public GameAutomations.Arena Arena { get; private set; }
        public GameAutomations.Allianz Allianz { get; private set; }
        public GameAutomations.GuvenourBefehl GuvenourBefehl { get; private set; }
        public GameAutomations.Helden Helden { get; private set; }
        public GameAutomations.Jagt Jagt { get; private set; }
        public GameAutomations.LagerOnlineBelohnung LagerOnlineBelohnung { get; private set; }
        public GameAutomations.LebensBaum LebensBaum { get; private set; }
        public GameAutomations.TruppenTraining TruppenTraining { get; private set; }
        public GameAutomations.VIP VIP { get; private set; }

        public ServicesInitializer()
        {
            InitializeAllServices();
        }

        private void InitializeAllServices()
        {
            Logging = new Log.Logging();
            Configuration = new Settings.Configuration();
            GameSettings = new Settings.GameSettings();
            GameScore = new Settings.GameScore();
            AdbCommandExecutor = new DeviceControl.AdbCommandExecutor(Logging);
            TextRecogntion = new TextReader.TextRecogntion(AdbCommandExecutor);
            AdbControl = new DeviceControl.AdbControl(AdbCommandExecutor);
            NoxControl = new DeviceControl.NoxControl(AdbCommandExecutor);
            PcControl = new DeviceControl.PcControl();
            AppControl = new DeviceControl.AppControl(AdbCommandExecutor);
            GameControl = new DeviceControl.GameControl(Logging, AdbCommandExecutor, NoxControl, TextRecogntion, AppControl, GameScore);
            Stability = new StableControl.Stability(Logging, PcControl, NoxControl, AdbControl, GameControl, AppControl);
            DevlopHelper = new Development.DevlopHelper(Logging, AdbCommandExecutor);

            Geheimdienst = new GameAutomations.Geheimdienst(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            TruppenHeilen = new GameAutomations.TruppenHeilen(Logging, GameControl, TextRecogntion);
            Erkundung = new GameAutomations.Erkundung(Logging, TextRecogntion, GameControl, GameScore);
            Arena = new GameAutomations.Arena(Logging, GameControl, GameScore);
            Allianz = new GameAutomations.Allianz(Logging, GameControl, GameSettings, GameScore);
            GuvenourBefehl = new GameAutomations.GuvenourBefehl(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            Helden = new GameAutomations.Helden(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            Jagt = new GameAutomations.Jagt(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            LagerOnlineBelohnung = new GameAutomations.LagerOnlineBelohnung(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            LebensBaum = new GameAutomations.LebensBaum(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            TruppenTraining = new GameAutomations.TruppenTraining(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
            VIP = new GameAutomations.VIP(Logging, GameControl, GameSettings, GameScore, TextRecogntion, NoxControl, AdbCommandExecutor);
        }
    }

}
