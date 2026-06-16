using SimpleRPG.Core.Enums;

namespace SimpleRPG.Core.Entities.Enemies
{
    internal class DummyEnemy : Entity
    {
        public DummyEnemy(int level, PropertyEnum property)
        {
            Name = "Dummy";
            Description = "Dummy Description";
            Level = level;
            Property = property;
            Target = TargetEnum.First;
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
