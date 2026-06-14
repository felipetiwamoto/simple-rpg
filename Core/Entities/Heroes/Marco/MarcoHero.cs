namespace SimpleRPG.Core.Entities.Heroes.Marco
{
    internal class MarcoHero : Entity
    {
        public MarcoHero(int Level)
        {
            Name = "Marco";
            Description = "Marco Description";
            this.Level = Level;
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
