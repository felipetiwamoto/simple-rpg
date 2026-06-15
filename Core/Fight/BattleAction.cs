using SimpleRPG.Core.Entities;

namespace SimpleRPG.Core.Fight
{
    internal class BattleAction
    {
        public EntityInBoard Source { get; }
        public Costume Costume { get; }

        public BattleAction(EntityInBoard source, Costume costume)
        {
            Source = source;
            Costume = costume;
        }
    }
}
