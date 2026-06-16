namespace SimpleRPG.Core.Entities.Heroes
{
    internal class MarcoCostume03 : Costume
    {
        public MarcoCostume03(int copies)
        {
            Name = "MarcoCostume03 Name";
            Description = "MarcoCostume03 Description";
            Copies = copies;
            Cost = 4;
            Hits = 1;
            Range = new int[][]
            {
                new int[] { 0, 1, 0 },
                new int[] { 1, 2, 1 },
                new int[] { 0, 1, 0 },
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
                    new int[] { 1, 1, 1 },
                    new int[] { 1, 2, 1 },
                    new int[] { 1, 1, 1 },
                };
            }
            if (Copies >= 4) Stats.Atk = 200;
            if (Copies >= 5) Stats.Atk = 250;
        }
    }
}
