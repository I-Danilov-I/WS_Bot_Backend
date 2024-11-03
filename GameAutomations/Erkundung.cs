namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class Erkundung(Logging.Logging logging ,TextReader.TextRecogntion textRecogntion, DeviceControl.GameControl gameControl, Settings.GameScore gameScore)
    {
        public void StartProcess()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logging.LogAndConsoleWirite("[ ERKUNDUNG ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");
 
            Untetigkeitsertrag();
            Erkundungskampf();
        }


        private void Untetigkeitsertrag()
        {
            logging.PrintFormatted("Untetigkeitsertra", "...");
            gameControl.ClickAtTouchPositionWithHexa("00000054", "000005f3"); // Erkundung
            gameControl.ClickAtTouchPositionWithHexa("000002cd", "00000479"); // Nehmen1
            gameControl.ClickAtTouchPositionWithHexa("000002cd", "00000479"); // Nehmen1  

            gameControl.ClickAtTouchPositionWithHexa("000001cd", "00000481"); // Nehmen Bestätigen1  
            gameControl.ClickAtTouchPositionWithHexa("000001cd", "00000481"); // Nehmen Bestätigen2  
            gameControl.ClickAtTouchPositionWithHexa("000001cd", "00000481"); // Bestätigen3
            logging.PrintFormatted("Untetigkeitsertrag", "Ausgeführt");
            gameControl.PressButtonBack();

            gameScore.ExplorationBonusCounter++;
        }


        private void Erkundungskampf()
        {
            logging.PrintFormatted("Erkundungskampf", "...");
            gameControl.ClickAtTouchPositionWithHexa("00000054", "000005f3"); // Erkundung
            
            gameControl.ClickAtTouchPositionWithHexa("000001c1", "000005b7"); // Erkunden (Kampf)
            gameControl.ClickAtTouchPositionWithHexa("000000d8", "000005e2"); // Schneller Einsatz
            gameControl.ClickAtTouchPositionWithHexa("00000296", "000005da"); // Kampf

            gameControl.ClickAtTouchPositionWithHexa("0000033c", "0000042a"); // Aktiviere  Speed 2x
            gameControl.ClickAtTouchPositionWithHexa("00000335", "000004ab"); // Aktiviere Auto Attack

            Thread.Sleep(45 * 1000);

            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Zum Verlassen irgendwo tippen", "Steigere Kraft durch:", "") == true)
            {
                gameScore.ExplorationBattleCounter++;
                logging.PrintFormatted("Erkundungskampf", "ausgeführt aber leider gescheitert");
            }
            else { logging.PrintFormatted("Erkundungskampf", "nicht beendet"); }

            logging.PrintFormatted("Erkundungskampf", "Ausgeführt");
            gameControl.PressButtonBack();
            gameControl.PressButtonBack();
        }

    }
}
