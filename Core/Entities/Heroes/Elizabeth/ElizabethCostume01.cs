using SimpleRPG.Core.Fight;

namespace SimpleRPG.Core.Entities.Heroes
{
    internal class ElizabethCostume01 : Costume
    {
        public ElizabethCostume01(int copies)
        {
            Name = "ElizabethCostume01 Name";
            Description = "ElizabethCostume01 Description";
            Copies = copies;
            Cost = 4;
            Hits = 1;
            Range = new int[][]
            {
                new int[] { 0, 0, 0 },
                new int[] { 0, 1, 0 },
                new int[] { 0, 2, 0 },
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
            if (Copies >= 2) Stats.Atk = 300;
            if (Copies >= 3)
            {
                Range = new int[][]
                {
                    new int[] { 0, 0, 0 },
                    new int[] { 0, 1, 0 },
                    new int[] { 1, 2, 1 },
                };
            }
            if (Copies >= 4) Stats.Atk = 400;
            if (Copies >= 5) Stats.Atk = 500;
        }
    }
}
