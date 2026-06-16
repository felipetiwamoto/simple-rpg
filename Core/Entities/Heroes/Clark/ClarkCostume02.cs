namespace SimpleRPG.Core.Entities.Heroes
{
    internal class ClarkCostume02 : Costume
    {
        public ClarkCostume02(int copies)
        {
            Name = "ClarkCostume02 Name";
            Description = "ClarkCostume02 Description";
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
