namespace SimpleRPG.Core.Entities.Heroes.Maria
{
    internal class MariaCostume01 : Costume
    {
        public MariaCostume01(int Copies)
        {
            Name = "MariaCostume01 Name";
            Description = "MariaCostume01 Description";
            this.Copies = Copies;
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

        public override void applyCopiesGrowth()
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
