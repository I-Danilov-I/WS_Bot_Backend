using System.Diagnostics;

namespace WhiteoutSurvival_Bot
{

    public static class Program
    {
        // Erstellen des Service Providers
        public static ServicesInitializer botControl = new ServicesInitializer();
        private static Stopwatch stopwatch = new Stopwatch();

        internal static void Main()
        {
            //stability.CheckStability();
            while (true)
            {
                try
                {
                    botControl.Logging.LogAndConsoleWirite(botControl.GameScore.GetAllCounters());

                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.Geheimdienst.StartProcess();
                    Time();


                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.Erkundung.StartProcess();
                    Time();

                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.TruppenHeilen.Heilen();
                    Time();

                        
                    Time();
                }
                catch (Exception e)
                {
                    botControl.Logging.LogAndConsoleWirite(e.Message);
                }
            }

        }


        internal static void Time()
        {
            botControl.Logging.LogAndConsoleWirite("_____________________________________________________________________________");
            stopwatch.Stop();
            double elapsedMinutes = stopwatch.Elapsed.TotalMinutes;
            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            botControl.Logging.PrintFormatted($"Verstrichene Zeit: ", $"{elapsedMinutes:F2} Minuten");
            botControl.Logging.PrintFormatted($"Verstrichene Zeit: ", $"{elapsedSeconds:F2} Sekunden");
            botControl.Logging.PrintFormatted($"Verstrichene Gesamt Zeit: ", $"{elapsedSeconds + elapsedMinutes:F2}");
            botControl.Logging.LogAndConsoleWirite("_____________________________________________________________________________");
        }

    }
}
