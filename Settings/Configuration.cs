namespace WhiteoutSurvival_Bot.Settings
{
    public class Configuration
    {
        // Deklaration der Felder
        internal string BaseDirectory { get; private set; }
        internal int CommandDelay { get; private set; }
        internal int ReconnectSleepTime { get; private set; }
        internal int LoopCounter { get; private set; }

        internal string TrainedDataDirectory { get; private set; }
        internal string ScreenshotDirectory { get; private set; }
        internal string LogFileFolderPath { get; private set; }
        internal string LogFileFolderPathWithName { get; private set; }
        internal string LocalScreenshotPath { get; private set; }

        internal string AdbPath { get; private set; }
        internal string NoxExePath { get; private set; }
        internal string DeviceIP { get; private set; }
        internal string InputDevice { get; private set; }
        internal string PackageName { get; private set; }


        // Konstruktor für die Initialisierung aller Felder
        public Configuration()
        {
            // Basisverzeichnis bereinigen und festlegen
            Action action = () => BaseDirectory = AppContext.BaseDirectory.Replace("bin\\Debug\\net8.0\\", "");
            action(); // Führt die zugewiesene Methode aus, die das BaseDirectory bereinigt und setzt

            // Initialisieren der Standardwerte
            CommandDelay = 500;
            ReconnectSleepTime = 10;
            LoopCounter = 0;

            // Verzeichnispfade auf Basis des bereinigten Basisverzeichnisses
            TrainedDataDirectory = Path.Combine(BaseDirectory, "TextReader");
            ScreenshotDirectory = Path.Combine(BaseDirectory, "Screens");

            LogFileFolderPath = Path.Combine(BaseDirectory, "Logging");
            LogFileFolderPathWithName = Path.Combine(LogFileFolderPath,"Logs.txt");

            LocalScreenshotPath = Path.Combine(ScreenshotDirectory, "screenshot.png");

            // ADB und Nox-Konfigurationspfade und Einstellungen
            AdbPath = "C:\\Program Files\\Nox\\bin\\adb.exe";
            NoxExePath = "C:\\Program Files\\Nox\\bin\\Nox.exe";
            DeviceIP = "127.0.0.1";
            InputDevice = "/dev/input/event4";
            PackageName = "com.gof.global";
        }
    }
}
