namespace SimpleRPG.Core.Entities
{
    internal class EntityStats
    {
        public int MaxHp { get; set; }
        public int Atk { get; set; }
        public int Matk { get; set; }
        public float Def { get; set; }
        public float Mdef { get; set; }
        public float CritRate { get; set; }
        public float CritDmg { get; set; }
        public float PropertyDmg { get; set; }

        public void CopyFrom(EntityStats stats)
        {
            MaxHp = stats.MaxHp;
            Atk = stats.Atk;
            Matk = stats.Matk;
            Def = stats.Def;
            Mdef = stats.Mdef;
            CritRate = stats.CritRate;
            CritDmg = stats.CritDmg;
            PropertyDmg = stats.PropertyDmg;
        }
    }
}
