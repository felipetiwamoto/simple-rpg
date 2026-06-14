namespace SimpleRPG.Core.Entities
{
    internal abstract class Costume
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Copies { get; set; }
        public int Cost { get; set; }
        public int Hits { get; set; }
        public int[][] Range { get; set; }
        public EntityStats Stats { get; set; } = new();

        abstract public void applyCopiesGrowth();
    }
}
