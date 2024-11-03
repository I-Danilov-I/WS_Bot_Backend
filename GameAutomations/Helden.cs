namespace WhiteoutSurvival_Bot.GameAutomations
{
    public class Helden(Log.Logging logging,
        DeviceControl.GameControl gameControl,
        Settings.GameSettings gameSettings,
        Settings.GameScore gameScore,
        TextReader.TextRecogntion textRecogntion,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbCommandExecutor adb)
    {

        public void HeldenRekrutieren()
        {
            logging.LogAndConsoleWirite("\n\nHeld Rekrutierung: Start...");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            gameControl.ClickAtTouchPositionWithHexa("000000e9", "000005f8"); // Helden
            gameControl.ClickAtTouchPositionWithHexa("0000029d", "000005df"); // Held rekrutieren

            gameControl.ClickAtTouchPositionWithHexa("00000335", "0000022d"); // Punkte Truhe   
            gameControl.ClickAtTouchPositionWithHexa("00000335", "0000022d"); // Punkte Truhe 

            gameControl.ClickAtTouchPositionWithHexa("000000f1", "00000411"); // Erweiterte rekrutierung         
            textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Kaufen", "500", "") == true)
            {
                gameControl.PressButtonBack();
                logging.LogAndConsoleWirite("Erweiterte Rekrutierung: Nicht aktiv!");
            }
            else
            {
                gameControl.PressButtonBack();
                logging.LogAndConsoleWirite("Erweiterte Rekrutierung: Erfolgreich!");
                gameScore.AdvancedHeroRecruitmentCounter++;
            }

            gameControl.ClickAtTouchPositionWithHexa("000000f6", "000005e1"); // Normale rekurtierung
           textRecogntion.TakeScreenshot();
            if (textRecogntion.CheckTextInScreenshot("Kaufen", "500", "") == true)
            {
                logging.LogAndConsoleWirite("Epische Rekrutierung: Nicht aktiv.");
            }
            else
            {
                logging.LogAndConsoleWirite("Epische Rekrutierung: Erfolgreich");
                gameScore.EpicHeroRecruitmentCounter++;
            }
            gameControl.BackUneversal();
        }


    }
}
