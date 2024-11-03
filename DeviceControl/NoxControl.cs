using System.Diagnostics;

namespace WhiteoutSurvival_Bot.DeviceControl
{
    public class NoxControl(AdbCommandExecutor adb) : Settings.Configuration
    {

        internal void StartNoxPlayer()
        {
            Process.Start(NoxExePath);
        }


        internal void CLoseNoxPlayerAllProcesses()
        {
            foreach (var process in Process.GetProcessesByName("Nox"))
            {
                process.Kill();
            }
            Thread.Sleep(10000);
        }


        public bool IsNoxRunning()
        {
            // Überprüfen, ob ein Prozess namens "Nox" oder "NoxVMHandle" läuft
            Process[] noxProcesses = Process.GetProcessesByName("Nox");
            Process[] noxVmProcesses = Process.GetProcessesByName("NoxVMHandle");

            // Rückgabe true, wenn mindestens ein Prozess gefunden wurde
            return noxProcesses.Length > 0 || noxVmProcesses.Length > 0;
        }


        internal bool IsNoxReady()
        {
            string output = adb.ExecuteAdbCommand("shell getprop init.svc.bootanim");
            return output.Contains("stopped");
        }


        internal bool IsNoxNetworkConnected()
        {
            string output = adb.ExecuteAdbCommand("shell ping -c 1 www.google.com");
            return output.Contains("1 received");
        }


        internal (int width, int height) GetResolution()
        {
            // ADB-Befehl zum Abrufen der Auflösung
            string adbCommand = "shell wm size";
            string output = adb.ExecuteAdbCommand(adbCommand);

            if (!string.IsNullOrEmpty(output) && output.Contains("Physical size:"))
            {
                // Extrahiere die Auflösung (Breite und Höhe)
                string resolution = output.Split(':')[1].Trim();
                string[] dimensions = resolution.Split('x');
                if (dimensions.Length == 2 &&
                    int.TryParse(dimensions[0], out int width) &&
                    int.TryParse(dimensions[1], out int height))
                {               
                    return (width, height);
                }
            }
            return (0, 0);
        }

    }
}
