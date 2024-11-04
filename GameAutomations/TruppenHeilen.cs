namespace WhiteoutSurvival_Bot.GameAutomations
{
    public class TruppenHeilen(Log.Logging logging, DeviceControl.GameControl GameControl, TextReader.TextRecogntion textRecogntion) : Settings.GameScore
    {

        public void Heilen()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            logging.LogAndConsoleWirite("[ TRUPPEN HEILUNG ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");
            
            logging.PrintFormatted("Heilung", "...");
            GameControl.ClickAtTouchPositionWithHexa("00000081", "0000004f"); // Bonusübersicht klick
            Thread.Sleep(1000);
            GameControl.ClickAtTouchPositionWithHexa("000001cf", "000003a6"); // Kraft klick
            Thread.Sleep(1000);
            GameControl.ClickAtTouchPositionWithHexa("000002f1", "00000540"); // Technologieforschung wälen
            Thread.Sleep(1000);
            GameControl.ClickAtTouchPositionWithHexa("0000029e", "00000209"); // Gebäude Krankenhaus wählen
            Thread.Sleep(1000);
            GameControl.ClickAtTouchPositionWithHexa("00000262", "0000042a"); // Heilen Button1
            Thread.Sleep(1000);

            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Keine", "Alles ist gut!", "") == true)
            {
                logging.PrintFormatted("Heilung", "Nicht erforderlich");
                GameControl.PressButtonBack();
                Thread.Sleep(1000);
                return;
            }

            GameControl.ClickAtTouchPositionWithHexa("000002d4", "000005dd"); // Heilen Button2
            logging.PrintFormatted("Heilung", "Comleted");
            Thread.Sleep(1000);

            // Hilfe für Truppenheilung Anfordern
            logging.PrintFormatted("Heil-Hilfe", "...");

            GameControl.ClickAtTouchPositionWithHexa("000001b9", "00000334"); // Gebäude Wählen
            Thread.Sleep(1000);
            GameControl.ClickAtTouchPositionWithHexa("00000262", "0000042a"); // Heilen Button1
            Thread.Sleep(1000);
            GameControl.ClickAtTouchPositionWithHexa("000002d4", "000005dd"); // Hilfe bitton

            logging.PrintFormatted("Heil-Hilfe", "Angefordert");
            Thread.Sleep(1000);
            GameControl.PressButtonBack();
            Thread.Sleep(1000);
            HealingCounter++;
            
        }

    }
}
