using SimpleRPG.Core.Fight;

namespace SimpleRPG.Core.Entities.Heroes
{
    internal class ElizabethCostume02 : Costume
    {
        public ElizabethCostume02(int copies)
        {
            Name = "ElizabethCostume02 Name";
            Description = "ElizabethCostume02 Description";
            Copies = copies;
            Cost = 3;
            Hits = 2;
            Range = new int[][]
            {
                new int[] { 0, 0, 0 },
                new int[] { 0, 2, 0 },
                new int[] { 0, 0, 0 },
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
            if (Copies >= 2) Stats.Atk = 150;
            if (Copies >= 3)
            {
                Range = new int[][]
                {
                    new int[] { 0, 0, 0 },
                    new int[] { 1, 2, 1 },
                    new int[] { 0, 0, 0 },
                };
            }
            if (Copies >= 4) Stats.Atk = 200;
            if (Copies >= 5) Stats.Atk = 250;
        }
    }
}
