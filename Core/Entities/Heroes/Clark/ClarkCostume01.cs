namespace SimpleRPG.Core.Entities.Heroes
{
    internal class ClarkCostume01 : Costume
    {
        public ClarkCostume01(int copies)
        {
            Name = "ClarkCostume01 Name";
            Description = "ClarkCostume01 Description";
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
