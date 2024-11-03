namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class Allianz(Log.Logging logging, DeviceControl.GameControl gameControl, Settings.GameSettings gameSettings, Settings.GameScore gameScore)
    {

        public void AutobeitritAktivieren()
        {
            logging.LogAndConsoleWirite("\n\nAllianz Autobeitrit re/aktivieren...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.ClickAtTouchPositionWithHexa("0000029e", "000005fa"); // Allianz
            gameControl.ClickAtTouchPositionWithHexa("000000ec", " 00000338"); // Krieg
            gameControl.ClickAtTouchPositionWithHexa("000001c9", " 000005f3"); // Autobeitrit

            if (gameSettings.AllianceAutobeitritt == true)
            {
                gameControl.ClickAtTouchPositionWithHexa("0000026e", "0000054a"); // Aktivieren
                logging.LogAndConsoleWirite("Allianz autobeitrit reaktiviert! :)");
            }
            else
            {
                gameControl.ClickAtTouchPositionWithHexa("000000dd", "00000558"); // Stopen
                logging.LogAndConsoleWirite("Allianz autobeitrit angehalten. :)");

            }
            gameControl.BackUneversal();
        }



        public void KistenAbholen()
        {
            logging.LogAndConsoleWirite("\n\nAllianz Kisten werden abgeholt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.ClickAtTouchPositionWithHexa("0000029e", "000005fa"); // Allianz
            gameControl.ClickAtTouchPositionWithHexa("000002af", "00000346"); // Kiste
            gameControl.ClickAtTouchPositionWithHexa("0000029d", "000001f3"); // Alliangeschenke 
            gameControl.ClickAtTouchPositionWithHexa("000002fc", "000005f0"); // Alliangeschenke nehmen
            gameControl.ClickAtTouchPositionWithHexa("000002fc", "000005f0"); // Alliangeschenke nehmen2
            gameControl.ClickAtTouchPositionWithHexa("0000000fa", "000001f1"); // Beutekiste
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000005e7"); // Beutekiste nehmen
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000005e7"); // Beutekiste nehmen
            gameControl.ClickAtTouchPositionWithHexa("000001c1", "00000100"); // Große Kiste
            gameControl.ClickAtTouchPositionWithHexa("000001c1", "00000100"); // Große Kiste zum verlassen klicken
            gameControl.ClickAtTouchPositionWithHexa("000001cb", "000005e7"); // Verlassen
            gameControl.PressButtonBack();
            gameControl.PressButtonBack();
            gameScore.AllianceChestsCounter++;
            logging.LogAndConsoleWirite("Allianz Kisten abholung beendet! :)");
        }


        public void Hilfe()
        {
            logging.LogAndConsoleWirite("\n\nAllianz Hilfe wird ausgeführt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.ClickAtTouchPositionWithHexa("0000029e", "000005fa"); // Allianz
            gameControl.ClickAtTouchPositionWithHexa("00000298", "0000052f"); // Hilfe Auswahl Label
            gameControl.ClickAtTouchPositionWithHexa("000001bf", " 000005dc"); // Allen helfen
            gameControl.PressButtonBack();
            gameControl.PressButtonBack();
            gameScore.AllianceHelpCounter++;
            logging.LogAndConsoleWirite("Allianz sagt Danke für deine Hilfe! ;)");
        }


        public void TechnologieBeitrag(int anzahlBeitrege)
        {
            logging.LogAndConsoleWirite("\n\nAllianz Technologie Beitrag wird ausgeführt...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.ClickAtTouchPositionWithHexa("0000029e", "000005fa"); // Allianz
            gameControl.ClickAtTouchPositionWithHexa("000002a5", "00000486"); // Technologie Label
            gameControl.ClickAtTouchPositionWithHexa("000001c3", "0000050c"); // Eigeschaft wählen

            int counter = 0;
            while (counter < anzahlBeitrege)
            {
                counter++;
                gameControl.ClickAtTouchPositionWithHexa("00000284", "00000505"); // Beitragen
                Thread.Sleep(500);
            }

            gameControl.PressButtonBack();
            gameControl.PressButtonBack();
            gameControl.PressButtonBack();
            gameScore.AllianceTechnologyCounter++;
            logging.LogAndConsoleWirite("Allianz sagt Danke für dein Beitrag! ;)");
        }


    }
}
