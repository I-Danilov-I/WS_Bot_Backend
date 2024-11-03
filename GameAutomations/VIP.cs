namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class VIP(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb) : Settings.GameScore
    {
        public void KistenAbholen()
        {
            logging.LogAndConsoleWirite("\n\nVIP tägliches Login: ...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.ClickAtTouchPositionWithHexa("0000025b", "0000004b"); // VIP
            gameControl.ClickAtTouchPositionWithHexa("00000308", "0000015e"); // Kiste 1x
            gameControl.ClickAtTouchPositionWithHexa("00000308", "0000015e"); // Kiste 2x
            gameControl.ClickAtTouchPositionWithHexa("000002d8", "00000406"); // GratisBündel
            gameControl.ClickAtTouchPositionWithHexa("000002d8", "00000406"); // GratisBündel
            gameControl.PressButtonBack();
            VipStatusCounter++;
            logging.LogAndConsoleWirite("VIP tägliches Login: Erfogreich)");
        }
    }
}
