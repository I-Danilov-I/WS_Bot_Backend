namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class TruppenTraining(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {


        private void TruppenAnzahl(int truppenAnzahl)
        {
            gameControl.ClickAtTouchPositionWithHexa("0000029d", "0000052e"); // Button Truppenanzahl klicken

            // Bestehenden Text/Zahlen löschen          
            int numberOfCharactersToDelete = 5; // Anzahl der Zeichen, die gelöscht werden sollen
            for (int i = 0; i < numberOfCharactersToDelete; i++)
            {
                string deleteCommand = "shell input keyevent KEYCODE_DEL"; // Löschen taster drücken
                adb.ExecuteAdbCommand(deleteCommand);
            }
       
            string adbCommand = $"shell input text {truppenAnzahl}";
            adb.ExecuteAdbCommand(adbCommand);

            // Enter-Taste drücken           
            string enterCommand = "shell input keyevent KEYCODE_ENTER"; // Bestätigen oder Enter drücken
            adb.ExecuteAdbCommand(enterCommand);
        }


        private void CheckResoursen()
        {
            logging.LogAndConsoleWirite("Checke ob Resoursen ausreichen...");
            textRecogntion.TakeScreenshot(); // Mache ein Screenshot
            bool reichenResursen = textRecogntion.CheckTextInScreenshot("Erhalte mehr", "Erhalte mehr", ""); // Suche nach Text im Screenshot
            if (reichenResursen)
            {
                logging.LogAndConsoleWirite("Resoursen reichen nicht aus :(");
                gameControl.PressButtonBack();
                gameControl.PressButtonBack();
                return;
            }
            logging.LogAndConsoleWirite("Es sind genug Resorsen da! ;)");
        }


        private void CheckeErfolg()
        {
            // Prüfe um Training erfoglreich gestartet wurde.
            textRecogntion.TakeScreenshot(); // Mache ein Screenshot
            bool erfolg = textRecogntion.CheckTextInScreenshot("Ausbildung", "gungen", ""); // Suche nach Text im Screenshot
            if (erfolg == true)
            {
                logging.LogAndConsoleWirite("Truppen Training erfogreich gestartet! ;)");
                gameControl.PressButtonBack();
            }
        }


        private void CheckeObTruppeAusgebildetWerden(int truppenAnzahl)
        {
            textRecogntion.TakeScreenshot();
            bool findOrNot = textRecogntion.CheckTextInScreenshot("Ausbildung", "Befördert:", "");
            if (!findOrNot)
            {
                TruppenAnzahl(truppenAnzahl);
                gameControl.ClickAtTouchPositionWithHexa("0000028c", "000005d8"); // Letzter Buttton: Ausbilden
                Thread.Sleep(5000);

                CheckResoursen(); // Prüfe ob genu REsursen da sind
                CheckeErfolg(); // Prüfe um Training erfoglreich gestartet wurde.
            }
            else
            {
                logging.LogAndConsoleWirite("Truppen werden bereits ausgebildet oder befödert. ;)");
                gameControl.PressButtonBack();
            }
        }


        internal void TrainiereInfaterie(int truppenAnzahl)
        {
            logging.LogAndConsoleWirite("\n\nInfaterie-Truppen Training wird gestartet...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.SeitenMenuOpen();

            gameControl.ClickAtTouchPositionWithHexa("00000040", "000002ad"); // Auswahl im Menü, Infaterie Truppe ausbilden klicken.

            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // Abholung der fertig tranierten Truppen
            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // Anklicken des Gebäudes der Infaterie Truppen
            gameControl.ClickAtTouchPositionWithHexa("0000028c", "000003f1"); // Button Ausbilden klicken.

            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // !!!!!!

            CheckeObTruppeAusgebildetWerden(truppenAnzahl);
            gameScore.InfantryUnitsTrainedCounter++;
        }


        internal void TrainiereLatenzTreger(int truppenAnzahl)
        {
            logging.LogAndConsoleWirite("\n\nLatenzträger-Truppen Training wird gestartet...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.SeitenMenuOpen();

            gameControl.ClickAtTouchPositionWithHexa("00000110", "00000304"); // Auswahl im Menü, Infaterie Truppe ausbilden klicken.

            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // Abholung der fertig tranierten Truppen
            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // Anklicken des Gebäudes der Infaterie Truppen
            gameControl.ClickAtTouchPositionWithHexa("0000028c", "000003f1"); // Button Ausbilden klicken.

            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // !!!!!!

            CheckeObTruppeAusgebildetWerden(truppenAnzahl);
            gameScore.LatencyCarrierUnitsTrainedCounter++;
        }


        internal void TrainiereSniper(int truppenAnzahl)
        {
            logging.LogAndConsoleWirite("\n\nSnipers-Truppen Training wird gestartet...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.SeitenMenuOpen();

            gameControl.ClickAtTouchPositionWithHexa("0000011a", "0000036d"); // Auswahl im Menü, Infaterie Truppe ausbilden klicken.

            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // Abholung der fertig tranierten Truppen
            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // Anklicken des Gebäudes der Infaterie Truppen
            gameControl.ClickAtTouchPositionWithHexa("0000028c", "000003f1"); // Button Ausbilden klicken.


            gameControl.ClickAtTouchPositionWithHexa("000001ba", "000002d0"); // !!!!!!

            CheckeObTruppeAusgebildetWerden(truppenAnzahl);
            gameScore.SniperUnitsTrainedCounter++;
        }

    }
}
