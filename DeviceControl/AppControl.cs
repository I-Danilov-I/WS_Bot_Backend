using WhiteoutSurvival_Bot.Log;
using WhiteoutSurvival_Bot.TextReader;

namespace WhiteoutSurvival_Bot.DeviceControl
{
    public class AppControl(AdbCommandExecutor adb) : Settings.Configuration
    {

        internal void StartApp()
        {
            adb.ExecuteAdbCommand($"shell monkey -p {PackageName} -c android.intent.category.LAUNCHER 1");
        }


        internal void CloseApp()
        {
            adb.ExecuteAdbCommand($"shell am force-stop {PackageName}");
        }


        internal bool IsAppRun()
        {
            string rezultat = adb.ExecuteAdbCommand($"shell pidof {PackageName}");
            if (string.IsNullOrEmpty(rezultat))
            {
                return false; // Die App wird ausgeführt
            }
            else
            {
                return true; // Die App wird nicht ausgeführt
            }
        }


        internal bool IsAppResponsiv()
        {
            string rezultat = adb.ExecuteAdbCommand("shell dumpsys activity");
            if (rezultat.Contains("ANR"))
            {
                return false; 
            }
            else
            {
                return true; 
            }
        }

    }
}
