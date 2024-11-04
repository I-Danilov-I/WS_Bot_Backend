
namespace WhiteoutSurvival_Bot.GameAutomations
{
    public class Jagt(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {
        public Log.Logging Logging { get; } = logging;

        internal void PolarTerrorStarten(int tierLevel)
        {
            logging.LogAndConsoleWirite("\n\nPolar Terror wird gestartet...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.GoWelt();
            gameControl.ClickAtTouchPositionWithHexa("00000036", "00000443"); // Such icon wählen
            gameControl.ClickAtTouchPositionWithHexa("00000133", "0000047a"); // Polar Terror Auswahl
            TierLevel(tierLevel); // Bestienlevel Eingabe
            gameControl.ClickAtTouchPositionWithHexa("000001be", "000005eb"); // Suche
            Thread.Sleep(1000);
            gameControl.ClickAtTouchPositionWithHexa("000001c1", "00000261"); // Rally
            gameControl.ClickAtTouchPositionWithHexa("000001bf", "00000419"); // Rally bestätigen // ZEitauwahl
       
            if (gameSettings.TruppenAusgleich == true)
            {
                gameControl.ClickAtTouchPositionWithHexa("000000fa", "000005ba"); // Ausgleichen?
            }

            gameControl.ClickAtTouchPositionWithHexa("000002b6", "000005eb"); // Einsetzen
            if (CheckAusdauer())
            {
                gameControl.GoStadt();
                logging.LogAndConsoleWirite("Polar Terror erfogreich gestartet! ;)");
            }
        }

        public void BestienJagtStarten(int bestienLevel)
        {
            logging.LogAndConsoleWirite("\n\nBestien Jagt wird gestartet...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.GoWelt();
            gameControl.ClickAtTouchPositionWithHexa("00000036", "00000443"); // Suchicon wählen
            gameControl.ClickAtTouchPositionWithHexa("00000061", "0000046d"); // Bestien Auswahl
            TierLevel(bestienLevel); // Bestienlevel Eingabe
            gameControl.ClickAtTouchPositionWithHexa("000001be", "000005eb"); // Suchen Butto
            Thread.Sleep(2000);
            gameControl.ClickAtTouchPositionWithHexa("000001c6", "000002ff"); // Angriff

            if (gameSettings.TruppenAusgleich == true)
            {
                gameControl.ClickAtTouchPositionWithHexa("000000fa", "000005ba"); // Ausgleichen?
            }

            // Prüfe Ausgangsituation
            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("scheitern", "Einsatz wird", "") == true)
            {
                gameControl.PressButtonBack();
                logging.LogAndConsoleWirite("Der Ausgang währe fatal gewesen, Jagt nicht gestartet. :)");
            };

            gameControl.ClickAtTouchPositionWithHexa("000002b6", "000005eb");       
            CheckAusdauer();
            gameControl.GoStadt();
        }


        private bool CheckAusdauer()
        {
            textRecogntion.TakeScreenshot();
            bool reichenResursen = textRecogntion.CheckTextInScreenshot("Ausdauer", "Gouverneur", ""); // Suche nach Text im Screenshot
            if (reichenResursen)
            {
                logging.LogAndConsoleWirite("Resoursen reichen nicht aus :(");
                gameControl.PressButtonBack();
                gameControl.PressButtonBack();
                return false;
            }
            gameScore.BeastHuntCounter++;
            logging.LogAndConsoleWirite("Bestien Jagt erfogreich gestartet! ;)");
            return true;
        }


        private void TierLevel(int bestienLevel)
        {
            gameControl.ClickAtTouchPositionWithHexa("000002f7", "00000522"); // Level zahl anklicken

            // Bestehenden Text/Zahlen löschen          
            int numberOfCharactersToDelete = 5; // Anzahl der Zeichen, die gelöscht werden sollen
            for (int i = 0; i < numberOfCharactersToDelete; i++)
            {
                string deleteCommand = "shell input keyevent KEYCODE_DEL"; // Löschen taster drücken
                adb.ExecuteAdbCommand(deleteCommand);
                Thread.Sleep(10); // Kurze Pause zwischen den Löschvorgängen
            }

            string adbCommand = $"shell input text {bestienLevel}";
            adb.ExecuteAdbCommand(adbCommand);

            // Enter-Taste drücken           
            string enterCommand = "shell input keyevent KEYCODE_ENTER"; // Bestätigen oder Enter drücken
            adb.ExecuteAdbCommand(enterCommand);
        }

    }
}
