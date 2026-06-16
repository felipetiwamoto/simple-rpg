using SimpleRPG.Core.Entities;

namespace SimpleRPG.Core.Fight
{
    internal class Board
    {
        public const int MenuWidth = 17;

        public int Rows { get; set; }
        public int Columns { get; set; }
        public int CurrentSp { get; set; } = 20;
        public int MaxSp { get; set; } = 20;
        public int[][] InRange { get; set; } = Array.Empty<int[]>();
        public EntityInBoard? HoveringEntity { get; set; }
        public EntityInBoard? SelectedEntity { get; set; }
        public Costume? HoveringCostume { get; set; }
        public EntityInBoard[] EntitiesInBoard { get; set; }

        public Board(int rows, int columns, EntityInBoard[] entitiesInBoard)
        {
            Rows = rows;
            Columns = columns;
            EntitiesInBoard = entitiesInBoard;
        }

        public void PrintMenu(bool reverse = false)
        {
            const int panelWidth = 100;
            int menuStartColumn = reverse ? panelWidth - MenuWidth - 2 : 0;

            for (int i = 0; i < 9; i++)
            {
                int currentLine = 1 + i * 3;

                Console.SetCursorPosition(menuStartColumn, currentLine);
                Console.Write($"|{new string(' ', MenuWidth)}|");

                Console.SetCursorPosition(menuStartColumn, currentLine + 1);
                Console.Write($"|{new string(' ', MenuWidth)}|");

                Console.SetCursorPosition(menuStartColumn, currentLine + 2);
                Console.Write(new string('-', MenuWidth + 2));

                EntityInBoard? entityInBoard = i < EntitiesInBoard.Length ? EntitiesInBoard[i] : null;

                if (entityInBoard is not null)
                {
                    Entity entity = entityInBoard.Entity;
                    string hp = $"{entity.CurrentHp}/{entity.FinalStats.MaxHp}";
                    int nameColumn = reverse
                        ? menuStartColumn + MenuWidth - entity.Name.Length
                        : menuStartColumn + 2;
                    int hpColumn = reverse
                        ? menuStartColumn + MenuWidth - hp.Length
                        : menuStartColumn + 2;

                    ConsoleColor previousColor = Console.ForegroundColor;
                    if (HoveringEntity?.Entity == entity) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(nameColumn, currentLine);
                    Console.Write(entity.Name);

                    Console.SetCursorPosition(hpColumn, currentLine + 1);
                    Console.Write(hp);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        public void PrintSubMenu(bool reverse = false)
        {
            const int panelWidth = 100;
            const int gap = 1;
            int subMenuStartColumn = reverse ? panelWidth - (MenuWidth + 2) * 2 - gap : MenuWidth + 2 + gap;
            Costume[] costumes = HoveringEntity?.Entity.Costumes ?? Array.Empty<Costume>();

            for (int i = 0; i < 6; i++)
            {
                int currentLine = 1 + i * 3;

                Console.SetCursorPosition(subMenuStartColumn, currentLine);
                Console.Write($"|{new string(' ', MenuWidth)}|");

                Console.SetCursorPosition(subMenuStartColumn, currentLine + 1);
                Console.Write($"|{new string(' ', MenuWidth)}|");

                Console.SetCursorPosition(subMenuStartColumn, currentLine + 2);
                Console.Write(new string('-', MenuWidth + 2));

                Costume? costume = i < costumes.Length ? costumes[i] : null;

                if (costume is not null)
                {
                    string costumeName = costume.Name.Length > 15
                        ? costume.Name[..15]
                        : costume.Name;
                    string cost = $"SP: {costume.Cost}";
                    int nameColumn = reverse
                        ? subMenuStartColumn + MenuWidth - costumeName.Length
                        : subMenuStartColumn + 2;
                    int costColumn = reverse
                        ? subMenuStartColumn + MenuWidth - cost.Length
                        : subMenuStartColumn + 2;

                    ConsoleColor previousColor = Console.ForegroundColor;
                    if (HoveringCostume == costume) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(nameColumn, currentLine);
                    Console.Write(costumeName);

                    Console.SetCursorPosition(costColumn, currentLine + 1);
                    Console.Write(cost);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        public void PrintTerrain(bool reverse = false)
        {
            const int panelWidth = 100;
            const int enemiesSpLine = 2;
            int enemyBoardStartLine = enemiesSpLine + 2;
            int heroBoardStartLine = enemyBoardStartLine + Rows + 1;
            int heroesSpLine = heroBoardStartLine + Rows + 1;

            if (reverse)
            {
                PrintCenteredText($"Enemies SP: {CurrentSp}/{MaxSp}", enemiesSpLine, panelWidth);
                PrintBoard(enemyBoardStartLine, panelWidth, reverse: true);
                return;
            }

            PrintBoard(heroBoardStartLine, panelWidth);
            PrintCenteredText($"Heroes SP: {CurrentSp}/{MaxSp}", heroesSpLine, panelWidth);
        }

        private void PrintBoard(
            int startLine,
            int panelWidth,
            bool reverse = false)
        {
            int boardWidth = Columns * 3;
            int boardStartColumn = (panelWidth - boardWidth) / 2;

            for (int displayRow = 0; displayRow < Rows; displayRow++)
            {
                int sourceRow = reverse ? Rows - 1 - displayRow : displayRow;

                for (int displayColumn = 0; displayColumn < Columns; displayColumn++)
                {
                    int sourceColumn = reverse ? Columns - 1 - displayColumn : displayColumn;
                    EntityInBoard? entityInBoard = EntitiesInBoard.FirstOrDefault(entityInBoard =>
                        entityInBoard.Row == sourceRow && entityInBoard.Col == sourceColumn);
                    bool isHoveringEntity = entityInBoard is not null && HoveringEntity == entityInBoard;
                    string cell = entityInBoard is not null ? "[o]" : "[ ]";
                    ConsoleColor previousColor = Console.ForegroundColor;

                    if (isHoveringEntity) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(
                        boardStartColumn + displayColumn * 3,
                        startLine + displayRow);
                    Console.Write(cell);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        private static void PrintCenteredText(
            string text,
            int line,
            int panelWidth)
        {
            int column = (panelWidth - text.Length) / 2;

            Console.SetCursorPosition(column, line);
            Console.Write(text);
        }
    }
}


//using SimpleRPG.Core.Entities;

//namespace SimpleRPG.Core.Fight
//{
//    internal class Board
//    {
//        public const int MenuWidth = 17;

//        public int Rows { get; set; }
//        public int Columns { get; set; }
//        public int CurrentSp { get; set; } = 20;
//        public int MaxSp { get; set; } = 20;
//        public int[][] InRange { get; set; } = Array.Empty<int[]>();
//        public EntityInBoard? SelectedEntity { get; set; }
//        public EntityInBoard[] EntitiesInBoard { get; set; }

//        public Board(int rows, int columns, EntityInBoard[] entitiesInBoard)
//        {
//            Rows = rows;
//            Columns = columns;
//            EntitiesInBoard = entitiesInBoard;
//        }

//        public BattleRenderLine[] PrintMenu(bool reverse = false)
//        {
//            List<BattleRenderLine> menuLines = new();

//            foreach (EntityInBoard entityInBoard in EntitiesInBoard)
//            {
//                bool isSelected = ReferenceEquals(entityInBoard, SelectedEntity);
//                string name = FitText(entityInBoard.Entity.Name, MenuWidth - 1);
//                string hp =
//                    $"{entityInBoard.Entity.CurrentHp}/{entityInBoard.Entity.FinalStats.MaxHp}";

//                string nameLine = AlignText(name, reverse);
//                string hpLine = AlignText(hp, reverse);
//                int nameStart = reverse
//                    ? MenuWidth - name.Length
//                    : 0;

//                menuLines.Add(isSelected
//                    ? new BattleRenderLine(
//                        nameLine,
//                        new TextHighlight(nameStart, name.Length))
//                    : new BattleRenderLine(nameLine));
//                menuLines.Add(new BattleRenderLine(hpLine));
//                menuLines.Add(new BattleRenderLine(
//                    new string('-', MenuWidth + 1)));
//            }

//            return menuLines.ToArray();
//        }

//        public BattleRenderLine[] PrintSubMenu(
//            int selectedCostumeIndex,
//            bool reverse = false)
//        {
//            if (SelectedEntity is null)
//            {
//                return Array.Empty<BattleRenderLine>();
//            }

//            Costume[] costumes = SelectedEntity.Entity.Costumes;

//            if (costumes.Length == 0)
//            {
//                return
//                [
//                    new BattleRenderLine(AlignText("No costumes", reverse))
//                ];
//            }

//            List<BattleRenderLine> subMenuLines = new();

//            for (int index = 0; index < costumes.Length; index++)
//            {
//                Costume costume = costumes[index];
//                string name = FitText(costume.Name, MenuWidth - 1);
//                string nameLine = AlignText(name, reverse);
//                string spLine = AlignText($"SP: {costume.Cost}", reverse);
//                int nameStart = reverse
//                    ? MenuWidth - name.Length
//                    : 0;

//                subMenuLines.Add(index == selectedCostumeIndex
//                    ? new BattleRenderLine(
//                        nameLine,
//                        new TextHighlight(nameStart, name.Length))
//                    : new BattleRenderLine(nameLine));
//                subMenuLines.Add(new BattleRenderLine(spLine));
//                subMenuLines.Add(new BattleRenderLine(
//                    new string('-', MenuWidth + 1)));
//            }

//            return subMenuLines.ToArray();
//        }

//        public Dictionary<int, BattleRenderLine> PrintTerrain(
//            int boardStartRow,
//            int spRow,
//            bool reverse = false)
//        {
//            Dictionary<int, BattleRenderLine> terrainLines = new();
//            string teamName = reverse ? "Enemies" : "Heroes";

//            terrainLines[spRow] = new BattleRenderLine(
//                $"{teamName} SP: {CurrentSp}/{MaxSp}");

//            HashSet<(int Row, int Col)> occupiedPositions = EntitiesInBoard
//                .Select(entityInBoard => (entityInBoard.Row, entityInBoard.Col))
//                .ToHashSet();

//            for (int displayRow = 0; displayRow < Rows; displayRow++)
//            {
//                int sourceRow = reverse
//                    ? Rows - 1 - displayRow
//                    : displayRow;
//                string boardRow = string.Empty;
//                List<TextHighlight> highlights = new();

//                for (int displayColumn = 0;
//                    displayColumn < Columns;
//                    displayColumn++)
//                {
//                    int sourceColumn = reverse
//                        ? Columns - 1 - displayColumn
//                        : displayColumn;
//                    bool isOccupied =
//                        occupiedPositions.Contains((sourceRow, sourceColumn));
//                    bool isSelected =
//                        SelectedEntity?.Row == sourceRow
//                        && SelectedEntity.Col == sourceColumn;

//                    int cellStart = boardRow.Length;
//                    boardRow += isOccupied ? "[o]" : "[ ]";

//                    if (isSelected)
//                    {
//                        highlights.Add(new TextHighlight(cellStart, 3));
//                    }
//                }

//                terrainLines[boardStartRow + displayRow] =
//                    new BattleRenderLine(boardRow, highlights.ToArray());
//            }

//            return terrainLines;
//        }

//        private static string AlignText(string text, bool reverse)
//        {
//            const int contentWidth = MenuWidth - 1;
//            string visibleText = FitText(text, contentWidth);

//            return reverse
//                ? $"|{visibleText.PadLeft(contentWidth)}"
//                : $"{visibleText.PadRight(contentWidth)}|";
//        }

//        private static string FitText(string text, int width)
//        {
//            return text.Length > width
//                ? text[..width]
//                : text;
//        }
//    }
//}
