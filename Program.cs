using System.Diagnostics;

namespace WhiteoutSurvival_Bot
{
    public static class Program
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private static Logging.Logging logging = new Logging.Logging();
        private static Settings.Configuration configuration = new Settings.Configuration();

        private static DeviceControl.AdbCommandExecutor adbCommandExecutor = new DeviceControl.AdbCommandExecutor(logging);
        private static TextReader.TextRecogntion textRecogntion = new TextReader.TextRecogntion(adbCommandExecutor);
        private static DeviceControl.AdbControl adbControl = new DeviceControl.AdbControl(adbCommandExecutor);
        private static DeviceControl.NoxControl noxControl = new DeviceControl.NoxControl(adbCommandExecutor);
        private static DeviceControl.PcControl pcControl = new DeviceControl.PcControl();
        private static DeviceControl.AppControl appControl = new DeviceControl.AppControl(adbCommandExecutor);
        private static DeviceControl.GameControl gameControl = new DeviceControl.GameControl(logging, adbCommandExecutor, noxControl, textRecogntion, appControl, gameScore);
        
        private static Settings.GameScore gameScore = new Settings.GameScore();
        private static Settings.GameSettings gameSettings = new Settings.GameSettings();
        private static Stability.Stability stability = new Stability.Stability(logging, pcControl, noxControl, adbControl, gameControl, appControl);

        private static GameAutomations.Geheimdienst geheimdienst = new GameAutomations.Geheimdienst(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.TruppenHeilen truppenHeilen = new GameAutomations.TruppenHeilen(logging, gameControl, textRecogntion);
        private static GameAutomations.Erkundung erkundung = new GameAutomations.Erkundung(logging, textRecogntion, gameControl, gameScore);
        private static GameAutomations.Arena arena = new GameAutomations.Arena(logging, gameControl, gameScore);
        private static GameAutomations.Allianz allianz = new GameAutomations.Allianz(logging, gameControl, gameSettings, gameScore);
        private static GameAutomations.GuvenourBefehl guvenourBefehl = new GameAutomations.GuvenourBefehl(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.Helden helden = new GameAutomations.Helden(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.Jagt jagt = new GameAutomations.Jagt(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.LagerOnlineBelohnung lagerOnlineBelohnung = new GameAutomations.LagerOnlineBelohnung(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.LebensBaum lebensBaum = new GameAutomations.LebensBaum(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.TruppenTraining truppenTraining = new GameAutomations.TruppenTraining(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);
        private static GameAutomations.VIP vip = new GameAutomations.VIP(logging, gameControl, gameSettings, gameScore, textRecogntion, noxControl, adbCommandExecutor);

        private static Development.DevlopHelper devlopHelper = new Development.DevlopHelper(logging, adbCommandExecutor);


        internal static void Main()
        {
            //stability.CheckStability();
            while (true)
            {
                ErrorHandler.ErrorHandler.Handle(() =>
                {
                    try
                    {
                        logging.LogAndConsoleWirite(gameScore.GetAllCounters());

                        stopwatch.Restart();
                        stability.CheckStability();
                        geheimdienst.StartProcess();
                        Time();

                        stopwatch.Restart();
                        stability.CheckStability();
                        erkundung.StartProcess();
                        Time();

                        stopwatch.Restart();
                        stability.CheckStability();
                        truppenHeilen.Heilen();
                        Time();

                        allianz.Hilfe();
                        allianz.KistenAbholen();
                        allianz.AutobeitritAktivieren();
                        allianz.TechnologieBeitrag(1);

                        arena.GoToArena();
                        
                        Time();
                    }
                    catch (Exception e)
                    {
                        logging.LogAndConsoleWirite(e.Message);
                    }

                }, "Main: ");
            }

        }


        internal static void Time()
        {
            logging.LogAndConsoleWirite("_____________________________________________________________________________");
            stopwatch.Stop();
            double elapsedMinutes = stopwatch.Elapsed.TotalMinutes;
            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            logging.PrintFormatted($"Verstrichene Zeit: ", $"{elapsedMinutes:F2} Minuten");
            logging.PrintFormatted($"Verstrichene Zeit: ", $"{elapsedSeconds:F2} Sekunden");
            logging.PrintFormatted($"Verstrichene Gesamt Zeit: ", $"{elapsedSeconds + elapsedMinutes:F2}");
            logging.LogAndConsoleWirite("_____________________________________________________________________________");
        }

    }
}
