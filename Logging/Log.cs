
namespace WhiteoutSurvival_Bot.Log
{
    public class Logging : Settings.Configuration
    {

        public Action<string> OnLogMessage { get; set; }

        
        internal void LogAndConsoleWirite(string inputString)
        {
            // Schreiben in die Datei und Konsole
            using (StreamWriter writer = new StreamWriter(LogFileFolderPathWithName, true)) // 'true' bedeutet, dass an die Datei angehängt wird
            {
                if (!string.IsNullOrEmpty(inputString))
                {
                    OnLogMessage?.Invoke(inputString); // Ruft die zugeordnete Action auf, um die Nachricht an die WPF-App zu senden
                    Console.WriteLine($"{inputString}");
                    writer.WriteLine($"{inputString}");
                }
            }        
        }


    }
}
