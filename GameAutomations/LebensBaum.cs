namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class LebensBaum(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {
        public void GoToBaum()
        {
            gameControl.ClickAtTouchPositionWithHexa("00000084", "0000004d"); // Bonusübersicht
            gameControl.ClickAtTouchPositionWithHexa("000001bc", "000003a8"); // Kraft
            gameControl.ClickAtTouchPositionWithHexa("000002f4", "000002ce"); // Truppenstäerke
            gameControl.ClickAtTouchPositionWithHexa("000002e6", "000004ba"); // Latenzträger
            gameControl.ClickAndHoldAndScroll("000002bd", "000004bc", " 00000027", "000000da", 300, 500);
            gameControl.ClickAtTouchPositionWithHexa("0000027f", "0000018b"); // Dock
            Thread.Sleep(4000);
        }


        public void BaumBelohnungAbholen()
        {
            logging.LogAndConsoleWirite("\n\nLebensbaum Essens wird abgeholt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");

            GoToBaum();

            gameControl.ClickAtTouchPositionWithHexa("000001c3", "00000331"); // Baum anwählen
            gameControl.ClickAtTouchPositionWithHexa("000001c3", "00000331"); // Baum (Vorsichthalber)
            gameControl.ClickAtTouchPositionWithHexa("000002ac", "000003a6"); // Sammeln
            Thread.Sleep(2000);
            gameControl.ClickAtTouchPositionWithHexa("0000032f", "000005e4"); // Stadt
            gameScore.LifeTreeEssenceCounter++;
            logging.LogAndConsoleWirite("Lebensbaum Essens erfogreich abgeholt! ;)");
            Thread.Sleep(3000);
        }


        public void EssensVonFreundenAbholen()
        {
            logging.LogAndConsoleWirite("\n\nEssens von Freunden wird abgeholt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            GoToBaum();

            // Klicke 1 von unten an
            gameControl.ClickAtTouchPositionWithHexa("00000346", "00000083"); // Freunde wählen
            gameControl.ScrollDown(15);
            gameControl.ClickAtTouchPositionWithHexa("00000328", "00000522"); // Klicke letzen an
            Thread.Sleep(3000);
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000001dd"); // Abholen
            Thread.Sleep(2000);

            // Klicke 2 von unten an
            gameControl.ClickAtTouchPositionWithHexa("00000346", "00000083"); // Freunde wählen
            gameControl.ScrollDown(15);
            gameControl.ClickAtTouchPositionWithHexa("0000032b", "0000047c"); // Klicke 2 von unten an
            Thread.Sleep(3000);
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000001dd"); // Abholen
            Thread.Sleep(2000);

            // Klicke 3 von unten an
            gameControl.ClickAtTouchPositionWithHexa("00000346", "00000083"); // Freunde wählen
            gameControl.ScrollDown(15);
            gameControl.ClickAtTouchPositionWithHexa("00000323", "000003cb"); // Klicke 3 von unten an
            Thread.Sleep(3000);
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000001dd"); // Abholen
            Thread.Sleep(2000);

            // Klicke 4 von unten an
            gameControl.ClickAtTouchPositionWithHexa("00000346", "00000083"); // Freunde wählen
            gameControl.ScrollDown(15);
            gameControl.ClickAtTouchPositionWithHexa("00000326", "0000032c"); // Klicke 4 von unten an
            Thread.Sleep(3000);
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000001dd"); // Abholen
            Thread.Sleep(2000);

            gameControl.ClickAtTouchPositionWithHexa("00000032", "00000020"); // Zurück
            Thread.Sleep(3000);

            gameControl.ClickAtTouchPositionWithHexa("0000032f", "000005e4"); // Stadt
            gameScore.LifeTreeEssenceCounter++;

            logging.LogAndConsoleWirite("Essens von Freunden erfogreich abgeholt! ;)");
            Thread.Sleep(3000);
        }


    }
}
