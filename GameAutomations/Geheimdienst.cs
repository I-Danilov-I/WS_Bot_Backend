namespace WhiteoutSurvival_Bot.GameAutomations
{
    public class Geheimdienst(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {

        private int lastClickedX = 0;
        private int lastClickedY = 0;


        internal void StartProcess()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logging.LogAndConsoleWirite("[ GEHEIMDIENST ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");
            gameControl.GoWelt();
            GoToMisson();
            string missionsart = ClickAcrossScreenSequentially(380, 450, 150, 100, 100);

            if (missionsart == "R")
            {
                StartMisson();
                logging.PrintFormatted("Rettung", "Starting");
                gameScore.GeheimdienstCounter++;
            }

            else if (missionsart == "B")
            {
                StartMisson();
                if (gameSettings.TruppenAusgleich == true) { gameControl.ClickAtTouchPositionWithHexa("000000fa", "000005ba"); }
                if (CheckTruppenKraft() == true)
                {
                    gameControl.ClickAtTouchPositionWithHexa("000002b6", "000005eb"); // Einsetzen
                    Thread.Sleep(1000);
                    if (CheckAusdauer() == true)
                    {
                        gameScore.GeheimdienstCounter++;
                        logging.PrintFormatted("Betienjagt", "Starting");

                    }
                    else { return; }
                }
            }

            else if (missionsart == "F")
            {
                StartMisson();
                if (gameSettings.TruppenAusgleich == true) { gameControl.ClickAtTouchPositionWithHexa("000000fa", "000005ba"); }
                if (CheckTruppenKraft() == true)
                {
                    gameControl.ClickAtTouchPositionWithHexa("000002b6", "000005eb"); // Einsetzen
                    Thread.Sleep(1000);
                    if (CheckAusdauer() == true)
                    {
                        gameScore.GeheimdienstCounter++;
                        logging.PrintFormatted("Feuerjäger", "Starting");

                    }
                    else { return; }
                }
            }

            else if (missionsart == "H") 
            {
                StartMisson();
                logging.PrintFormatted("Kampf", "...");
                gameControl.ClickAtTouchPositionWithHexa("000000d8", "000005e2"); // Schneller Einsatz
                gameControl.ClickAtTouchPositionWithHexa("00000296", "000005da"); // Kampf
                gameControl.ClickAtTouchPositionWithHexa("0000033c", "0000042a"); // Aktiviere  Speed 2x
                gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab"); // Aktiviere Auto Attack
                logging.PrintFormatted("Kampf", "Starting");
                Thread.Sleep(45 * 1000);
                gameControl.ClickAtTouchPositionWithHexa("000000d8", "000005e2"); // Abholen
                gameScore.GeheimdienstCounter++;
                logging.PrintFormatted("Kampf", "Complete");
            }

            else if (missionsart == "M")
            {
                logging.PrintFormatted("Kopfgeld", "OFF");
                gameControl.PressButtonBack();
                gameControl.PressButtonBack();
                logging.PrintFormatted("Kopfgeld", "OFF");
            }

            else if (missionsart == "Bel")
            {
                logging.PrintFormatted("Belohnung", "...");
                gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab"); // Abholen
                Thread.Sleep(4000);
                gameScore.GeheimdienstCounter++;
                logging.PrintFormatted("Belohnung", "Abgeholt");
            }

        }


        internal void StartMisson()
        {
            logging.PrintFormatted("Start Mission", "...");
            gameControl.ClickAtTouchPositionWithHexa("000001ca", "00000472"); // Ansehen
            Thread.Sleep(4000);
            gameControl.ClickAtTouchPositionWithHexa("000001bc", "00000311"); // Agreifen / Erkunden /Retten
            Thread.Sleep(4000);
            logging.PrintFormatted("Start Mission", "Completed");
        }


        internal string ClickAcrossScreenSequentially(int topMargin, int bottomMargin, int leftMargin, int rightMargin, int clickCount)
        {
            (int screenWidth, int screenHeight) = noxControl.GetResolution();

            if (screenWidth == 0 || screenHeight == 0)
            {
                logging.LogAndConsoleWirite("Auflösung konnte nicht abgerufen werden. Klickvorgang wird abgebrochen.");
                return "0";
            }

            int startX = leftMargin;
            int endX = screenWidth - rightMargin;
            int startY = topMargin;
            int endY = screenHeight - bottomMargin;
            int stepSize = 75;

            bool isFirstIteration = lastClickedX == 0 && lastClickedY == 0;

            for (int y = isFirstIteration ? startY : lastClickedY; y < endY; y += stepSize)
            {
                for (int x = isFirstIteration ? startX : lastClickedX; x < endX; x += stepSize)
                {
                    logging.PrintFormatetInSameLine("[ Search Mission", ".");
                    
                    ClickAt(x, y);

                    lastClickedX = x;
                    lastClickedY = y;
                    string missionsart = CheckMissionsArt();
                    logging.PrintFormatetInSameLine("[ Search Mission", "..");

                    if (missionsart == "Belohnung")
                    {
                        logging.PrintFormatted("Belohnung", "...");
                        gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab");
                        Thread.Sleep(4000);
                        logging.PrintFormatted("Belohnung", "Abgeholt");
                        continue;
                    }
                    else if (missionsart == "Bestienjagt" || missionsart == "Rettung" || missionsart == "Heldenreise" || missionsart == "Feuerjäger")
                    {
                        return missionsart.Substring(0, 1);  // Return und speichere den letzten Zustand
                    }
                    else if (missionsart == "Meister")
                    {
                        logging.PrintFormatted("Meister", "Überspringen");
                        gameControl.PressButtonBack();
                        Thread.Sleep(4000);
                        continue;
                    }

                    isFirstIteration = false;
                    Thread.Sleep(1000);
                    logging.PrintFormatetInSameLine("[ Search Mission", "...");
                    Thread.Sleep(1500);
                }

                lastClickedX = startX;  // Zurücksetzen von X für die nächste Zeile
            }

            lastClickedX = 0;  // Reset der Positionen am Ende
            lastClickedY = 0;
            return "0";
        }


        private void ClickAt(int x, int y)
        {
            string adbCommand = $"shell input tap {x} {y}";
            adb.ExecuteAdbCommand(adbCommand);
        }


        private void GoToMisson()
        {
            logging.PrintFormatted("Go To Misson", "...");
            gameControl.ClickAtTouchPositionWithHexa("00000340", "00000437"); // Geheimmission Icon 
            logging.PrintFormatted("Go To Misson", "Completed");
            Thread.Sleep(2000);
        }                                                                               


        internal string CheckMissionsArt()
        {
            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Zum", "tippen", "weiß"))
            {
                logging.PrintFormatted("Find Misson", "Belohnung");
                return "Belohnung";
            }

            if (textRecogntion.CheckTextInScreenshot("Bestienjagd", "jagt", "weiß"))
            {
                logging.PrintFormatted("Find Misson", "Bestienjagt");           
                return "Bestienjagt";
            }

            if (textRecogntion.CheckTextInScreenshot("Rette", "Überlebende", "weiß"))
            {
                logging.PrintFormatted("Find Misson", "Rettung");
                return "Rettung";
            }

            if (textRecogntion.CheckTextInScreenshot("Eine", "Heldenreise", "weiß"))
            {
                logging.PrintFormatted("Find Misson", "Heldenreise");
                return "Heldenreise";
            }

            if (textRecogntion.CheckTextInScreenshot("Meister", "Kopfgeld", "weiß"))
            {
                logging.PrintFormatted("Find Misson", "Kopfgeld");
                return "Meister";
            }

            if (textRecogntion.CheckTextInScreenshot("Feuerjäger", "Feuer", "weiß"))
            {
                logging.PrintFormatted("Find Misson", "Feuerjäger");
                return "Feuerjäger";
            }
            if (textRecogntion.CheckTextInScreenshot("Belohnungen", "Geheimdienst", "blau"))
            {
                logging.PrintFormatted("Find Misson", "Feuerjäger");
                return "Feuerjäger";
            }


            return "null";
        }

        internal bool CheckTruppenKraft()
        {
            logging.PrintFormatted("Truppen Kraft", "...");
            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Du", "gute", "grün") == false)
            {
                logging.PrintFormatted("Truppen Kraft", "Nicht ausreichen");
                gameControl.PressButtonBack();   
                Thread.Sleep(2000);
                return false;
            }
            logging.PrintFormatted("Truppen Kraft", "OK");
            return true;
        }

        internal bool CheckAusdauer()
        {
            logging.PrintFormatted("Ausdauer", "...");
            textRecogntion.TakeScreenshot();
            bool reichenResursen = textRecogntion.CheckTextInScreenshot("Ausdauer", "Gouverneur", null!); // Suche nach Text im Screenshot
            if (reichenResursen)
            {
                logging.PrintFormatted("Ausdauer", "Nicht ausreichend");
                return false;
            }
            logging.PrintFormatted("Ausdauer", "OK");
            return true;      
        }


    }
}
