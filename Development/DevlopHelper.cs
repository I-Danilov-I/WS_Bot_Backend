using System.Diagnostics;

namespace WhiteoutSurvival_Bot.Development
{
    public class DevlopHelper : Settings.Configuration
    {
        private readonly Log.Logging logging;
        private readonly DeviceControl.AdbCommandExecutor adb;


        public DevlopHelper(Log.Logging logging, DeviceControl.AdbCommandExecutor adb)
        {
            this.logging = logging;
            this.adb = adb;
        }


        public void TrackTouchEvents()
        {
            string command = $"shell getevent -lt {InputDevice}"; // Verwende getevent ohne -lp für Live-Daten
            logging.LogAndConsoleWirite("Starte die Erfassung von Touch-Ereignissen...");

            string logFilePathTouchEvens = Path.Combine(LogFileFolderPath, "TouchEventsLogs.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePathTouchEvens, true)) // Append mode
                {
                    Process process = new Process();
                    process.StartInfo.FileName = AdbPath;
                    process.StartInfo.Arguments = command;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.OutputDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            writer.WriteLine(args.Data);
                            logging.LogAndConsoleWirite(args.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                }

                logging.LogAndConsoleWirite($"Touch-Ereignisse wurden in {logFilePathTouchEvens} gespeichert.");
            }
            catch (Exception ex)
            {
                logging.LogAndConsoleWirite($"Fehler bei der Erfassung der Touch-Ereignisse: {ex.Message}");
            }
        }


        public void ListRunningApps()
        {
            string command = "shell ps | grep u0_a";
            logging.LogAndConsoleWirite("Liste der laufenden Apps...");
            string output = adb.ExecuteAdbCommand(command);

            string logFilePathRunningApps = Path.Combine(LogFileFolderPath, "RunningAppsLogs.txt");
            using (StreamWriter writer = new StreamWriter(logFilePathRunningApps, true)) // Append mode
            {
                if (!string.IsNullOrEmpty(output))
                {
                    writer.WriteLine(output);
                }
                else
                {
                    writer.WriteLine("Es laufen derzeit keine Apps.");
                }
            }
        }

    }
}
