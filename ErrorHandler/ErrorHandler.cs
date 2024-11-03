namespace WhiteoutSurvival_Bot.ErrorHandler
{
    internal static class ErrorHandler
    {
        // Allgemeine Fehlerbehandlung für jede Aktion
        public static void Handle(Action action, string errorContext = "Fehler aufgetreten")
        {
            try
            {
                action();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{errorContext}: Datei nicht gefunden - {ex.Message}");
                // Spezifische Behandlung für Datei-bezogene Fehler, wie Loggen oder Benachrichtigung
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"{errorContext}: Zugriff verweigert - {ex.Message}");
                // Behandlung für Zugriffsfehler
            }
            catch (IOException ex)
            {
                Console.WriteLine($"{errorContext}: Ein-/Ausgabefehler - {ex.Message}");
                // Behandlung für allgemeine E/A-Fehler
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{errorContext}: Allgemeiner Fehler - {ex.Message}");
                // Behandlung für andere Fehlerarten
            }
        }

        // Fehlerbehandlung mit Rückgabewert
        public static T Handle<T>(Func<T> func, T fallbackValue = default, string errorContext = "Fehler aufgetreten")
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{errorContext}: {ex.Message}");
                return fallbackValue;
            }
        }
    }
}
