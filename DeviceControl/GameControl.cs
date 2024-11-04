namespace WhiteoutSurvival_Bot.DeviceControl
{
    public class GameControl(
        Log.Logging logging, 
        DeviceControl.AdbCommandExecutor adb, 
        DeviceControl.NoxControl noxControl, 
        TextReader.TextRecogntion textRecogntion, 
        DeviceControl.AppControl appControl,
        Settings.GameScore gameStats
        ) : Settings.Configuration
    {


        public bool IsAccountUsage()
        {
            textRecogntion.TakeScreenshot();

            bool isAccountInUse = textRecogntion.CheckTextInScreenshot("anderen", "Konto", "blau");
            bool isAccountInUse2 = textRecogntion.CheckTextInScreenshot("Tipps", "Kontakt", "weiß");
            if (isAccountInUse == true || isAccountInUse2 == true)
            {
                logging.PrintFormatted("Accaunt", "In Verwendung", $"Reconectin in {ReconnectSleepTime} Min");
                appControl.CloseApp();

                Thread.Sleep(TimeSpan.FromMinutes(ReconnectSleepTime));             
                throw new Exception("Konto derzeit in Verwendung.");
            }

            return true;
        }



        internal void ScrollDown(int anzahlScroll)
        {
            int count = 0;
            while (count < anzahlScroll)
            {
                count++;
                // Beispielkoordinaten für eine Wischgeste von oben nach unten
                int startX = 500;  // X-Koordinate für den Startpunkt des Wischens
                int startY = 1000; // Y-Koordinate für den Startpunkt des Wischens (oben)
                int endX = 500;    // X-Koordinate für den Endpunkt des Wischens (gleichbleibend)
                int endY = 300;    // Y-Koordinate für den Endpunkt des Wischens (unten)
                int duration = 300; // Dauer der Wischgeste in Millisekunden

                // ADB-Befehl zum Wischen
                string adbCommand = $"shell input swipe {startX} {startY} {endX} {endY} {duration}";
                adb.ExecuteAdbCommand(adbCommand);
            }
        }


        internal void BackUneversal()
        {
            PressButtonBack();
            PressButtonBack();
            PressButtonBack();
            PressButtonBack();
            textRecogntion.TakeScreenshot();
            Thread.Sleep(2000);
            if (textRecogntion.CheckTextInScreenshot("Spiel", "verlassen?", null!) == true)
            {
                PressButtonBack();
            }
        }


        internal void GoWelt()
        {
            ClickAtTouchPositionWithHexa("00000081", "0000004f"); // Bonusübersicht klick
            Thread.Sleep(3000);
            ClickAtTouchPositionWithHexa("000001cf", "000003a6"); // Kraft klick
            Thread.Sleep(3000);
            ClickAtTouchPositionWithHexa("000002f1", "00000540"); // Technologieforschung wälen
            Thread.Sleep(5000);
            ClickAtTouchPositionWithHexa("0000032f", "000005fd"); // Welt / Stadt
            Thread.Sleep(5000);
        }


        internal void GoStadt()
        {
            ClickAtTouchPositionWithHexa("00000081", "0000004f"); // Bonusübersicht klick
            Thread.Sleep(3000);
            ClickAtTouchPositionWithHexa("000001cf", "000003a6"); // Kraft klick
            Thread.Sleep(3000);
            ClickAtTouchPositionWithHexa("000002f1", "00000540"); // Technologieforschung wälen
            Thread.Sleep(5000);
        }


        internal void SeitenMenuOpen()
        {
            ClickAtTouchPositionWithHexa("00000017", "000002b0"); // Öffne Seitenmenü
            Thread.Sleep(3000);
        }


        internal void SeitenMenuClose()
        {
            ClickAtTouchPositionWithHexa("0000023f", "000002a6"); // Schliese das Seitenmenü
            Thread.Sleep(2000);
        }


        internal void SeitenMenuScrolDown()
        {
            ClickAndHoldAndScroll("0000005b", "000003ab", "00000025", "000000b5", 300, 500); // Switsche runter im Seitenmenü
            Thread.Sleep(2000);
        }


        internal void OfflineErtregeAbholen()
        {

            logging.LogAndConsoleWirite($"\n\nChekce Offline Erträge");
            logging.LogAndConsoleWirite("---------------------------------------------------------------------------");
            textRecogntion.TakeScreenshot();
            bool offlineErtrege = textRecogntion.CheckTextInScreenshot("Willkommen", "Offline", null!);
            if (offlineErtrege == true)
            {
                ClickAtTouchPositionWithHexa("000001bf", "000004d3"); // Bestätigen Button klicken
                Thread.Sleep(3000);
                logging.LogAndConsoleWirite($"Offline Erträge wurden abgeholt.");
                gameStats.OfflineEarningsCounter++;
            }
            else
            {
                logging.LogAndConsoleWirite($"Keine Offline Erträge.");
            }
        }


        public void ClickAtTouchPositionWithHexa(string hexX, string hexY)
        {
            int x = int.Parse(hexX, System.Globalization.NumberStyles.HexNumber);
            int y = int.Parse(hexY, System.Globalization.NumberStyles.HexNumber);

            string adbCommand = $"shell input tap {x} {y}";
            adb.ExecuteAdbCommand(adbCommand);
        }


        public void PressButtonBack()
        {
            string adbCommand = "shell input keyevent 4";
            adb.ExecuteAdbCommand(adbCommand);
        }


        public void ClickAndHoldAndScroll(string startXHex, string startYHex, string endXHex, string endYHex, int holdDuration, int scrollDuration)
        {

            // Hex-Werte in Dezimalwerte umwandeln
            int startX = int.Parse(startXHex, System.Globalization.NumberStyles.HexNumber);
            int startY = int.Parse(startYHex, System.Globalization.NumberStyles.HexNumber);
            int endX = int.Parse(endXHex, System.Globalization.NumberStyles.HexNumber);
            int endY = int.Parse(endYHex, System.Globalization.NumberStyles.HexNumber);

            // Schritt 1: Klicken und Halten (Finger auf dem Bildschirm gedrückt halten)
            string adbCommandHold = $"shell input swipe {startX} {startY} {startX} {startY} {holdDuration}";
            adb.ExecuteAdbCommand(adbCommandHold);

            // Schritt 2: Ziehen (Finger auf dem Bildschirm bewegen)
            string adbCommandScroll = $"shell input swipe {startX} {startY} {endX} {endY} {scrollDuration}";
            adb.ExecuteAdbCommand(adbCommandScroll);
        }
    }
}
