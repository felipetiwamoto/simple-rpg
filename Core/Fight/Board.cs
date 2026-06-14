namespace SimpleRPG.Core.Fight
{
    internal class Board
    {
        private const int MenuWidth = 20;

        public int Rows { get; set; }
        public int Columns { get; set; }
        public int CurrentSp { get; set; } = 20;
        public int MaxSp { get; set; } = 20;
        public int[][] InRange { get; set; } = Array.Empty<int[]>();
        public EntityInBoard? SelectedEntity { get; set; }
        public EntityInBoard[] EntitiesInBoard { get; set; }

        public Board(int rows, int columns, EntityInBoard[] entitiesInBoard)
        {
            Rows = rows;
            Columns = columns;
            EntitiesInBoard = entitiesInBoard;
        }

        public string[] PrintMenu(bool reverse = false)
        {
            List<string> menuLines = new();

            foreach (EntityInBoard entityInBoard in EntitiesInBoard)
            {
                string name = entityInBoard.Entity.Name;
                string hp = $"{entityInBoard.Entity.CurrentHp}/{entityInBoard.Entity.FinalStats.MaxHp}";

                menuLines.Add(AlignText(name, reverse));
                menuLines.Add(AlignText(hp, reverse));
                menuLines.Add(new string('-', MenuWidth + 1));
            }

            return menuLines.ToArray();
        }

        public void PrintSubMenu() { }

        public Dictionary<int, string> PrintTerrain(
            int boardStartRow,
            int spRow,
            bool reverse = false)
        {
            Dictionary<int, string> terrainLines = new();
            string teamName = reverse ? "Enemies" : "Heroes";
            string spText = $"{teamName} SP: {CurrentSp}/{MaxSp}";

            terrainLines[spRow] = spText;

            HashSet<(int Row, int Col)> occupiedPositions = EntitiesInBoard
                .Select(entityInBoard => (entityInBoard.Row, entityInBoard.Col))
                .ToHashSet();

            for (int displayRow = 0; displayRow < Rows; displayRow++)
            {
                int sourceRow = reverse
                    ? Rows - 1 - displayRow
                    : displayRow;

                string boardRow = string.Concat(
                    Enumerable.Range(0, Columns).Select(displayColumn =>
                    {
                        int sourceColumn = reverse
                            ? Columns - 1 - displayColumn
                            : displayColumn;

                        return occupiedPositions.Contains((sourceRow, sourceColumn))
                            ? "[o]"
                            : "[ ]";
                    }));

                terrainLines[boardStartRow + displayRow] = boardRow;
            }

            return terrainLines;
        }

        private static string AlignText(string text, bool reverse)
        {
            const int contentWidth = MenuWidth - 1;

            string visibleText = text.Length > contentWidth
                ? text[..contentWidth]
                : text;

            return reverse
                ? $"|{ visibleText.PadLeft(contentWidth)}"
                : $"{ visibleText.PadRight(contentWidth)}|";
        }
    }
}
