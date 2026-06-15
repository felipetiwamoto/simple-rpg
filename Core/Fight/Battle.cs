using System.Text;
using SimpleRPG.Core.Entities;

namespace SimpleRPG.Core.Fight
{
    internal enum BattleViewMode
    {
        Default,
        EntityMenu,
        CostumeSubMenu
    }

    internal enum BattleSide
    {
        Heroes,
        Enemies
    }

    internal class Battle
    {
        private const int PanelWidth = 100;
        private const int PanelHeight = 25;
        private const string GreenAnsi = "\u001b[32m";
        private const string ResetAnsi = "\u001b[0m";

        private int _selectedCostumeIndex;

        public Board HeroesBoard { get; set; }
        public Board EnemiesBoard { get; set; }
        public int Turn { get; set; } = 1;
        public string? Winner { get; set; }
        public BattleViewMode ViewMode { get; private set; }
            = BattleViewMode.Default;
        public BattleSide? ActiveSide { get; private set; }
        public List<BattleAction> Actions { get; } = new();

        public Battle(Board heroesBoard, Board enemiesBoard)
        {
            HeroesBoard = heroesBoard;
            EnemiesBoard = enemiesBoard;
        }

        public void Run()
        {
            if (Console.IsInputRedirected)
            {
                Print();
                return;
            }

            Console.CursorVisible = false;

            try
            {
                bool isRunning = true;

                while (isRunning)
                {
                    Print();
                    isRunning = HandleInput(Console.ReadKey(intercept: true).Key);
                }
            }
            finally
            {
                Console.CursorVisible = true;
                Console.WriteLine();
            }
        }

        public bool HandleInput(ConsoleKey key)
        {
            return ViewMode switch
            {
                BattleViewMode.Default => HandleDefaultInput(key),
                BattleViewMode.EntityMenu => HandleEntityMenuInput(key),
                BattleViewMode.CostumeSubMenu =>
                    HandleCostumeSubMenuInput(key),
                _ => true
            };
        }

        public void Print()
        {
            PanelCell[][] panel = CreatePanel(PanelWidth, PanelHeight);

            const int borderSpacing = 1;
            int heroesMenuStart = 1 + borderSpacing;
            int enemiesMenuStart =
                PanelWidth - 1 - borderSpacing - Board.MenuWidth;

            WriteLines(
                panel,
                HeroesBoard.PrintMenu(),
                heroesMenuStart,
                startRow: 1,
                reverse: false);
            WriteLines(
                panel,
                EnemiesBoard.PrintMenu(reverse: true),
                enemiesMenuStart,
                startRow: 1,
                reverse: true);

            if (ViewMode == BattleViewMode.CostumeSubMenu
                && ActiveSide is not null)
            {
                Board activeBoard = GetActiveBoard();
                bool reverse = ActiveSide == BattleSide.Enemies;
                int subMenuStart = reverse
                    ? enemiesMenuStart - Board.MenuWidth - 1
                    : heroesMenuStart + Board.MenuWidth + 1;

                WriteLines(
                    panel,
                    activeBoard.PrintSubMenu(
                        _selectedCostumeIndex,
                        reverse),
                    subMenuStart,
                    startRow: 1,
                    reverse);
            }

            WriteTerrains(panel);
            WriteInfo(panel, PrintInfo());

            string output = BuildOutput(
                panel,
                useColor: !Console.IsOutputRedirected);

            if (!Console.IsOutputRedirected)
            {
                Console.SetCursorPosition(0, 0);
            }

            Console.Write(output);
        }

        public string[] PrintInfo()
        {
            return ViewMode switch
            {
                BattleViewMode.Default => PrintInfoDefault(),
                BattleViewMode.EntityMenu => PrintInfoEntityMenu(),
                BattleViewMode.CostumeSubMenu => PrintInfoCostumeSubMenu(),
                _ => PrintInfoDefault()
            };
        }

        public string[] PrintInfoDefault()
        {
            return
            [
                $"Turn: {Turn}",
                "[1] Heroes menu [2] Enemies menu [3] Leave",
                string.Empty,
                string.Empty,
                string.Empty
            ];
        }

        public string[] PrintInfoEntityMenu()
        {
            string entityName = ActiveSide == BattleSide.Heroes
                ? "Hero"
                : "Enemy";

            return
            [
                $"[Arrow Up] Previous {entityName} "
                    + $"[Arrow Down] Next {entityName} "
                    + "[Enter] Open Costumes",
                "[Backspace] Back",
                string.Empty,
                string.Empty,
                string.Empty
            ];
        }

        public string[] PrintInfoCostumeSubMenu()
        {
            Costume? costume = GetSelectedCostume();

            if (costume is null)
            {
                return
                [
                    "No costumes",
                    GetSubMenuBackCommand(),
                    string.Empty,
                    string.Empty,
                    string.Empty
                ];
            }

            string[] rangeLines = FormatRange(costume.Range);
            int rangeWidth = rangeLines.Length == 0
                ? 0
                : rangeLines.Max(line => line.Length);
            int descriptionWidth = PanelWidth - 2 - rangeWidth - 2;
            string[] descriptionLines = WrapText(
                costume.Description,
                descriptionWidth,
                maximumLines: 5);
            string[] infoLines = new string[5];

            for (int line = 0; line < infoLines.Length; line++)
            {
                string range = line < rangeLines.Length
                    ? rangeLines[line].PadRight(rangeWidth)
                    : new string(' ', rangeWidth);
                string description = line < descriptionLines.Length
                    ? descriptionLines[line]
                    : string.Empty;

                infoLines[line] = rangeWidth > 0
                    ? $"{range}  {description}"
                    : description;
            }

            return infoLines;
        }

        private bool HandleDefaultInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    OpenEntityMenu(BattleSide.Heroes);
                    return true;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    OpenEntityMenu(BattleSide.Enemies);
                    return true;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return false;
                default:
                    return true;
            }
        }

        private bool HandleEntityMenuInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MoveEntitySelection(-1);
                    break;
                case ConsoleKey.DownArrow:
                    MoveEntitySelection(1);
                    break;
                case ConsoleKey.Enter:
                    _selectedCostumeIndex = 0;
                    ViewMode = BattleViewMode.CostumeSubMenu;
                    break;
                case ConsoleKey.Backspace:
                    CloseMenus();
                    break;
            }

            return true;
        }

        private bool HandleCostumeSubMenuInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MoveCostumeSelection(-1);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCostumeSelection(1);
                    break;
                case ConsoleKey.Enter:
                    ConfirmSelectedCostume();
                    break;
                case ConsoleKey.Backspace:
                    CloseSubMenu();
                    break;
                case ConsoleKey.LeftArrow
                    when ActiveSide == BattleSide.Heroes:
                    CloseSubMenu();
                    break;
                case ConsoleKey.RightArrow
                    when ActiveSide == BattleSide.Enemies:
                    CloseSubMenu();
                    break;
            }

            return true;
        }

        private void OpenEntityMenu(BattleSide side)
        {
            HeroesBoard.SelectedEntity = null;
            EnemiesBoard.SelectedEntity = null;
            ActiveSide = side;
            ViewMode = BattleViewMode.EntityMenu;
            _selectedCostumeIndex = 0;

            Board board = GetActiveBoard();
            board.SelectedEntity = board.EntitiesInBoard.FirstOrDefault();
        }

        private void MoveEntitySelection(int direction)
        {
            Board board = GetActiveBoard();

            if (board.EntitiesInBoard.Length == 0)
            {
                board.SelectedEntity = null;
                return;
            }

            int currentIndex = Array.IndexOf(
                board.EntitiesInBoard,
                board.SelectedEntity);
            int nextIndex = WrapIndex(
                currentIndex + direction,
                board.EntitiesInBoard.Length);

            board.SelectedEntity = board.EntitiesInBoard[nextIndex];
            _selectedCostumeIndex = 0;
        }

        private void MoveCostumeSelection(int direction)
        {
            int costumeCount =
                GetActiveBoard().SelectedEntity?.Entity.Costumes.Length ?? 0;

            if (costumeCount == 0)
            {
                _selectedCostumeIndex = 0;
                return;
            }

            _selectedCostumeIndex = WrapIndex(
                _selectedCostumeIndex + direction,
                costumeCount);
        }

        private void ConfirmSelectedCostume()
        {
            if (ActiveSide != BattleSide.Heroes)
            {
                return;
            }

            Board board = HeroesBoard;
            EntityInBoard? source = board.SelectedEntity;
            Costume? costume = GetSelectedCostume();

            if (source is null || costume is null)
            {
                return;
            }

            int existingActionIndex = Actions.FindIndex(action =>
                ReferenceEquals(action.Source, source));
            BattleAction action = new(source, costume);

            if (existingActionIndex >= 0)
            {
                Actions[existingActionIndex] = action;
            }
            else
            {
                Actions.Add(action);
            }
        }

        private void CloseSubMenu()
        {
            _selectedCostumeIndex = 0;
            ViewMode = BattleViewMode.EntityMenu;
        }

        private void CloseMenus()
        {
            HeroesBoard.SelectedEntity = null;
            EnemiesBoard.SelectedEntity = null;
            ActiveSide = null;
            ViewMode = BattleViewMode.Default;
            _selectedCostumeIndex = 0;
        }

        private Board GetActiveBoard()
        {
            return ActiveSide == BattleSide.Enemies
                ? EnemiesBoard
                : HeroesBoard;
        }

        private Costume? GetSelectedCostume()
        {
            Costume[] costumes =
                GetActiveBoard().SelectedEntity?.Entity.Costumes
                ?? Array.Empty<Costume>();

            if (costumes.Length == 0)
            {
                return null;
            }

            _selectedCostumeIndex = WrapIndex(
                _selectedCostumeIndex,
                costumes.Length);

            return costumes[_selectedCostumeIndex];
        }

        private string GetSubMenuBackCommand()
        {
            string arrow = ActiveSide == BattleSide.Enemies
                ? "Right Arrow"
                : "Left Arrow";

            return $"[Backspace/{arrow}] Back";
        }

        private void WriteTerrains(PanelCell[][] panel)
        {
            const int enemiesSpRow = 1;
            const int emptyLineCount = 1;

            int enemiesBoardStartRow =
                enemiesSpRow + emptyLineCount + 1;
            int heroesBoardStartRow =
                enemiesBoardStartRow
                + EnemiesBoard.Rows
                + emptyLineCount;
            int heroesSpRow =
                heroesBoardStartRow
                + HeroesBoard.Rows
                + emptyLineCount;

            WriteTerrain(
                panel,
                EnemiesBoard.PrintTerrain(
                    enemiesBoardStartRow,
                    enemiesSpRow,
                    reverse: true));
            WriteTerrain(
                panel,
                HeroesBoard.PrintTerrain(
                    heroesBoardStartRow,
                    heroesSpRow));
        }

        private static PanelCell[][] CreatePanel(int width, int height)
        {
            PanelCell[][] panel = new PanelCell[height][];

            for (int row = 0; row < height; row++)
            {
                panel[row] = Enumerable
                    .Repeat(new PanelCell(' '), width)
                    .ToArray();

                if (row == 0 || row == height - 1)
                {
                    for (int column = 0; column < width; column++)
                    {
                        panel[row][column] = new PanelCell('-');
                    }
                }
                else
                {
                    panel[row][0] = new PanelCell('|');
                    panel[row][width - 1] = new PanelCell('|');
                }
            }

            return panel;
        }

        private static void WriteLines(
            PanelCell[][] panel,
            BattleRenderLine[] lines,
            int startColumn,
            int startRow,
            bool reverse)
        {
            int availableRows = panel.Length - startRow - 1;
            int linesToWrite = Math.Min(lines.Length, availableRows);

            for (int lineIndex = 0;
                lineIndex < linesToWrite;
                lineIndex++)
            {
                BattleRenderLine line = lines[lineIndex];
                bool isSeparator =
                    line.Text.All(character => character == '-');
                int lineStartColumn = isSeparator && !reverse
                    ? startColumn - 1
                    : startColumn;

                WriteLine(
                    panel,
                    startRow + lineIndex,
                    lineStartColumn,
                    line);
            }
        }

        private static void WriteTerrain(
            PanelCell[][] panel,
            Dictionary<int, BattleRenderLine> terrainLines)
        {
            foreach ((int row, BattleRenderLine line) in terrainLines)
            {
                if (row <= 0 || row >= panel.Length - 1)
                {
                    continue;
                }

                int startColumn =
                    (panel[row].Length - line.Text.Length) / 2;

                WriteLine(panel, row, startColumn, line);
            }
        }

        private static void WriteInfo(
            PanelCell[][] panel,
            string[] infoLines)
        {
            const int infoHeight = 5;

            int separatorRow = panel.Length - infoHeight - 2;
            int infoStartRow = separatorRow + 1;
            int contentWidth = panel[0].Length - 2;

            for (int column = 0;
                column < panel[separatorRow].Length;
                column++)
            {
                panel[separatorRow][column] = new PanelCell('-');
            }

            for (int line = 0; line < infoHeight; line++)
            {
                string text = line < infoLines.Length
                    ? infoLines[line]
                    : string.Empty;
                string visibleText = text.Length > contentWidth
                    ? text[..contentWidth]
                    : text;

                WriteLine(
                    panel,
                    infoStartRow + line,
                    startColumn: 1,
                    new BattleRenderLine(visibleText));
            }
        }

        private static void WriteLine(
            PanelCell[][] panel,
            int row,
            int startColumn,
            BattleRenderLine line)
        {
            if (row < 0 || row >= panel.Length)
            {
                return;
            }

            int writableLength = Math.Min(
                line.Text.Length,
                panel[row].Length - startColumn);

            for (int index = 0; index < writableLength; index++)
            {
                bool isGreen = line.Highlights.Any(highlight =>
                    index >= highlight.Start
                    && index < highlight.Start + highlight.Length);

                panel[row][startColumn + index] =
                    new PanelCell(line.Text[index], isGreen);
            }
        }

        private static string BuildOutput(
            PanelCell[][] panel,
            bool useColor)
        {
            StringBuilder output = new(
                PanelWidth * PanelHeight + 256);

            for (int row = 0; row < panel.Length; row++)
            {
                bool greenIsActive = false;

                foreach (PanelCell cell in panel[row])
                {
                    if (useColor && cell.IsGreen != greenIsActive)
                    {
                        output.Append(
                            cell.IsGreen ? GreenAnsi : ResetAnsi);
                        greenIsActive = cell.IsGreen;
                    }

                    output.Append(cell.Character);
                }

                if (useColor && greenIsActive)
                {
                    output.Append(ResetAnsi);
                }

                if (row < panel.Length - 1)
                {
                    output.AppendLine();
                }
            }

            return output.ToString();
        }

        private static string[] FormatRange(int[][] range)
        {
            return range
                .Select(row => string.Concat(row.Select(value => value switch
                {
                    1 => "[x]",
                    2 => "[X]",
                    _ => "[ ]"
                })))
                .ToArray();
        }

        private static string[] WrapText(
            string text,
            int width,
            int maximumLines)
        {
            if (string.IsNullOrWhiteSpace(text) || width <= 0)
            {
                return Array.Empty<string>();
            }

            List<string> lines = new();
            string[] words = text.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);
            StringBuilder currentLine = new();

            foreach (string word in words)
            {
                if (word.Length > width)
                {
                    if (currentLine.Length > 0)
                    {
                        lines.Add(currentLine.ToString());
                        currentLine.Clear();
                    }

                    lines.Add(word[..width]);
                }
                else if (currentLine.Length == 0)
                {
                    currentLine.Append(word);
                }
                else if (currentLine.Length + 1 + word.Length <= width)
                {
                    currentLine.Append(' ').Append(word);
                }
                else
                {
                    lines.Add(currentLine.ToString());
                    currentLine.Clear();
                    currentLine.Append(word);
                }

                if (lines.Count == maximumLines)
                {
                    return lines.ToArray();
                }
            }

            if (currentLine.Length > 0 && lines.Count < maximumLines)
            {
                lines.Add(currentLine.ToString());
            }

            return lines.ToArray();
        }

        private static int WrapIndex(int index, int count)
        {
            return (index % count + count) % count;
        }

        private readonly record struct PanelCell(
            char Character,
            bool IsGreen = false);
    }
}
