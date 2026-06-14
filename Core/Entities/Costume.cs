namespace SimpleRPG.Core.Entities
{
    internal abstract class Costume
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Copies { get; set; }
        public int Cost { get; set; }
        public int Hits { get; set; }
        public int[][] Range { get; set; } = Array.Empty<int[]>();
        public EntityStats Stats { get; set; } = new();

        public abstract void ApplyCopiesGrowth();
    }
}
