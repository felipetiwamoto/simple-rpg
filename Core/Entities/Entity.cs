using SimpleRPG.Core.Enums;
using SimpleRPG.Core.Equips;

namespace SimpleRPG.Core.Entities
{
    internal abstract class Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Level { get; set; }
        public int CurrentHp { get; set; }
        public Costume[] Costumes { get; set; } = Array.Empty<Costume>();
        public PropertyEnum Property { get; set; } = PropertyEnum.Neutral;
        public TargetEnum Target { get; set; } = TargetEnum.First;
        public EntityStats BaseStats { get; } = new();
        public EntityStats FinalStats { get; } = new();
        public Equip[] Equips { get; protected set; } = Array.Empty<Equip>();

        public float BonusEvery5Levels(float value)
        {
            return value * MathF.Floor((Level + 5) / 10f);
        }
        public float BonusEvery10Levels(float value)
        {
            return value * MathF.Floor(Level / 10f);
        }
        public void AddCostume(Costume costume)
        {
            if (Costumes.Any(item => item.Name == costume.Name)) return;
            Costumes = Costumes.Append(costume).ToArray();
        }
        public abstract void ApplyLevelGrowth();
    }
}
