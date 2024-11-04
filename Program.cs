using System.Diagnostics;

namespace WhiteoutSurvival_Bot
{

    public static class Program
    {
        // Erstellen des Service Providers
        public static ServicesInitializer botControl = new ServicesInitializer();
        private static Stopwatch stopwatch = new Stopwatch();

        public static double elapsedMinutesNew = stopwatch.Elapsed.TotalMinutes; 

        internal static void Main()
        {
            while (true)
            {

                try
                {
                    botControl.Logging.LogAndConsoleWirite(botControl.GameScore.GetAllCounters());
                   
                    // Truppen Heilen
                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.TruppenHeilen.Heilen();
                    Time();

                    // Geheimdienst
                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.Geheimdienst.StartProcess();
                    Time();

                    // Allianz Kisten
                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.Allianz.KistenAbholen();
                    Time();

                    // Allianz Technologie BEitrag
                    stopwatch.Restart();
                    botControl.Stability.CheckStability();
                    botControl.Allianz.TechnologieBeitrag(5);
                    botControl.Stability.CheckStability();
                    Time();


                    // Allianz Hilfe geben
                    stopwatch.Restart();
                    botControl.Allianz.Hilfe();
                    botControl.Stability.CheckStability();
                    Time();

                    // Allianz Autobeitritt 
                    stopwatch.Restart();
                    botControl.Allianz.AutobeitritAktivieren();
                    botControl.Stability.CheckStability();
                    Time();

                    // Arena
                    stopwatch.Restart();
                    botControl.Arena.GoToArena();
                    botControl.Stability.CheckStability();
                    Time();

                    // Lebensbaum
                    stopwatch.Restart();
                    botControl.LebensBaum.BaumBelohnungAbholen();
                    botControl.Stability.CheckStability();
                    Time();

                    // LAebensbaun von Freunden
                    stopwatch.Restart();
                    botControl.LebensBaum.EssensVonFreundenAbholen();
                    botControl.Stability.CheckStability();
                    Time();

                    // Truppen Training
                    stopwatch.Restart();
                    botControl.TruppenTraining.TrainiereLatenzTreger(500);
                    botControl.Stability.CheckStability();
                    Time();
                    stopwatch.Restart();
                    botControl.TruppenTraining.TrainiereInfaterie(500);
                    botControl.Stability.CheckStability();
                    Time();
                    stopwatch.Restart();
                    botControl.TruppenTraining.TrainiereSniper(500);
                    botControl.Stability.CheckStability();
                    Time();

                    // Erkundung Abholen und Kampf
                    stopwatch.Restart();
                    botControl.Erkundung.StartProcess();
                    botControl.Stability.CheckStability();
                    Time();

                    // VIp Kiste abholen
                    stopwatch.Restart();
                    botControl.VIP.KistenAbholen();
                    botControl.Stability.CheckStability();
                    Time();

                    // Eilauftrag
                    stopwatch.Restart();
                    botControl.GuvenourBefehl.EilauftragAbholen();
                    botControl.Stability.CheckStability();
                    Time();

                    // Fetlichkeitsauftrag
                    stopwatch.Restart();
                    botControl.GuvenourBefehl.FestlichkeitenAbholen();
                    botControl.Stability.CheckStability();
                    Time();

                    // HElden Rekurt
                    stopwatch.Restart();
                    botControl.Helden.HeldenRekrutieren();
                    botControl.Stability.CheckStability();
                    Time();

                    // PolarTerror
                    //stopwatch.Restart();
                    //botControl.Jagt.PolarTerrorStarten(6);
                    //botControl.Stability.CheckStability();
                    //Time();

                    // BEtienjagt
                    stopwatch.Restart();
                    botControl.Jagt.BestienJagtStarten(25);
                    botControl.Stability.CheckStability();
                    Time();

                    // Arena
                    stopwatch.Restart();
                    botControl.Arena.GoToArena();
                    botControl.Stability.CheckStability();
                    Time();

                    // Lager belohnung Ausdauer
                    stopwatch.Restart();
                    botControl.LagerOnlineBelohnung.AusdauerAbholen();
                    botControl.Stability.CheckStability();
                    Time();

                    // Lager belohnung Geschenk
                    stopwatch.Restart();
                    botControl.LagerOnlineBelohnung.GeschnekAbholen();
                    botControl.Stability.CheckStability();
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
            double elapsed = elapsedMinutesNew += elapsedMinutes;


            botControl.Logging.PrintFormatted($"Verstrichene Zeit: ", $"{elapsedMinutes:F2} Minuten");
            botControl.Logging.PrintFormatted($"Verstrichene Zeit: ", $"{elapsedSeconds:F2} Sekunden");
            botControl.Logging.PrintFormatted($"Verstrichene Gesamt Zeit: ", $"{elapsed:F2} Min");
            botControl.Logging.LogAndConsoleWirite("_____________________________________________________________________________");
        }

    }
}
