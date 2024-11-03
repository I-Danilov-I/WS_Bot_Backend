namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class LagerOnlineBelohnung(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {

        internal void GeschnekAbholen()
        {
            logging.LogAndConsoleWirite("\n\nLager Online Belohnung wird abgeholt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.SeitenMenuOpen();
            gameControl.SeitenMenuScrolDown();

            textRecogntion.TakeScreenshot(); // Mache ein Screenshot
            bool findOrNot = textRecogntion.CheckTextInScreenshot("Belohnung", "Belohnung", ""); // Suche nach Text im Screenshot
            if (findOrNot)
            {
                gameControl.ClickAtTouchPositionWithHexa("0000003b", "000003f8"); // Wähle Online Belohnungen

                gameControl.ClickAtTouchPositionWithHexa("000001c3", "000002ce"); // Abholen

                gameControl.ClickAtTouchPositionWithHexa("000001c3", "000002ce"); // Bestätigen

                gameControl.ClickAtTouchPositionWithHexa("0000023f", "000002a6"); // Schliese das Seitenmenü
                gameScore.StorageBonusGiftCounter++;
                logging.LogAndConsoleWirite($"Lager Online Belohnung erforgreich abgeholt!");
            }
            else
            {
                gameControl.SeitenMenuClose();
                logging.LogAndConsoleWirite("Keine Online Belohnung verfügbar, versuche später erneut.");
                Thread.Sleep(5000);
            }
        }


        internal void AusdauerAbholen()
        {
            logging.LogAndConsoleWirite("\n\nLager Online Ausdauer wird abgeholt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");

            gameControl.ClickAtTouchPositionWithHexa("00000081", "0000004f"); // Bonusübersicht klick
            gameControl.ClickAtTouchPositionWithHexa("000001cf", "000003a6"); // Kraft klick

            gameControl.ClickAtTouchPositionWithHexa("000002f1", "00000540"); // Technologieforschung wälen
            gameControl.ClickAtTouchPositionWithHexa("00000086", "000002ad"); // Lagerhausgebäude wählen

            gameControl.ClickAtTouchPositionWithHexa("000001c8", "000002bfe"); // Gebäude anwählen
            gameControl.ClickAtTouchPositionWithHexa("000001c1", "000002c6"); // Abholen 

            textRecogntion.TakeScreenshot(); // Mache ein Screenshot
            bool findOrNot = textRecogntion.CheckTextInScreenshot("Nehmen", "herzliches", ""); // Suche nach Text im Screenshot
            if (findOrNot)
            {
                gameControl.ClickAtTouchPositionWithHexa("000001cb", "0000049a"); // Bestätigen
                gameScore.StorageBonusGiftCounter++;
                logging.LogAndConsoleWirite($"Lager Online Ausdauer erforgreich abgeholt!");
            }
            else
            {
                logging.LogAndConsoleWirite($"Aktuel keine Ausdauer zu verschenken.");
            }
        }


    }
}
