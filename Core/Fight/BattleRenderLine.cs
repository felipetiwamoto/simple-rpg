namespace SimpleRPG.Core.Fight
{
    internal readonly record struct TextHighlight(int Start, int Length);

    internal class BattleRenderLine
    {
        public string Text { get; }
        public TextHighlight[] Highlights { get; }

        public BattleRenderLine(
            string text,
            params TextHighlight[] highlights)
        {
            Text = text;
            Highlights = highlights;
        }
    }
}
