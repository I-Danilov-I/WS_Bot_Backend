namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class Arena(Logging.Logging writeLogs, DeviceControl.GameControl deviceControl, Settings.GameScore gameScore)
    {

        public void GoToArena()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            writeLogs.LogAndConsoleWirite("\n\nArena Kapf");
            writeLogs.LogAndConsoleWirite("---------------------------------------------------------------------------");
            deviceControl.ClickAtTouchPositionWithHexa("00000084", "0000004d"); // Bonusübersicht
            deviceControl.ClickAtTouchPositionWithHexa("000001bc", "000003a8"); // Kraft
            deviceControl.ClickAtTouchPositionWithHexa("000002f4", "000002ce"); // Truppenstäerke
            deviceControl.ClickAtTouchPositionWithHexa("000002e6", "000004ba"); // Latenzträger
            deviceControl.ClickAtTouchPositionWithHexa("00000286", "000001ff"); // Scharfschützen
            deviceControl.ClickAtTouchPositionWithHexa("00000347", " 000003b7"); // Arena
            deviceControl.ClickAtTouchPositionWithHexa("000001f6", "000005e9"); // Herausfordern



            deviceControl.ClickAtTouchPositionWithHexa("00000313", "00000190"); // Den erten von oben anklciken
            deviceControl.ClickAtTouchPositionWithHexa("000000ef", "000005d8"); // Schnelleinsatz
            deviceControl.ClickAtTouchPositionWithHexa("00000287", "000005d6"); // Kampf
            Thread.Sleep(45 * 1000);
            deviceControl.BackUneversal();
            gameScore.ArenaFightsCounter++;
            writeLogs.LogAndConsoleWirite("Arena Kapf: OK");
            Console.ResetColor();
        }

    }
}
