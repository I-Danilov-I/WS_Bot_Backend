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


        public void StartProcess()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logging.LogAndConsoleWirite("[ GEHEIMDIENST ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");
            gameControl.GoWelt();
            GoToMisson();
            string missionsart = ClickAcrossScreenSequentially(380, 450, 150, 100, 25);
            if (missionsart == "0")
            {
                logging.LogAndConsoleWirite("Counter abgelaufen setze fort");
                gameControl.PressButtonBack();
                return;
            }


            if (missionsart == "R")
            {
                StartMisson();
                logging.LogAndConsoleWirite("Rettung Starting");
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
                        logging.LogAndConsoleWirite("Betienjagt Starting");

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
                        logging.LogAndConsoleWirite("Feuerjäger Starting");

                    }
                    else { return; }
                }
            }

            else if (missionsart == "H") 
            {
                StartMisson();
                logging.LogAndConsoleWirite("Kampf");
                gameControl.ClickAtTouchPositionWithHexa("000000d8", "000005e2"); // Schneller Einsatz
                gameControl.ClickAtTouchPositionWithHexa("00000296", "000005da"); // Kampf
                gameControl.ClickAtTouchPositionWithHexa("0000033c", "0000042a"); // Aktiviere  Speed 2x
                gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab"); // Aktiviere Auto Attack
                logging.LogAndConsoleWirite("Kampf");
                Thread.Sleep(45 * 1000);
                gameControl.ClickAtTouchPositionWithHexa("000000d8", "000005e2"); // Abholen
                gameScore.GeheimdienstCounter++;
                logging.LogAndConsoleWirite("Kampf");
            }

            else if (missionsart == "M")
            {
                logging.LogAndConsoleWirite("Kopfgeld");
                gameControl.PressButtonBack();
                gameControl.PressButtonBack();
                logging.LogAndConsoleWirite("Kopfgeld");
            }

            else if (missionsart == "Bel")
            {
                logging.LogAndConsoleWirite("Belohnung");
                gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab"); // Abholen
                Thread.Sleep(4000);
                gameScore.GeheimdienstCounter++;
                logging.LogAndConsoleWirite("Belohnung");
            }

        }


        internal void StartMisson()
        {
            logging.LogAndConsoleWirite("Start Mission");
            gameControl.ClickAtTouchPositionWithHexa("000001ca", "00000472"); // Ansehen
            Thread.Sleep(4000);
            gameControl.ClickAtTouchPositionWithHexa("000001bc", "00000311"); // Agreifen / Erkunden /Retten
            Thread.Sleep(4000);
            logging.LogAndConsoleWirite("Start Mission");
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
            int i = 0;
            for (int y = isFirstIteration ? startY : lastClickedY; y < endY; y += stepSize)
            {
                for (int x = isFirstIteration ? startX : lastClickedX; x < endX; x += stepSize)
                {
                    logging.LogAndConsoleWirite("[ Search Mission Versuch: [{i}]");         
                    
                    ClickAt(x, y);

                    lastClickedX = x;
                    lastClickedY = y;

                    string missionsart = CheckMissionsArt();
                    logging.LogAndConsoleWirite("[ Search Mission");

                    if (missionsart == "Belohnung")
                    {
                        logging.LogAndConsoleWirite("Belohnung");
                        gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab");
                        Thread.Sleep(4000);
                        logging.LogAndConsoleWirite("Belohnung");
                        continue;
                    }
                    else if (missionsart == "Bestienjagt" || missionsart == "Rettung" || missionsart == "Heldenreise" || missionsart == "Feuerjäger")
                    {
                        return missionsart.Substring(0, 1);  // Return und speichere den letzten Zustand
                    }
                    else if (missionsart == "Meister")
                    {
                        logging.LogAndConsoleWirite("Meister");
                        gameControl.PressButtonBack();
                        Thread.Sleep(4000);
                        continue;
                    }

                    i++;
                    if(i > clickCount)
                    {
                        return "0";
                    }

                    isFirstIteration = false;
                    logging.LogAndConsoleWirite("[ Search Mission");
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
            logging.LogAndConsoleWirite("Go To Misson");
            gameControl.ClickAtTouchPositionWithHexa("00000340", "00000437"); // Geheimmission Icon 
            logging.LogAndConsoleWirite("Go To Misson Completed");
            Thread.Sleep(2000);
        }                                                                               


        internal string CheckMissionsArt()
        {
            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Zum", "tippen", "weiß"))
            {
                logging.LogAndConsoleWirite("Find Misson");
                return "Belohnung";
            }

            if (textRecogntion.CheckTextInScreenshot("Bestienjagd", "jagt", "weiß"))
            {
                logging.LogAndConsoleWirite("Find Misson");           
                return "Bestienjagt";
            }

            if (textRecogntion.CheckTextInScreenshot("Rette", "Überlebende", "weiß"))
            {
                logging.LogAndConsoleWirite("Find Misson");
                return "Rettung";
            }

            if (textRecogntion.CheckTextInScreenshot("Eine", "Heldenreise", "weiß"))
            {
                logging.LogAndConsoleWirite("Find Misson");
                return "Heldenreise";
            }

            if (textRecogntion.CheckTextInScreenshot("Meister", "Kopfgeld", "weiß"))
            {
                logging.LogAndConsoleWirite("Find Misson");
                return "Meister";
            }

            if (textRecogntion.CheckTextInScreenshot("Feuerjäger", "Feuer", "weiß"))
            {
                logging.LogAndConsoleWirite("Find Misson");
                return "Feuerjäger";
            }
            if (textRecogntion.CheckTextInScreenshot("Belohnungen", "Geheimdienst", "blau"))
            {
                logging.LogAndConsoleWirite("Find Misson");
                return "Feuerjäger";
            }


            return "null";
        }

        internal bool CheckTruppenKraft()
        {
            logging.LogAndConsoleWirite("Truppen Kraft");
            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Du", "gute", "grün") == false)
            {
                logging.LogAndConsoleWirite("Truppen Kraft");
                gameControl.PressButtonBack();   
                Thread.Sleep(2000);
                return false;
            }
            logging.LogAndConsoleWirite("Truppen Kraft");
            return true;
        }

        internal bool CheckAusdauer()
        {
            logging.LogAndConsoleWirite("Ausdauer");
            textRecogntion.TakeScreenshot();
            bool reichenResursen = textRecogntion.CheckTextInScreenshot("Ausdauer", "Gouverneur", null!); // Suche nach Text im Screenshot
            if (reichenResursen)
            {
                logging.LogAndConsoleWirite("Ausdauer");
                return false;
            }
            logging.LogAndConsoleWirite("Ausdauer");
            return true;      
        }


    }
}
