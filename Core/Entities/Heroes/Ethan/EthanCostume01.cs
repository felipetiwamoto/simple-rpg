namespace SimpleRPG.Core.Entities.Heroes.Ethan
{
    internal class EthanCostume01 : Costume
    {
        public EthanCostume01(int Copies)
        {
            this.Name = "EthanCostume01 Name";
            this.Description = "EthanCostume01 Description";
            this.Copies = Copies;
            this.Cost = 4;
            this.Hits = 1;
            this.Range = new int[][] {
                new int[] { 0, 0, 0 },
                new int[] { 0, 1, 0 },
                new int[] { 0, 2, 0 },
            };
            this.Stats.Atk = 200;
        }

        public override void applyCopiesGrowth()
        {
            if (this.Copies >= 1) this.Cost--;
            if (this.Copies >= 2) this.Stats.Atk = 300;
            if (this.Copies >= 3)
            {
                this.Range = new int[][] {
                    new int[] { 0, 0, 0 },
                    new int[] { 0, 1, 0 },
                    new int[] { 1, 2, 1 },
                };
            }
            if (this.Copies >= 4) this.Stats.Atk = 400;
            if (this.Copies >= 5) this.Stats.Atk = 500;
        }
    }
}
