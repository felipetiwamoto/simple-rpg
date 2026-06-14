namespace SimpleRPG.Core.Entities.Heroes.Ethan
{
    internal class EthanHero : Entity
    {
        public EthanHero(int Level) {
            this.Name = "Ethan";
            this.Description = "A brave warrior with a strong sense of justice.";
            this.Level = Level;
            this.applyLevelGrowth();
        }
        override public void applyLevelGrowth()
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
