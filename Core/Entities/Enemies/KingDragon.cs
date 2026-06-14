using SimpleRPG.Core.Enums;

namespace SimpleRPG.Core.Entities.Enemies
{
    internal class KingDragon : Entity
    {
        public KingDragon(int Level, PropertyEnum Property)
        {
            Name = "King Dragon";
            Description = "King Dragon Description";
            this.Level = Level;
            this.Property = Property;
            applyLevelGrowth();
        }

        public override void applyLevelGrowth()
        {
            BaseStats.MaxHP += Level * 50;
            BaseStats.Atk += Level * 5;

            BaseStats.Mdef += bonusEvery10Levels(0.5f);
            BaseStats.CritRate += bonusEvery10Levels(1f);
            BaseStats.CritDmg += bonusEvery10Levels(5f);

            BaseStats.Def += bonusEvery5Levels(0.5f);
            BaseStats.CritDmg += bonusEvery5Levels(5f);

            CurrentHP = BaseStats.MaxHP;
            FinalStats.copyFrom(BaseStats);
        }
    }
}
