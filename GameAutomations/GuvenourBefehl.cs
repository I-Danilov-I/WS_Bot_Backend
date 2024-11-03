namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class GuvenourBefehl(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {

        public void EilauftragAbholen()
        {
            logging.LogAndConsoleWirite("\n\nGuvenour Befehl wird veröffetlicht...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.GoStadt();
            gameControl.ClickAtTouchPositionWithHexa("00000346", "000004a9"); // Open
            gameControl.ClickAtTouchPositionWithHexa("0000026e", "000001a2"); // Eilauftrag
            gameControl.ClickAtTouchPositionWithHexa("000001e3", "0000047a"); // Erlassen
            gameControl.ClickAtTouchPositionWithHexa("000001e3", "0000047a"); // Abholen
            gameControl.BackUneversal();
            logging.LogAndConsoleWirite("Guvenour Befehl Eilauftrag erfolgreich veröffetlicht! ;)");
        }


        public void FestlichkeitenAbholen()
        {
            logging.LogAndConsoleWirite("\n\nGuvenour Befehl wird veröffetlicht...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.GoStadt();
            gameControl.ClickAtTouchPositionWithHexa("00000346", "000004a9"); // Open
            gameControl.ClickAtTouchPositionWithHexa("00000276", "000004c4"); // Fetlichkeiten
            gameControl.ClickAtTouchPositionWithHexa("000001e3", "0000047a"); // Erlassen
            gameControl.BackUneversal();
            logging.LogAndConsoleWirite("Guvenour Befehl Festlichkeiten erfolgreich veröffetlicht! ;)");
        }

    }
}
