using SimpleRPG.Core.Fight;
using SimpleRPG.Core.Enums;

namespace SimpleRPG.Core.Entities
{
    internal abstract class Costume
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Copies { get; set; }
        public int Cost { get; set; }
        public int Hits { get; set; }
        public int[][] Range { get; set; } = Array.Empty<int[]>();
        public EntityStats Stats { get; set; } = new();

        public int DamageCalc(Entity hero, Entity enemy)
        {
            float baseDamage = hero.FinalStats.Matk == 0 ? hero.FinalStats.Atk : hero.FinalStats.Matk;
            bool isCritical = Random.Shared.NextDouble() * 100 < hero.FinalStats.CritRate;
            float critMultiplier = isCritical ? 1 + hero.FinalStats.CritDmg / 100 : 1;
            float propertyMultiplier = 1 + hero.FinalStats.PropertyDmg / 100;
            float weaknessMultiplier = HasPropertyDisadvantage(hero.Property, enemy.Property) ? 2 : 1;

            return (int)(baseDamage * critMultiplier * propertyMultiplier * weaknessMultiplier);
        }

        public abstract void Run(Battle battle);
        public abstract void ApplyCopiesGrowth();

        private static bool HasPropertyDisadvantage(
            PropertyEnum attackerProperty,
            PropertyEnum defenderProperty)
        {
            return attackerProperty switch
            {
                PropertyEnum.Fire => defenderProperty == PropertyEnum.Water,
                PropertyEnum.Water => defenderProperty == PropertyEnum.Air,
                PropertyEnum.Air => defenderProperty == PropertyEnum.Fire,
                PropertyEnum.Dark => defenderProperty == PropertyEnum.Light,
                PropertyEnum.Light => defenderProperty == PropertyEnum.Dark,
                _ => false
            };
        }
    }
}
