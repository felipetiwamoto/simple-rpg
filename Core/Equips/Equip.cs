using SimpleRPG.Core.Enums;

namespace SimpleRPG.Core.Equips
{
    internal abstract class Equip
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EquipTypeEnum Type { get; set; }
        public int Rarity { get; set; }
        public int Level { get; set; }
        public int FlatHp { get; set; }
        public float PercentHp { get; set; }
        public int FlatAtk { get; set; }
        public float PercentAtk { get; set; }
        public int FlatMatk { get; set; }
        public float PercentMatk { get; set; }
        public float Def { get; set; }
        public float Mdef { get; set; }
        public float CritRate { get; set; }
        public float CritDmg { get; set; }

        public abstract void ApplyLevelGrowth(int level);
    }
}
