using System.Diagnostics;

namespace WhiteoutSurvival_Bot.DeviceControl
{
    public class AdbCommandExecutor(Log.Logging logging) : Settings.Configuration
    {

        public string ExecuteAdbCommand(string command)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = AdbPath,
                        Arguments = command,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Thread.Sleep(CommandDelay);              
                return output;
            }
            catch (Exception ex)
            {
                logging.LogAndConsoleWirite($"ADB Error: {ex.Message}");
                return "";
            }
        }

    }
}
