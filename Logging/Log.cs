namespace WhiteoutSurvival_Bot.Log
{
    public class Logging : Settings.Configuration
    {

        public void loading()
        {
            PrintFormatetInSameLine("Seraching", ".");
            PrintFormatetInSameLine("Seraching", "..");
            PrintFormatetInSameLine("Seraching", "...");
        }




        // Der dritte Parameter ist optional und standardmäßig leer
        // Methode zum formatieren und farbigen Ausgeben mit festgelegten Spaltenbreiten
        internal void PrintFormatted(string label, string status, string? additionalInfo = "")
        {
            Console.ForegroundColor = ConsoleColor.Green;

            // Definiere die Breiten für die Spalten
            int labelWidth = 35;      // Breite für das Label, etwas breiter für Konsistenz
            int statusWidth = 34;     // Breite für den Status (True/False)
            int additionalWidth = 35; // Breite für Zusatzinfo, damit es rechts ausgerichtet ist

            // Label wird linksbündig ausgegeben
            string formattedLabel = label.PadRight(labelWidth);
            // Status (`True` oder `False`) wird linksbündig mit `statusWidth` ausgegeben
            string formattedStatus = status.PadRight(statusWidth);

            // Zusatzinfo wird rechtsbündig ausgegeben (nur wenn vorhanden)
            string formattedInfo = string.IsNullOrWhiteSpace(additionalInfo) ? "" : $" => {additionalInfo}".PadLeft(additionalWidth);

            // Ausgabe im gewünschten Format
            LogAndConsoleWirite($"[ {formattedLabel} : {formattedStatus} {formattedInfo} ]");

            Console.ResetColor();
        }


        internal void PrintFormatetInSameLine(string label, string value)
        {

            // Setze die Farbe des Wertes auf Grün.
            Console.ForegroundColor = ConsoleColor.Green;
            // Setzte Abstand zwischen den Werten
            int labelWidth = 21;

            // Formatiere das Label, damit es rechts mit Leerzeichen aufgefüllt wird.
            string formattedLabel = label.PadRight(labelWidth);

            // Der Wert wird direkt übernommen, mit optionaler Ausrichtung.
            string formattedValue = value.PadLeft(labelWidth);

            // Speichere die aktuelle Cursorposition und gehe an den Anfang der Zeile.
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;
            Console.SetCursorPosition(0, currentTop);

            // Schreibe das formatierte Label und den Wert in einem einheitlichen Format.
            LogAndConsoleWirite($"{formattedLabel} {formattedValue}");

            // Setze den Cursor zurück an die Originalposition, falls weitere Ausgaben folgen.
            Console.SetCursorPosition(currentLeft, currentTop);

            // Setze die Konsolenfarbe zurück auf den Standard.
            Console.ResetColor();
        }


        internal void LogAndConsoleWirite(string inputString)
        {
            // Schreiben in die Datei und Konsole
            using (StreamWriter writer = new StreamWriter(LogFileFolderPathWithName, true)) // 'true' bedeutet, dass an die Datei angehängt wird
            {
                if (!string.IsNullOrEmpty(inputString))
                {
                    Console.WriteLine($"{inputString}");
                    writer.WriteLine($"{inputString}");
                }
            }        
        }

    }
}
