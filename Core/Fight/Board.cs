using System.Text;
using SimpleRPG.Core.Entities;
using SimpleRPG.Core.Enums;

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
            if (reverse) PrintMenuEnemies();
            else PrintMenuHeroes();
        }

        public void PrintMenuHeroes()
        {
            for (int i = 0; i < 9; i++)
            {
                int currentLine = 1 + i * 3;

                Console.SetCursorPosition(0, currentLine);
                Console.Write($"|{new string(' ', MenuWidth)}|");

                Console.SetCursorPosition(0, currentLine + 1);
                Console.Write($"|{new string(' ', MenuWidth)}|");

                Console.SetCursorPosition(0, currentLine + 2);
                Console.Write(new string('-', MenuWidth + 2));

                EntityInBoard? entityInBoard = i < EntitiesInBoard.Length ? EntitiesInBoard[i] : null;

                if (entityInBoard is not null)
                {
                    Entity entity = entityInBoard.Entity;
                    string entityName = FitText(entity.Name, MenuWidth - 8);
                    string entityInfo = FormatEntityInfo(entity);
                    string hp = $"{entity.CurrentHp}/{entity.FinalStats.MaxHp}";

                    ConsoleColor previousColor = Console.ForegroundColor;
                    if (HoveringEntity?.Entity == entity) Console.ForegroundColor = ConsoleColor.Green;

                    PrintEntityInfo(entity, MenuWidth - entityInfo.Length + 1, currentLine - 1);

                    Console.SetCursorPosition(2, currentLine);
                    Console.Write(entityName);

                    //PrintEntityInfo(entity, MenuWidth - entityInfo.Length, currentLine);

                    Console.SetCursorPosition(2, currentLine + 1);
                    Console.Write(hp);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        public void PrintMenuEnemies()
        {
            const int panelWidth = 100;
            int menuStartColumn = panelWidth - MenuWidth - 2;

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
                    string entityInfo = FormatEntityInfo(entity);
                    string entityName = FitText(entity.Name, MenuWidth - 8);
                    string hp = $"{entity.CurrentHp}/{entity.FinalStats.MaxHp}";

                    ConsoleColor previousColor = Console.ForegroundColor;
                    if (HoveringEntity?.Entity == entity) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(menuStartColumn + 2, currentLine);
                    PrintEntityInfo(entity, menuStartColumn + 1, currentLine - 1);

                    Console.SetCursorPosition(menuStartColumn + MenuWidth - entityName.Length, currentLine);
                    Console.Write(entityName);

                    Console.SetCursorPosition(menuStartColumn + MenuWidth - hp.Length, currentLine + 1);
                    Console.Write(hp);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        public void PrintSubMenu(bool reverse = false)
        {
            if (reverse) PrintSubMenuEnemies();
            else PrintSubMenuHeroes();
        }

        public void PrintSubMenuHeroes()
        {
            const int gap = 1;
            int subMenuStartColumn = MenuWidth + 2 + gap;
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

                    ConsoleColor previousColor = Console.ForegroundColor;
                    if (HoveringCostume == costume) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(subMenuStartColumn + 2, currentLine);
                    Console.Write(costumeName);

                    Console.SetCursorPosition(subMenuStartColumn + 2, currentLine + 1);
                    Console.Write(cost);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        public void PrintSubMenuEnemies()
        {
            const int panelWidth = 100;
            const int gap = 1;
            int subMenuStartColumn = panelWidth - (MenuWidth + 2) * 2 - gap;
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

                    ConsoleColor previousColor = Console.ForegroundColor;
                    if (HoveringCostume == costume) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(subMenuStartColumn + 2, currentLine);
                    Console.Write(costumeName);

                    Console.SetCursorPosition(subMenuStartColumn + 2, currentLine + 1);
                    Console.Write(cost);

                    Console.ForegroundColor = previousColor;
                }
            }
        }

        public void PrintTerrain(bool reverse = false)
        {
            if (reverse) PrintTerrainEnemies();
            else PrintTerrainHeroes();
        }

        public void PrintTerrainHeroes()
        {
            const int panelWidth = 100;
            const int enemiesSpLine = 2;
            int enemyBoardStartLine = enemiesSpLine + 2;
            int heroBoardStartLine = enemyBoardStartLine + Rows + 1;
            int heroesSpLine = heroBoardStartLine + Rows + 1;
            int boardWidth = Columns * 3;
            int boardStartColumn = (panelWidth - boardWidth) / 2;

            for (int displayRow = 0; displayRow < Rows; displayRow++)
            {
                int sourceRow = displayRow;

                for (int displayColumn = 0; displayColumn < Columns; displayColumn++)
                {
                    int sourceColumn = displayColumn;
                    EntityInBoard? entityInBoard = EntitiesInBoard.FirstOrDefault(entityInBoard =>
                        entityInBoard.Row == sourceRow && entityInBoard.Col == sourceColumn);
                    bool isHoveringEntity = entityInBoard is not null && HoveringEntity == entityInBoard;
                    bool isInRange = InRange.Any(position =>
                        position.Length >= 2
                        && position[0] == sourceRow
                        && position[1] == sourceColumn);
                    string cell = entityInBoard is not null ? "[o]" : "[ ]";
                    ConsoleColor previousColor = Console.ForegroundColor;

                    if (isInRange) Console.ForegroundColor = ConsoleColor.Red;
                    else if (isHoveringEntity) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(
                        boardStartColumn + displayColumn * 3,
                        heroBoardStartLine + displayRow);
                    Console.Write(cell);

                    Console.ForegroundColor = previousColor;
                }
            }

            PrintCenteredText($"Heroes SP: {CurrentSp}/{MaxSp}", heroesSpLine, panelWidth);
        }

        public void PrintTerrainEnemies()
        {
            const int panelWidth = 100;
            const int enemiesSpLine = 2;
            int enemyBoardStartLine = enemiesSpLine + 2;
            int boardWidth = Columns * 3;
            int boardStartColumn = (panelWidth - boardWidth) / 2;

            PrintCenteredText($"Enemies SP: {CurrentSp}/{MaxSp}", enemiesSpLine, panelWidth);

            for (int displayRow = 0; displayRow < Rows; displayRow++)
            {
                int sourceRow = Rows - 1 - displayRow;

                for (int displayColumn = 0; displayColumn < Columns; displayColumn++)
                {
                    int sourceColumn = displayColumn;
                    EntityInBoard? entityInBoard = EntitiesInBoard.FirstOrDefault(entityInBoard =>
                        entityInBoard.Row == sourceRow && entityInBoard.Col == sourceColumn);
                    bool isHoveringEntity = entityInBoard is not null && HoveringEntity == entityInBoard;
                    bool isInRange = InRange.Any(position =>
                        position.Length >= 2
                        && position[0] == sourceRow
                        && position[1] == sourceColumn);
                    string cell = entityInBoard is not null ? "[o]" : "[ ]";
                    ConsoleColor previousColor = Console.ForegroundColor;

                    if (isInRange) Console.ForegroundColor = ConsoleColor.Red;
                    else if (isHoveringEntity) Console.ForegroundColor = ConsoleColor.Green;

                    Console.SetCursorPosition(
                        boardStartColumn + displayColumn * 3,
                        enemyBoardStartLine + displayRow);
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

        private static string FormatEntityInfo(Entity entity)
        {
            //return $"({entity.Property.ToString()[0]},{entity.Target.ToString()[0]})";
            return $"({entity.Target.ToString()})";
        }

        private static void PrintEntityInfo(Entity entity, int column, int line)
        {
            ConsoleColor previousColor = Console.ForegroundColor;

            Console.ForegroundColor = GetPropertyColor(entity.Property, previousColor);
            Console.SetCursorPosition(column, line);
            Console.Write(FormatEntityInfo(entity));
            Console.ForegroundColor = previousColor;
        }

        private static ConsoleColor GetPropertyColor(
            PropertyEnum property,
            ConsoleColor defaultColor)
        {
            return property switch
            {
                PropertyEnum.Fire => ConsoleColor.Red,
                PropertyEnum.Water => ConsoleColor.Blue,
                PropertyEnum.Air => ConsoleColor.DarkGreen,
                PropertyEnum.Light => ConsoleColor.Yellow,
                PropertyEnum.Dark => ConsoleColor.Magenta,
                PropertyEnum.Neutral => ConsoleColor.White,
                _ => defaultColor
            };
        }

        private static string FitText(string text, int width)
        {
            return text.Length > width
                ? text[..width]
                : text;
        }
        public void PrintInfoCostume()
        {
            if (HoveringCostume is null) return;

            const int startColumn = 22;
            const int titleLine = 20;
            const int contentStartLine = 22;
            const int rangeColumn = 22;
            const int infoEndColumn = 78;
            const int contentLines = 6;

            Costume costume = HoveringCostume;
            string title = FitText(
                $"{costume.Name} - SP: {costume.Cost}",
                56);
            string[] rangeLines = FormatRange(costume.Range);
            int rangeWidth = rangeLines.Length == 0
                ? 0
                : rangeLines.Max(line => line.Length);
            int descriptionColumn = rangeColumn + rangeWidth + 1;
            int descriptionWidth = infoEndColumn - descriptionColumn;
            string[] descriptionLines = WrapText(
                costume.Description,
                descriptionWidth,
                contentLines);

            Console.SetCursorPosition(startColumn, titleLine);
            Console.Write(title);

            for (int i = 0; i < contentLines; i++)
            {
                int currentLine = contentStartLine + i;

                if (i < rangeLines.Length)
                {
                    Console.SetCursorPosition(rangeColumn, currentLine);
                    Console.Write(rangeLines[i]);
                }

                if (i < descriptionLines.Length)
                {
                    Console.SetCursorPosition(descriptionColumn, currentLine);
                    Console.Write(descriptionLines[i]);
                }
            }
        }

        private static string[] FormatRange(int[][] range)
        {
            return range
                .Select(row => string.Concat(row.Select(value => value switch
                {
                    1 => "[x]",
                    2 => "[X]",
                    _ => "[ ]"
                }))).ToArray();
        }

        private static string[] WrapText(string text, int width, int maximumLines)
        {
            if (string.IsNullOrWhiteSpace(text) || width <= 0) return Array.Empty<string>();

            List<string> lines = new();
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder currentLine = new();

            foreach (string word in words)
            {
                if (currentLine.Length == 0) currentLine.Append(word);
                else if (currentLine.Length + 1 + word.Length <= width) currentLine.Append(' ').Append(word);
                else
                {
                    lines.Add(FitText(currentLine.ToString(), width));
                    currentLine.Clear();
                    currentLine.Append(word);
                }

                if (lines.Count == maximumLines) return lines.ToArray();
            }

            if (currentLine.Length > 0 && lines.Count < maximumLines) lines.Add(FitText(currentLine.ToString(), width));

            return lines.ToArray();
        }
        public EntityInBoard? DetectTarget(Board opponentBoard)
        {
            EntityInBoard[] onlyOneLeft = opponentBoard.EntitiesInBoard.Where(e => e.Entity.CurrentHp > 0).ToArray();
            if (onlyOneLeft.Length == 1)
            {
                opponentBoard.InRange = new[] { new[] { onlyOneLeft[0].Row, onlyOneLeft[0].Col } };
                return onlyOneLeft[0];
            }

            EntityInBoard? target = this.SelectedEntity?.Entity.Target switch
            {
                TargetEnum.First => this.DetectTargetFirst(opponentBoard),
                TargetEnum.Next => this.DetectTargetNext(opponentBoard),
                TargetEnum.Last => this.DetectTargetLast(opponentBoard),
                TargetEnum.SecondLast => this.DetectTargetSecondLast(opponentBoard),
                _ => null
            };

            if (target is not null)
            {
                opponentBoard.InRange = new[] { new[] { target.Row, target.Col } };
                return target;
            }

            return null;
        }

        public EntityInBoard? DetectTargetFirst(Board opponentBoard)
        {
            if (this.SelectedEntity is null) return null;

            int mid = this.SelectedEntity.Col;
            int left = this.SelectedEntity.Col - 1;
            int right = this.SelectedEntity.Col + 1;

            EntityInBoard? midTarget = opponentBoard.EntitiesInBoard
                .Where(e => e.Col == mid && e.Entity.CurrentHp > 0)
                .OrderBy(e => e.Row)
                .FirstOrDefault();
            if (midTarget is not null) return midTarget;

            while (left >= 0 || right <= opponentBoard.Columns - 1)
            {
                if (right <= opponentBoard.Columns - 1)
                {
                    EntityInBoard? target = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == right && e.Entity.CurrentHp > 0)
                        .OrderBy(e => e.Row)
                        .FirstOrDefault();
                    if (target is not null) return target;
                }
                if (left >= 0)
                {
                    EntityInBoard? target = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == left && e.Entity.CurrentHp > 0)
                        .OrderBy(e => e.Row)
                        .FirstOrDefault();
                    if (target is not null) return target;
                }
                left--; right++;
            }

            return null;
        }

        public EntityInBoard? DetectTargetLast(Board opponentBoard)
        {
            if (this.SelectedEntity is null) return null;

            int mid = this.SelectedEntity.Col;
            int left = this.SelectedEntity.Col - 1;
            int right = this.SelectedEntity.Col + 1;

            EntityInBoard? midTarget = opponentBoard.EntitiesInBoard
                .Where(e => e.Col == mid && e.Entity.CurrentHp > 0)
                .OrderByDescending(e => e.Row)
                .FirstOrDefault();
            if (midTarget is not null) return midTarget;

            while (left >= 0 || right <= opponentBoard.Columns - 1)
            {
                if (right <= opponentBoard.Columns - 1)
                {
                    EntityInBoard? target = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == right && e.Entity.CurrentHp > 0)
                        .OrderByDescending(e => e.Row)
                        .FirstOrDefault();
                    if (target is not null) return target;
                }
                if (left >= 0)
                {
                    EntityInBoard? target = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == left && e.Entity.CurrentHp > 0)
                        .OrderByDescending(e => e.Row)
                        .FirstOrDefault();
                    if (target is not null) return target;
                }
                left--; right++;
            }

            return null;
        }
        public EntityInBoard? DetectTargetNext(Board opponentBoard)
        {
            if (this.SelectedEntity is null) return null;

            int mid = this.SelectedEntity.Col;
            int left = this.SelectedEntity.Col - 1;
            int right = this.SelectedEntity.Col + 1;

            EntityInBoard[] midTargets = opponentBoard.EntitiesInBoard
                .Where(e => e.Col == mid && e.Entity.CurrentHp > 0)
                .OrderBy(e => e.Row)
                .ToArray();

            if (midTargets.Length > 0)
            {
                return midTargets.Length >= 2
                    ? midTargets[1]
                    : midTargets[0];
            }

            while (left >= 0 || right <= opponentBoard.Columns - 1)
            {
                if (right <= opponentBoard.Columns - 1)
                {
                    EntityInBoard[] targets = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == right && e.Entity.CurrentHp > 0)
                        .OrderBy(e => e.Row)
                        .ToArray();

                    if (targets.Length > 0)
                    {
                        return targets.Length >= 2
                            ? targets[1]
                            : targets[0];
                    }
                }

                if (left >= 0)
                {
                    EntityInBoard[] targets = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == left && e.Entity.CurrentHp > 0)
                        .OrderBy(e => e.Row)
                        .ToArray();

                    if (targets.Length > 0)
                    {
                        return targets.Length >= 2
                            ? targets[1]
                            : targets[0];
                    }
                }

                left--; right++;
            }

            return null;
        }

        public EntityInBoard? DetectTargetSecondLast(Board opponentBoard)
        {
            if (this.SelectedEntity is null) return null;

            int mid = this.SelectedEntity.Col;
            int left = this.SelectedEntity.Col - 1;
            int right = this.SelectedEntity.Col + 1;

            EntityInBoard[] midTargets = opponentBoard.EntitiesInBoard
                .Where(e => e.Col == mid && e.Entity.CurrentHp > 0)
                .OrderByDescending(e => e.Row)
                .ToArray();

            if (midTargets.Length > 0)
            {
                return midTargets.Length >= 2
                    ? midTargets[1]
                    : midTargets[0];
            }

            while (left >= 0 || right <= opponentBoard.Columns - 1)
            {
                if (right <= opponentBoard.Columns - 1)
                {
                    EntityInBoard[] targets = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == right && e.Entity.CurrentHp > 0)
                        .OrderByDescending(e => e.Row)
                        .ToArray();

                    if (targets.Length > 0)
                    {
                        return targets.Length >= 2
                            ? targets[1]
                            : targets[0];
                    }
                }

                if (left >= 0)
                {
                    EntityInBoard[] targets = opponentBoard.EntitiesInBoard
                        .Where(e => e.Col == left && e.Entity.CurrentHp > 0)
                        .OrderByDescending(e => e.Row)
                        .ToArray();

                    if (targets.Length > 0)
                    {
                        return targets.Length >= 2
                            ? targets[1]
                            : targets[0];
                    }
                }

                left--; right++;
            }

            return null;
        }
    }
}
