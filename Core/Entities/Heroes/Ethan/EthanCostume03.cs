namespace SimpleRPG.Core.Entities.Heroes.Ethan
{
    internal class EthanCostume03 : Costume
    {
        public EthanCostume03(int Copies)
        {
            this.Name = "EthanCostume03 Name";
            this.Description = "EthanCostume03 Description";
            this.Copies = Copies;
            this.Cost = 4;
            this.Hits = 1;
            this.Range = new int[][] {
                new int[] { 0, 1, 0 },
                new int[] { 1, 2, 1 },
                new int[] { 0, 1, 0 },
            };
            this.Stats.Atk = 100;
        }

        public override void applyCopiesGrowth()
        {
            if (this.Copies >= 1) this.Cost--;
            if (this.Copies >= 2) this.Stats.Atk = 150;
            if (this.Copies >= 3)
            {
                this.Range = new int[][] {
                    new int[] { 1, 1, 1 },
                    new int[] { 1, 2, 1 },
                    new int[] { 1, 1, 1 },
                };
            }
            if (this.Copies >= 4) this.Stats.Atk = 200;
            if (this.Copies >= 5) this.Stats.Atk = 250;
        }
    }
}
