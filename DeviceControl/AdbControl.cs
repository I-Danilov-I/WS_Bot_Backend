namespace WhiteoutSurvival_Bot.DeviceControl
{
    public class AdbControl(AdbCommandExecutor adb) : Settings.Configuration
    {

        internal void StartADBConnection()
        {
            adb.ExecuteAdbCommand("start-server");
            string connectCommand = $"connect {DeviceIP}";
            string output = adb.ExecuteAdbCommand(connectCommand);
        }


        internal bool DisconnectADB()
        {
            string adbCommand = $"disconnect {DeviceIP}";
            string output = adb.ExecuteAdbCommand(adbCommand);
            if (output.Contains("disconnected"))
            {
                return true;
            }
            return false;
        }


        internal void KillServerADB()
        {
            // ADB-Dienst neu starten
            adb.ExecuteAdbCommand("kill-server");
            System.Threading.Thread.Sleep(10000); //
        }


        internal bool IsAdbConnected()
        {
            string output = adb.ExecuteAdbCommand("devices");
            if (output.Contains("device"))
            {
                return true;
            }
            return false;
        }

    }
}
