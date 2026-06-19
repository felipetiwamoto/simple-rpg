using SimpleRPG.Core.Fight;

namespace SimpleRPG.Core.Entities.Enemies
{
    internal class OrcCostume02 : Costume
    {
        public OrcCostume02(int copies)
        {
            Name = "OrcCostume02 Name";
            Description = "OrcCostume02 Description";
            Copies = copies;
            Cost = 4;
            Hits = 1;
            Range = new int[][]
            {
                new int[] { 0, 1, 0 },
                new int[] { 0, 2, 0 },
                new int[] { 0, 1, 0 },
            };
            Stats.Atk = 200;
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
                    new int[] { 1, 2, 1 },
                    new int[] { 0, 1, 0 },
                };
            }
            if (Copies >= 4) Stats.Atk += 50;
            if (Copies >= 5) Stats.Atk += 50;
        }
    }
}