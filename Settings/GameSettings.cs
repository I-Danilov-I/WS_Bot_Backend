namespace WhiteoutSurvival_Bot.Settings
{
    public class GameSettings
    {
        // Einstellungen für das Spiel
        internal bool TruppenAusgleich { get; set; } = false; // Truppen ausgleichen
        internal int TruppTrainAnzahl { get; set; } = 500;
        internal bool AllianceAutobeitritt { get; set; } = true;
        internal int PolarTerrorLevel { get; set; } = 6;
        internal int BestienJagtLevel { get; set; } = 26;
    }
}
