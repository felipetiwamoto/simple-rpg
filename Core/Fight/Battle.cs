using System.Text;

namespace SimpleRPG.Core.Fight
{
    internal enum BattleInfoMode
    {
        Default
    }

    internal class Battle
    {
        public Board HeroesBoard { get; set; }
        public Board EnemiesBoard { get; set; }
        public int Turn { get; set; } = 1;
        public string? Winner { get; set; }
        public BattleInfoMode InfoMode { get; set; } = BattleInfoMode.Default;

        public Battle(Board heroesBoard, Board enemiesBoard)
        {
            HeroesBoard = heroesBoard;
            EnemiesBoard = enemiesBoard;
        }

        public void Print()
        {
            const int width = 100;
            const int height = 25;

            char[][] panel = CreatePanel(width, height);

            const int menuWidth = 20;
            const int borderSpacing = 1;

            WriteMenu(
                panel,
                HeroesBoard.PrintMenu(),
                1 + borderSpacing,
                reverse: false);
            WriteMenu(
                panel,
                EnemiesBoard.PrintMenu(reverse: true),
                width - 1 - borderSpacing - menuWidth,
                reverse: true);

            const int enemiesSpRow = 1;
            const int emptyLineCount = 1;

            int enemiesBoardStartRow = enemiesSpRow + emptyLineCount + 1;
            int heroesBoardStartRow =
                enemiesBoardStartRow + EnemiesBoard.Rows + emptyLineCount;
            int heroesSpRow =
                heroesBoardStartRow + HeroesBoard.Rows + emptyLineCount;

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
            WriteInfo(panel, PrintInfo());

            StringBuilder output = new(width * height);

            for (int row = 0; row < height; row++)
            {
                output.Append(panel[row]);

                if (row < height - 1) output.AppendLine();
            }

            if (!Console.IsOutputRedirected)
            {
                Console.SetCursorPosition(0, 0);
            }

            Console.Write(output);
        }

        public string[] PrintInfo()
        {
            return InfoMode switch
            {
                BattleInfoMode.Default => PrintInfoDefault(),
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

        public void PrintEdges()
        {
            const int width = 100;
            const int height = 25;

            char[][] panel = CreatePanel(width, height);
            StringBuilder output = new(width * height);

            for (int row = 0; row < height; row++)
            {
                output.Append(panel[row]);

                if (row < height - 1) output.AppendLine();
            }

            if (!Console.IsOutputRedirected)
            {
                Console.SetCursorPosition(0, 0);
            }

            Console.Write(output);
        }

        private static char[][] CreatePanel(int width, int height)
        {
            char[][] panel = new char[height][];

            for (int row = 0; row < height; row++)
            {
                panel[row] = new string(' ', width).ToCharArray();

                if (row == 0 || row == height - 1)
                {
                    Array.Fill(panel[row], '-');
                }
                else
                {
                    panel[row][0] = '|';
                    panel[row][width - 1] = '|';
                }
            }

            return panel;
        }

        private static void WriteMenu(
            char[][] panel,
            string[] menuLines,
            int startColumn,
            bool reverse)
        {
            int availableRows = panel.Length - 2;
            int linesToWrite = Math.Min(menuLines.Length, availableRows);

            for (int line = 0; line < linesToWrite; line++)
            {
                bool isSeparator = menuLines[line].All(character => character == '-');
                int lineStartColumn = isSeparator && !reverse
                    ? startColumn - 1
                    : startColumn;

                menuLines[line].CopyTo(
                    sourceIndex: 0,
                    destination: panel[line + 1],
                    destinationIndex: lineStartColumn,
                    count: menuLines[line].Length);
            }
        }

        private static void WriteTerrain(
            char[][] panel,
            Dictionary<int, string> terrainLines)
        {
            foreach ((int row, string text) in terrainLines)
            {
                if (row <= 0 || row >= panel.Length - 1)
                {
                    continue;
                }

                int startColumn = (panel[row].Length - text.Length) / 2;

                text.CopyTo(
                    sourceIndex: 0,
                    destination: panel[row],
                    destinationIndex: startColumn,
                    count: text.Length);
            }
        }

        private static void WriteInfo(char[][] panel, string[] infoLines)
        {
            const int infoHeight = 5;

            int separatorRow = panel.Length - infoHeight - 2;
            int infoStartRow = separatorRow + 1;
            int contentWidth = panel[0].Length - 2;

            Array.Fill(panel[separatorRow], '-');

            for (int line = 0; line < infoHeight; line++)
            {
                string text = line < infoLines.Length
                    ? infoLines[line]
                    : string.Empty;
                string visibleText = text.Length > contentWidth
                    ? text[..contentWidth]
                    : text;

                visibleText.CopyTo(
                    sourceIndex: 0,
                    destination: panel[infoStartRow + line],
                    destinationIndex: 1,
                    count: visibleText.Length);
            }
        }
    }
}
