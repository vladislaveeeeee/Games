using static Pacman_game.Pacman;

namespace Pacman_game
{
    public class Pacman
    {
        public class Food
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Food(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class Enemy
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Enemy(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public static void Main()
        {
            Console.CursorVisible = false; // видимість курсора 
            char[,] map = ReadMap();
            DrawMap(map);
            ConsoleKeyInfo pressKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false); // кнопка по замовчуванню

            Task.Run(() =>
            {
                while (true)
                {
                    pressKey = Console.ReadKey();
                }
            });

            int PacmanX = 21;
            int PacmanY = 7;

            int score = 0;
            Food food = GenerateFood(map);
            Enemy enemy = GenerateEnemy(map);
            while (true)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(PacmanX, PacmanY);
                Console.Write("(");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(44, 0);
                Console.Write($"Score :  {score}");

                Console.SetCursorPosition(food.X, food.Y);
                Console.Write(".");

                Console.SetCursorPosition(enemy.X, enemy.Y);
                Console.Write("O");
     

                HandleInput(pressKey, ref PacmanX, ref PacmanY, map, ref score);

                if (PacmanX == food.X && PacmanY == food.Y)
                {
                    score++;
                    food = GenerateFood(map);
                }

                MoveEnemy(map, ref enemy);

                if (CheckCollision(PacmanX, PacmanY, enemy.X, enemy.Y))
                {
                    Console.Clear();
                    Console.Write("GAME OVER");
                    break;
                }

                if (score == 20)
                {
                    Console.Clear();
                    break;

                }

                Thread.Sleep(120);
            }

            Console.Write("GAME OVER");

        }

        public static Food GenerateFood(char[,] map)
        {
            Random random = new Random();
            int x, y;

            do
            {
                x = random.Next(0, map.GetLength(0));
                y = random.Next(0, map.GetLength(1));
            } while (map[x, y] != ' '); // Повторюємо, якщо обране місце не порожнє

            return new Food(x, y);
        }

        public static Enemy GenerateEnemy(char[,] map)
        {
            Random random = new Random();
            int x, y;

            do
            {
                x = random.Next(0, map.GetLength(0));
                y = random.Next(0, map.GetLength(1));
            } while (map[x, y] != ' ');

            return new Enemy(x, y);
        }

        public static void MoveEnemy(char[,] map, ref Enemy enemy)
        {
            Random random = new Random();
            int direction = random.Next(0, 4); // 0 - вверх, 1 - вниз, 2 - вліво, 3 - вправо

            int newX = enemy.X;
            int newY = enemy.Y;

            switch (direction)
            {
                case 0:
                    newY--;
                    break;
                case 1:
                    newY++;
                    break;
                case 2:
                    newX--;
                    break;
                case 3:
                    newX++;
                    break;
            }

            if (newX >= 0 && newX < map.GetLength(0) && newY >= 0 && newY < map.GetLength(1) && map[newX, newY] == ' ')
            {
                enemy.X = newX;
                enemy.Y = newY;
            }
        }

        public static bool CheckCollision(int pacmanX, int pacmanY, int enemyX, int enemyY)
        {
            return pacmanX == enemyX && pacmanY == enemyY;
        }

        public static char[,] ReadMap()
        {
            string[] file = File.ReadAllLines("map.txt");

            int maxLength = GetMaxLengthOfLines(file);
            char[,] map = new char[maxLength, file.Length];

            for (int y = 0; y < file.Length; y++)
            {
                string line = file[y];
                for (int x = 0; x < line.Length; x++)
                {
                    map[x, y] = line[x];
                }
            }

            return map;
        }

        public static void DrawPacman(char[,] map, int PacmanX, int PacmanY, char direction)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (x == PacmanX && y == PacmanY)
                    {
                        // Відображення Пакмена залежно від напрямку руху
                        switch (direction)
                        {
                            case 'l':
                                Console.Write(")");
                                break;
                            case 'r':
                                Console.Write("(");
                                break;
                            case 'u':
                                Console.Write("v");
                                break;
                            case 'd':
                                Console.Write("^");
                                break;
                        }
                    }
                    else
                    {
                        Console.Write(map[x, y]);
                    }
                }
                Console.Write("\n");
            }
        }

        public static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }

        public static void HandleInput(ConsoleKeyInfo pressKey, ref int PacmanX, ref int PacmanY, char[,] map, ref int score)
        {

            int[] direction = Direction(pressKey);
            int nextPositionX = PacmanX + direction[0];
            int nextPositionY = PacmanY + direction[1];
            char nextSymbol = map[nextPositionX, nextPositionY];

            if (nextSymbol == ' ' || nextSymbol == '.')
            {
                PacmanX = nextPositionX;
                PacmanY = nextPositionY;

                if (nextSymbol == '.')
                {
                    score++;
                    map[nextPositionX, nextPositionY] = ' ';
                }
            }

        }

        public static int[] Direction(ConsoleKeyInfo pressKey)
        {
            int[] direction = { 0, 0 };

            if (pressKey.Key == ConsoleKey.UpArrow)
            {

                direction[1] = -1;

            }
            else if (pressKey.Key == ConsoleKey.DownArrow)
            {

                direction[1] = 1;

            }
            else if (pressKey.Key == ConsoleKey.LeftArrow)
            {
                direction[0] = -1;
            }
            else if (pressKey.Key == ConsoleKey.RightArrow)
            {
                direction[0] = 1;
            }
            return direction;
        }

        public static int GetMaxLengthOfLines(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;
                }
            }
            return maxLength;
        }
    }
}