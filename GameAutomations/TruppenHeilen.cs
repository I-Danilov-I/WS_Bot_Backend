namespace WhiteoutSurvival_Bot.GameAutomations
{
    internal class TruppenHeilen(Log.Logging logging, DeviceControl.GameControl GameControl, TextReader.TextRecogntion textRecogntion) : Settings.GameScore
    {

        public void Heilen()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logging.LogAndConsoleWirite("[ TRUPPEN HEILUNG ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");
            
            logging.PrintFormatted("Heilung", "...");
            GameControl.ClickAtTouchPositionWithHexa("00000081", "0000004f"); // Bonusübersicht klick
            GameControl.ClickAtTouchPositionWithHexa("000001cf", "000003a6"); // Kraft klick
            GameControl.ClickAtTouchPositionWithHexa("000002f1", "00000540"); // Technologieforschung wälen
            GameControl.ClickAtTouchPositionWithHexa("0000029e", "00000209"); // Gebäude Krankenhaus wählen
            GameControl.ClickAtTouchPositionWithHexa("00000262", "0000042a"); // Heilen Button1

            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Keine", "Alles ist gut!", "") == true)
            {
                logging.PrintFormatted("Heilung", "Nicht erforderlich");
                GameControl.PressButtonBack();
                return;
            }

            // Heilung Aufügren
            GameControl.ClickAtTouchPositionWithHexa("000002d4", "000005dd"); // Heilen Button2
            logging.PrintFormatted("Heilung", "Comleted");

            // Hilfe für Truppenheilung Anfordern
            logging.PrintFormatted("Heil-Hilfe", "...");

            GameControl.ClickAtTouchPositionWithHexa("000001b9", "00000334"); // Gebäude Wählen
            GameControl.ClickAtTouchPositionWithHexa("00000262", "0000042a"); // Heilen Button1
            GameControl.ClickAtTouchPositionWithHexa("000002d4", "000005dd"); // Hilfe bitton

            logging.PrintFormatted("Heil-Hilfe", "Angefordert");
            GameControl.PressButtonBack();
            HealingCounter++;
            
        }

    }
}
