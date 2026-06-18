using SimpleRPG.Core.Entities;

namespace SimpleRPG.Core.Fight
{
    internal class Battle
    {
        private const int PanelWidth = 100;
        private const int PanelHeight = 26;

        public ConsoleKey Key;
        public string selectedMenu = "DEFAULT";
        public Board HeroesBoard { get; set; }
        public Board EnemiesBoard { get; set; }
        public int Turn { get; set; } = 1;
        public string? Winner { get; set; }

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
                Print();
                while (isRunning)
                {
                    this.Key = Console.ReadKey(intercept: true).Key;
                    ProcessKey();
                    Print();
                }
            }
            finally
            {
                Console.CursorVisible = true;
                Console.WriteLine();
            }
        }

        public void Print()
        {
            this.PrintEdges();
            this.PrintInfo();

            this.HeroesBoard.PrintMenu();
            this.HeroesBoard.PrintTerrain();
            this.EnemiesBoard.PrintMenu(reverse: true);
            this.EnemiesBoard.PrintTerrain(reverse: true);

            if (this.HeroesBoard.HoveringEntity is not null)
                this.HeroesBoard.PrintSubMenu();

            if (this.EnemiesBoard.HoveringEntity is not null)
                this.EnemiesBoard.PrintSubMenu(reverse: true);

            if (this.HeroesBoard.HoveringCostume is not null)
                this.HeroesBoard.PrintInfoCostume();
            else if (this.EnemiesBoard.HoveringCostume is not null)
                this.EnemiesBoard.PrintInfoCostume();
            else
                this.PrintInfoDefault();
        }

        public void ProcessKey()
        {
            if (this.selectedMenu == "DEFAULT")
            {
                if (Key == ConsoleKey.D1 || Key == ConsoleKey.NumPad1)
                {
                    this.selectedMenu = "HEROES_MENU";
                    this.EnemiesBoard.HoveringEntity = null;
                    this.HeroesBoard.HoveringEntity = this.HeroesBoard.EntitiesInBoard.FirstOrDefault();
                }
                if (Key == ConsoleKey.D2 || Key == ConsoleKey.NumPad2)
                {
                    this.selectedMenu = "ENEMIES_MENU";
                    this.HeroesBoard.HoveringEntity = null;
                    this.EnemiesBoard.HoveringEntity = this.EnemiesBoard.EntitiesInBoard.FirstOrDefault();
                }
            }

            if (this.selectedMenu == "HEROES_MENU")
            {
                if (Key == ConsoleKey.UpArrow) MoveHoveringEntity(this.HeroesBoard, -1);
                if (Key == ConsoleKey.DownArrow) MoveHoveringEntity(this.HeroesBoard, 1);
                if (Key == ConsoleKey.Enter)
                {
                    this.selectedMenu = "HEROES_SUBMENU";
                    this.HeroesBoard.SelectedEntity = this.HeroesBoard.HoveringEntity;
                    this.HeroesBoard.HoveringCostume =
                        this.HeroesBoard.SelectedEntity?.Entity.Costumes.FirstOrDefault();
                    UpdateHeroesCostumeRange();
                }
                if (Key == ConsoleKey.Backspace) ResetMenus();
            }

            if (this.selectedMenu == "HEROES_SUBMENU")
            {
                if (Key == ConsoleKey.UpArrow)
                {
                    MoveHoveringCostume(this.HeroesBoard, -1);
                    UpdateHeroesCostumeRange();
                }
                if (Key == ConsoleKey.DownArrow)
                {
                    MoveHoveringCostume(this.HeroesBoard, 1);
                    UpdateHeroesCostumeRange();
                }
                if (Key == ConsoleKey.Enter) { }
                if (Key == ConsoleKey.Backspace)
                {
                    this.HeroesBoard.HoveringCostume = null;
                    this.EnemiesBoard.InRange = Array.Empty<int[]>();
                    this.selectedMenu = "HEROES_MENU";
                }
            }
            if (this.selectedMenu == "HEROES_POSITION") { }
            if (this.selectedMenu == "HEROES_ORDER") { }

            if (this.selectedMenu == "ENEMIES_MENU")
            {
                if (Key == ConsoleKey.UpArrow) MoveHoveringEntity(this.EnemiesBoard, -1);
                if (Key == ConsoleKey.DownArrow) MoveHoveringEntity(this.EnemiesBoard, 1);
                if (Key == ConsoleKey.Enter)
                {
                    this.selectedMenu = "ENEMIES_SUBMENU";
                    this.EnemiesBoard.HoveringCostume =
                        this.EnemiesBoard.HoveringEntity?.Entity.Costumes.FirstOrDefault();
                }
                if (Key == ConsoleKey.Backspace) ResetMenus();
            }

            if (this.selectedMenu == "ENEMIES_SUBMENU")
            {
                if (Key == ConsoleKey.UpArrow) MoveHoveringCostume(this.EnemiesBoard, -1);
                if (Key == ConsoleKey.DownArrow) MoveHoveringCostume(this.EnemiesBoard, 1);
                if (Key == ConsoleKey.Backspace)
                {
                    this.EnemiesBoard.HoveringCostume = null;
                    this.selectedMenu = "ENEMIES_MENU";
                }
            }
            //if (this.selectedMenu == "ENEMIES_POSITION") { }
            //if (this.selectedMenu == "ENEMIES_ORDER") { }
        }

        private static void MoveHoveringEntity(Board board, int direction)
        {
            if (board.EntitiesInBoard.Length == 0) return;

            int currentIndex = Array.IndexOf(board.EntitiesInBoard, board.HoveringEntity);
            int nextIndex = (currentIndex + direction + board.EntitiesInBoard.Length) % board.EntitiesInBoard.Length;
            board.HoveringEntity = board.EntitiesInBoard[nextIndex];
        }

        private static void MoveHoveringCostume(Board board, int direction)
        {
            Costume[] costumes = board.HoveringEntity?.Entity.Costumes
                ?? Array.Empty<Costume>();
            if (costumes.Length == 0) return;

            int currentIndex = Array.IndexOf(costumes, board.HoveringCostume);
            int nextIndex = (currentIndex + direction + costumes.Length) % costumes.Length;
            board.HoveringCostume = costumes[nextIndex];
        }

        private void ResetMenus()
        {
            this.selectedMenu = "DEFAULT";
            this.HeroesBoard.HoveringEntity = null;
            this.EnemiesBoard.HoveringEntity = null;
            this.HeroesBoard.HoveringCostume = null;
            this.EnemiesBoard.HoveringCostume = null;
            this.HeroesBoard.SelectedEntity = null;
            this.EnemiesBoard.SelectedEntity = null;
            this.HeroesBoard.InRange = Array.Empty<int[]>();
            this.EnemiesBoard.InRange = Array.Empty<int[]>();
        }

        private void UpdateHeroesCostumeRange()
        {
            EntityInBoard? target = this.HeroesBoard.DetectTarget(this.EnemiesBoard);
            Costume? costume = this.HeroesBoard.HoveringCostume;

            if (target is null || costume is null)
            {
                this.EnemiesBoard.InRange = Array.Empty<int[]>();
                return;
            }

            this.EnemiesBoard.InRange = this.HeroesBoard.CostumeRangeInBoard(
                new[] { target.Row, target.Col },
                costume,
                this.EnemiesBoard);
        }
        public void PrintEdges()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string("-").PadLeft(PanelWidth, '-'));
            for (int i = 1; i <= PanelHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|" + new string(' ', PanelWidth - 2) + "|");
            }
            Console.SetCursorPosition(0, PanelHeight + 1);
            Console.Write(new string("-").PadLeft(PanelWidth, '-'));
        }
        public void PrintInfo()
        {
            Console.SetCursorPosition(20, PanelHeight - 7);
            Console.Write(new string("-").PadLeft(PanelWidth - 40, '-'));
            for (int i = 1; i <= 7; i++)
            {
                Console.SetCursorPosition(20, (PanelHeight - 7) + i);
                Console.Write(new string("|").PadRight(PanelWidth - 41, ' ') + "|");
            }
        }

        public void PrintInfoDefault()
        {
            Console.SetCursorPosition(22, PanelHeight - 6);
            Console.Write("[1] Heroes menu | [2] Enemies menu | [3] Leave");
        }
    }
}
