namespace SimpleRPG.Core.Entities.Heroes
{
    internal class EthanHero : Entity
    {
        public EthanHero(int level)
        {
            Name = "Ethan";
            Description = "A brave warrior with a strong sense of justice.";
            Level = level;
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
