namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class Geheimdienst(Logging.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {


        internal void StartProcess()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logging.LogAndConsoleWirite("[ GEHEIMDIENST ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");
            gameControl.GoWelt();
            GoToMisson();
            string missionsart = ClickAcrossScreenSequentially(400, 400, 100, 100, 100);

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
                    if (CheckAusdauer() == true)
                    {
                        gameScore.GeheimdienstCounter++;
                        logging.PrintFormatted("Betienjagt", "Starting");

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
  
                Thread.Sleep(45 * 1000);
                gameControl.ClickAtTouchPositionWithHexa("000000d8", "000005e2"); // Abholen
                gameScore.GeheimdienstCounter++;
                logging.PrintFormatted("Kampf", "Starting");
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
            // Bildschirmauflösung abrufen
            (int screenWidth, int screenHeight) = noxControl.GetResolution();

            if (screenWidth == 0 || screenHeight == 0)
            {
                logging.LogAndConsoleWirite("Auflösung konnte nicht abgerufen werden. Klickvorgang wird abgebrochen.");
                return "0";
            }

            // Berechnung der Start- und Endbereiche für die X- und Y-Koordinaten
            int startX = leftMargin;
            int endX = screenWidth - rightMargin;
            int startY = topMargin;
            int endY = screenHeight - bottomMargin;

            // Schrittgröße festlegen (z.B. alle 50 Pixel klicken, um die Fläche abzudecken)
            int stepSize = 50; // Anpassen je nach gewünschter Genauigkeit

            logging.PrintFormatted("Search Mission", "...");

            // Systematisches Klicken in einem Rasterformat über die gesamte Fläche
            for (int y = startY; y < endY; y += stepSize)
            {
                for (int x = startX; x < endX; x += stepSize)
                {
                    ClickAt(x, y); // Klick an der aktuellen Position

                    string missionsart = CheckMissionsArt();

                    // Abbrechen, wenn eine Missionsart gefunden wird (außer "Meister")
                    if (missionsart == "Belohnung")
                    {
                        logging.PrintFormatted("Belohnung", "...");
                        gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab"); // Abholen
                        Thread.Sleep(4000);
                        gameScore.GeheimdienstCounter++;
                        logging.PrintFormatted("Belohnung", "Abgeholt");
                        continue;
                    }
                    else if (missionsart == "Bestienjagt")
                    {
                        return "B";
                    }
                    else if (missionsart == "Rettung")
                    {
                        return "R";
                    }
                    else if (missionsart == "Heldenreise")
                    {
                        return "H";
                    }
                    else if (missionsart == "Meister")
                    {
                        // Wenn "Meister" gefunden wird, mache weiter ohne Abbruch
                        logging.PrintFormatted("Meister", "Überspringen");
                        continue;
                    }
                }
            }

            // Keine relevante Missionsart gefunden
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
            Thread.Sleep(1000);
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
