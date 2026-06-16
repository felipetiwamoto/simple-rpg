using SimpleRPG.Core.Enums;

namespace SimpleRPG.Core.Entities.Heroes
{
    internal class MariaHero : Entity
    {
        public MariaHero(int level)
        {
            Name = "Maria";
            Description = "Maria Description";
            Level = level;
            Property = PropertyEnum.Water;
            Target = TargetEnum.Next;
            ApplyLevelGrowth();
        }

        public override void ApplyLevelGrowth()
        {
            BaseStats.MaxHp += Level * 50;
            BaseStats.Atk += Level * 5;

            BaseStats.Mdef += BonusEvery10Levels(0.5f);
            BaseStats.CritRate += BonusEvery10Levels(1f);
            BaseStats.CritDmg += BonusEvery10Levels(5f);

            BaseStats.Def += BonusEvery5Levels(0.5f);
            BaseStats.CritDmg += BonusEvery5Levels(5f);

            CurrentHp = BaseStats.MaxHp;
            FinalStats.CopyFrom(BaseStats);
        }
    }
}
