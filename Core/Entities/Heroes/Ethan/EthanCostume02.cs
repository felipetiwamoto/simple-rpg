namespace SimpleRPG.Core.Entities.Heroes.Ethan
{
    internal class EthanCostume02 : Costume
    {
        public EthanCostume02(int Copies)
        {
            this.Name = "EthanCostume02 Name";
            this.Description = "EthanCostume02 Description";
            this.Copies = Copies;
            this.Cost = 3;
            this.Hits = 2;
            this.Range = new int[][] {
                new int[] { 0, 0, 0 },
                new int[] { 0, 2, 0 },
                new int[] { 0, 0, 0 },
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
                    new int[] { 0, 0, 0 },
                    new int[] { 1, 2, 1 },
                    new int[] { 0, 0, 0 },
                };
            }
            if (this.Copies >= 4) this.Stats.Atk = 200;
            if (this.Copies >= 5) this.Stats.Atk = 250;
        }
    }
}
