
namespace WhiteoutSurvival_Bot.StableControl
{

    public class Stability(Log.Logging logging,
        DeviceControl.PcControl pcControl,
        DeviceControl.NoxControl noxControl,
        DeviceControl.AdbControl adbControl,
        DeviceControl.GameControl gameControl,
        DeviceControl.AppControl appControl)
    {


        public void CheckStability()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            logging.LogAndConsoleWirite("\n\n[ STABILITY CONTROL ]");
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------");

            logging.LogAndConsoleWirite($"PC Network: {pcControl.IsInternetConnected()}");
            StabileAdbConnetion();
            StabileNoxRun();
            StableNoxNetwork();
            StableAppRun();
          
            StableAppResponsive();
            logging.LogAndConsoleWirite($"GAME Accaunt {gameControl.IsAccountUsage()}");

            logging.LogAndConsoleWirite($"Null Possition");
            gameControl.BackUneversal();
            gameControl.GoStadt();
            logging.LogAndConsoleWirite($"Null Possition True");

            Console.ForegroundColor = ConsoleColor.Yellow;
            logging.LogAndConsoleWirite("-----------------------------------------------------------------------------\n\n");
        }


        public void StabileAdbConnetion()
        {
            bool adbConnected = adbControl.IsAdbConnected();
            if (adbConnected == false)
            {
                logging.LogAndConsoleWirite($"ADB Connection {adbConnected} Starting...");
                adbControl.StartADBConnection();
                int i3 = 0;
                while (i3 < 10)
                {
                    i3++;
                    bool adbConnected2 = adbControl.IsAdbConnected();
                    if (adbConnected2 == false)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        logging.LogAndConsoleWirite($"ADB Connetion {adbConnected2}");
                        return;
                    }
                }
                throw new Exception("ADB can't conecting.");

            }
            else
            {
                logging.LogAndConsoleWirite($"ADB Connetion {adbConnected}");
            }
        }


        public void StabileNoxRun() 
        {
            bool isNoxRun = noxControl.IsNoxRunning();
            if (isNoxRun == false)
            {
                logging.LogAndConsoleWirite($"NOX Ready {isNoxRun} Starting...");
                int i1 = 0;
                noxControl.StartNoxPlayer();
                while (i1 < 60)
                {
                    i1++;
                    bool noxReady = noxControl.IsNoxReady();
                    if (noxReady == false)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        logging.LogAndConsoleWirite($"NOX Ready {noxReady}");
                        return;
                    }
                }
                throw new Exception("NOX can't Running.");
            }
        }


        public void StableNoxNetwork()
        {
            bool isNoxNetworkConnented = noxControl.IsNoxNetworkConnected();
            if (isNoxNetworkConnented == false)
            {
                logging.LogAndConsoleWirite($"NOX Network {isNoxNetworkConnented} Waiting...");
                int i2 = 0;
                while (i2 < 60)
                {
                    i2++;
                    bool isNoxNetworkConnented2 = noxControl.IsNoxNetworkConnected();
                    if (isNoxNetworkConnented2 == false)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        logging.LogAndConsoleWirite($"NOX Network {isNoxNetworkConnented}");
                        return;
                    }
                }
                throw new Exception("NOX Network can't connect.");

            }
            else 
            {
                logging.LogAndConsoleWirite($"NOX Network {isNoxNetworkConnented}");
            }

            
        }


        public void StableAppRun()
        {
            bool isAppRun = appControl.IsAppRun();
            if (isAppRun == false)
            {
                logging.LogAndConsoleWirite($"APP Run {isAppRun} Starting...");
                appControl.StartApp();
                Thread.Sleep(50 * 1000);
                int i4 = 0;
                while (i4< 60) 
                {
                    i4++;
                    bool isAppRun2 = appControl.IsAppRun();
                    if(isAppRun2 == false)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        logging.LogAndConsoleWirite($"APP Run {isAppRun2}");
                        return;
                    }
                }
                throw new Exception("App can't startup.");
            }
            else
            {
                logging.LogAndConsoleWirite($"APP Run {isAppRun}");
            }
        }


        public void StableAppResponsive()
        {
            bool isAppResponsive = appControl.IsAppResponsiv();
            if (isAppResponsive == false)
            {
                logging.LogAndConsoleWirite($"APP Responsiv {isAppResponsive} Restarting...");
                int i6 = 0;
                while (i6 < 60)
                {
                    i6++;
                    bool isAppResponsive2 = appControl.IsAppResponsiv();
                    if (isAppResponsive2 == false)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        logging.LogAndConsoleWirite($"APP Responsiv {appControl.IsAppResponsiv()}");
                        return;
                    }
                }
                throw new Exception("App Responsive");

            }
            else
            {
                logging.LogAndConsoleWirite($"APP Responsiv {appControl.IsAppResponsiv()}");
            }
        }

    }
}
