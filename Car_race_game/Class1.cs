namespace Car_race_game
{
    public class Car
    {

        static int Winner(int[] pos)
        {
            for (int k = 0; k < pos.Length; k++)
            {
                if (pos[k] > 80)
                {
                    return k;
                }
            }
            return -1;
        }

        static void PrintCar(int color, int pos)
        {
            switch (color)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            for (int i = 0; i < pos; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("    _______");
            for (int i = 0; i < pos; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" __/___|___\\____");
            for (int i = 0; i < pos; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("|__\\___|___/____\\");
            for (int i = 0; i < pos; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("  @           @");
            Console.ResetColor();
        }

        public static void Main()
        {
            Random random = new Random();
            int amount = 3;
            int[] pos = new int[amount];
            int car;

            do
            {
                Console.Write("Choose one winner car number from 1 to " + amount + ": ");
                car = int.Parse(Console.ReadLine());
            } while (car < 1 || car > amount);

            while (Winner(pos) < 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    pos[i] += random.Next(1, 4); 
                    PrintCar(i + 1, pos[i]);
                }
                Thread.Sleep(200);
                Console.Clear();
            }

            if (Winner(pos) + 1 == car)
            {
                Console.WriteLine("You win, congratulations!!!");
            }
            else
            {
                Console.WriteLine("You lose, don't panic :)");
            }
        }
    }
}