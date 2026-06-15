using SimpleRPG.Core.Entities;

namespace SimpleRPG.Core.Fight
{
    internal class Board
    {
        public const int MenuWidth = 20;

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

        public BattleRenderLine[] PrintMenu(bool reverse = false)
        {
            List<BattleRenderLine> menuLines = new();

            foreach (EntityInBoard entityInBoard in EntitiesInBoard)
            {
                bool isSelected = ReferenceEquals(entityInBoard, SelectedEntity);
                string name = FitText(entityInBoard.Entity.Name, MenuWidth - 1);
                string hp =
                    $"{entityInBoard.Entity.CurrentHp}/{entityInBoard.Entity.FinalStats.MaxHp}";

                string nameLine = AlignText(name, reverse);
                string hpLine = AlignText(hp, reverse);
                int nameStart = reverse
                    ? MenuWidth - name.Length
                    : 0;

                menuLines.Add(isSelected
                    ? new BattleRenderLine(
                        nameLine,
                        new TextHighlight(nameStart, name.Length))
                    : new BattleRenderLine(nameLine));
                menuLines.Add(new BattleRenderLine(hpLine));
                menuLines.Add(new BattleRenderLine(
                    new string('-', MenuWidth + 1)));
            }

            return menuLines.ToArray();
        }

        public BattleRenderLine[] PrintSubMenu(
            int selectedCostumeIndex,
            bool reverse = false)
        {
            if (SelectedEntity is null)
            {
                return Array.Empty<BattleRenderLine>();
            }

            Costume[] costumes = SelectedEntity.Entity.Costumes;

            if (costumes.Length == 0)
            {
                return
                [
                    new BattleRenderLine(AlignText("No costumes", reverse))
                ];
            }

            List<BattleRenderLine> subMenuLines = new();

            for (int index = 0; index < costumes.Length; index++)
            {
                Costume costume = costumes[index];
                string name = FitText(costume.Name, MenuWidth - 1);
                string nameLine = AlignText(name, reverse);
                string spLine = AlignText($"SP: {costume.Cost}", reverse);
                int nameStart = reverse
                    ? MenuWidth - name.Length
                    : 0;

                subMenuLines.Add(index == selectedCostumeIndex
                    ? new BattleRenderLine(
                        nameLine,
                        new TextHighlight(nameStart, name.Length))
                    : new BattleRenderLine(nameLine));
                subMenuLines.Add(new BattleRenderLine(spLine));
                subMenuLines.Add(new BattleRenderLine(
                    new string('-', MenuWidth + 1)));
            }

            return subMenuLines.ToArray();
        }

        public Dictionary<int, BattleRenderLine> PrintTerrain(
            int boardStartRow,
            int spRow,
            bool reverse = false)
        {
            Dictionary<int, BattleRenderLine> terrainLines = new();
            string teamName = reverse ? "Enemies" : "Heroes";

            terrainLines[spRow] = new BattleRenderLine(
                $"{teamName} SP: {CurrentSp}/{MaxSp}");

            HashSet<(int Row, int Col)> occupiedPositions = EntitiesInBoard
                .Select(entityInBoard => (entityInBoard.Row, entityInBoard.Col))
                .ToHashSet();

            for (int displayRow = 0; displayRow < Rows; displayRow++)
            {
                int sourceRow = reverse
                    ? Rows - 1 - displayRow
                    : displayRow;
                string boardRow = string.Empty;
                List<TextHighlight> highlights = new();

                for (int displayColumn = 0;
                    displayColumn < Columns;
                    displayColumn++)
                {
                    int sourceColumn = reverse
                        ? Columns - 1 - displayColumn
                        : displayColumn;
                    bool isOccupied =
                        occupiedPositions.Contains((sourceRow, sourceColumn));
                    bool isSelected =
                        SelectedEntity?.Row == sourceRow
                        && SelectedEntity.Col == sourceColumn;

                    int cellStart = boardRow.Length;
                    boardRow += isOccupied ? "[o]" : "[ ]";

                    if (isSelected)
                    {
                        highlights.Add(new TextHighlight(cellStart, 3));
                    }
                }

                terrainLines[boardStartRow + displayRow] =
                    new BattleRenderLine(boardRow, highlights.ToArray());
            }

            return terrainLines;
        }

        private static string AlignText(string text, bool reverse)
        {
            const int contentWidth = MenuWidth - 1;
            string visibleText = FitText(text, contentWidth);

            return reverse
                ? $"|{visibleText.PadLeft(contentWidth)}"
                : $"{visibleText.PadRight(contentWidth)}|";
        }

        private static string FitText(string text, int width)
        {
            return text.Length > width
                ? text[..width]
                : text;
        }
    }
}
