using SimpleRPG.Core.Fight;

namespace SimpleRPG.Core.Entities.Enemies
{
    internal class KingSlimeCostume01 : Costume
    {
        public KingSlimeCostume01(int copies)
        {
            Name = "KingSlimeCostume01 Name";
            Description = "KingSlimeCostume01 Description";
            Copies = copies;
            Cost = 3;
            Hits = 1;
            Range = new int[][]
            {
                new int[] { 0, 0, 0 },
                new int[] { 0, 2, 0 },
                new int[] { 0, 1, 0 },
            };
            Stats.Atk = 100;
            ApplyCopiesGrowth();
        }

        public override void Run(Battle battle)
        {
        }

        public override void ApplyCopiesGrowth()
        {
            if (Copies >= 1) Cost--;
            if (Copies >= 2) Stats.Atk += 50;
            if (Copies >= 3)
            {
                Range = new int[][]
                {
                    new int[] { 0, 1, 0 },
                    new int[] { 0, 2, 0 },
                    new int[] { 0, 1, 0 },
                };
            }
            if (Copies >= 4) Stats.Atk += 50;
            if (Copies >= 5) Stats.Atk += 50;
        }
    }
}