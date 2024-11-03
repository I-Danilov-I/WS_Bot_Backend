using System;
using System.Reflection;
using System.Text;

namespace WhiteoutSurvival_Bot.Settings
{
    internal class GameScore
    {
        internal int GeheimdienstCounter { get; set; } = 0;
        internal int OfflineEarningsCounter { get; set; } = 0;
        internal int StorageBonusGiftCounter { get; set; } = 0;
        internal int StorageBonusStaminaCounter { get; set; } = 0;
        internal int InfantryUnitsTrainedCounter { get; set; } = 0;
        internal int LatencyCarrierUnitsTrainedCounter { get; set; } = 0;
        internal int SniperUnitsTrainedCounter { get; set; } = 0;
        internal int ExplorationBonusCounter { get; set; } = 0;
        internal int ExplorationBattleCounter { get; set; } = 0;
        internal int AllianceHelpCounter { get; set; } = 0;
        internal int AllianceChestsCounter { get; set; } = 0;
        internal int AllianceTechnologyCounter { get; set; } = 0;
        internal int HealingCounter { get; set; } = 0;
        internal int AdvancedHeroRecruitmentCounter { get; set; } = 0;
        internal int EpicHeroRecruitmentCounter { get; set; } = 0;
        internal int BeastHuntCounter { get; set; } = 0;
        internal int LifeTreeEssenceCounter { get; set; } = 0;
        internal int VipStatusCounter { get; set; } = 0;
        internal int ArenaFightsCounter { get; set; } = 0;

        // Methode zum dynamischen Ausgeben aller Zählerwerte
        internal string GetAllCounters()
        {
            StringBuilder result = new StringBuilder();
            Type type = typeof(GameScore);

            // Überschrift und Trennlinie
            result.AppendLine("GameScore Counters:");
            result.AppendLine(new string('-', 77));

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (property.PropertyType == typeof(int))
                {
                    string name = property.Name;
                    int value = (int)property.GetValue(this);

                    // Schöne Formatierung mit PadRight für gleichmäßige Ausrichtung
                    result.AppendLine($"{name.PadRight(50)}: {value}");
                }
            }
            result.AppendLine(new string('-', 77));
            return result.ToString();
        }
    }
}
