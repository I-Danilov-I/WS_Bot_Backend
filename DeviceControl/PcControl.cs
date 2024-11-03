using System;
using System.Diagnostics;

namespace WhiteoutSurvival_Bot.DeviceControl
{
    internal class PcControl
    {

        // Methode zum Überprüfen des freien Festplattenspeichers
        public long GetFreeDiskSpace(string driveLetter = "C")
        {
            var drive = new DriveInfo(driveLetter);
            return drive.AvailableFreeSpace;
        }

        // Methode zum Neustarten des PCs
        public void RestartComputer()
        {
            Process.Start("shutdown", "/r /t 0");
        }

        // Methode zum Herunterfahren des PCs
        public void ShutdownComputer()
        {
            Process.Start("shutdown", "/s /t 0");
        }

        // Methode zum Überprüfen der Netzwerkkonnektivität
        public bool IsInternetConnected()
        {
            try
            {
                System.Net.WebClient webClient = new System.Net.WebClient();
                using (var client = webClient)
                using (client.OpenRead("http://google.com"))
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
