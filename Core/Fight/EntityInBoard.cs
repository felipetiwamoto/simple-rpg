using SimpleRPG.Core.Entities;

namespace SimpleRPG.Core.Fight
{
    internal class EntityInBoard
    {
        public Entity Entity { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Costume? SelectedCostume { get; set; }
        public int Order { get; set; }

        public EntityInBoard(Entity entity, int row, int col, int order)
        {
            Entity = entity;
            Row = row;
            Col = col;
            Order = order;
        }
    }
}
