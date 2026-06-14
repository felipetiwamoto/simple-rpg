namespace SimpleRPG.Core.Entities.Heroes.Maria
{
    internal class MariaCostume03 : Costume
    {
        public MariaCostume03(int Copies)
        {
            Name = "MariaCostume03 Name";
            Description = "MariaCostume03 Description";
            this.Copies = Copies;
            Cost = 4;
            Hits = 1;
            Range = new int[][]
            {
                new int[] { 0, 1, 0 },
                new int[] { 1, 2, 1 },
                new int[] { 0, 1, 0 },
            };
            Stats.Atk = 100;
        }

        public override void applyCopiesGrowth()
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
