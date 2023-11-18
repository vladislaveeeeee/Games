using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Visilica_game_kursova;
using Car_race_game;
using Pacman_game;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

class Games
{
    class SignUp
    {
        public string login { get; set; }
        public string password { get; set; }

        public SignUp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Create account!");
        }

        public void Register()
        {
            Console.WriteLine("Create login: ");
            login = Console.ReadLine();

            string password;
            do
            {
                Console.WriteLine("Create password: ");
                password = Console.ReadLine();

                if (!IsPasswordValid(password))
                {
                    Console.WriteLine("Invalid password format. Please try again.");
                }

            } while (!IsPasswordValid(password));

            using (StreamWriter file = new StreamWriter("users.txt", true))
            {
                file.WriteLine($"{login} {password}");
            }

            Console.WriteLine("Registration successful. Press any key to continue...");
            Console.ReadKey();
        }

        private bool IsPasswordValid(string password)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(password);
        }
    }

    class SignIn : SignUp
    {

        public SignIn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Login in account");
        }

        public new void Register()
        {
            bool check = false;
            do
            {

                Console.WriteLine("Enter login: ");
                login = Console.ReadLine();

                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
                using (StreamReader file = new StreamReader("users.txt"))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] userData = line.Split(' ');
                        if (userData[0] == login && userData[1] == password)
                        {
                            Console.WriteLine("Sign-in successful. Press any key to continue...");
                            Console.ReadKey();
                            check = true;
                            return;
                        }
                    }
                }
            }
            while (!check);
            {
                Console.WriteLine("Error: Invalid login or password. Press any key to continue...");
                Console.ReadKey();
            }
        }

    };

    static int GetUserChoice()
    {
        int choice;
        while (true)
        {
            Console.Write("Ваш вибір: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice >= 1 && choice <= 5)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Будь ласка, виберіть одну з вказаних цифр!");
                }
            }
            else
            {
                Console.WriteLine("Невірний вибір. Будь ласка, виберіть одну з вказаних цифр!");
            }
        }
    }

    static void Main()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string log, pass;
        int choice;
        bool isSignSlected = false;
        bool isSignUpSelected = true;
        bool isAlreadySignedUp = false;
        int count = 0;

        if (File.Exists("UserCredentials.txt"))
        {
            Console.WriteLine("Ви вже зареєстровані. Перезапис користувача заборонено. Повернення в головне меню...");
            Thread.Sleep(3000);
            isAlreadySignedUp = true;
        }

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("=========================================================");
                Console.WriteLine("|       Виберіть гру для початку ігрового процесу       |");
                Console.WriteLine("|=======================================================|");
                Console.WriteLine("| 1. Hangman                                            |");
                Console.WriteLine("| 2. Car race                                           |");
                Console.WriteLine("| 3. Pacman                                             |");
                Console.WriteLine("| 4. Вихід з гри                                        |");
                Console.WriteLine("| 5. Реєстрація (обовязково)                            |");
                Console.WriteLine("========================================================");
                Console.Write("Ваш вибір: ");
                choice = GetUserChoice();

            switch (choice)
                {
                    case 5:
                        isSignUpSelected = true;
                        break;
                    case 1:
                        if (isAlreadySignedUp)
                        {
                            SignIn acc;
                            acc = new SignIn();
                            acc.Register();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Запуск Hangman game...");
                            Thread.Sleep(1000);
                            Console.WriteLine("");
                            Hangman.Main();
                            Console.WriteLine("\nГра Hangman завершена. Натисніть Enter, щоб повернутися на головний екран...");
                            Console.ReadLine();

                        }
                        break;
                    case 2:
                        if (isAlreadySignedUp)
                        {
                            SignIn acc1;
                            acc1 = new SignIn();
                            acc1.Register();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Запуск Car race game...");
                            Thread.Sleep(1000);
                            Car.Main();
                            Console.WriteLine("\nГра Car race завершена. Натисніть Enter, щоб повернутися на головний екран...");
                            Console.ReadLine();
                        }
                        break;
                    case 3:
                        if (isAlreadySignedUp)
                        {
                            SignIn acc2;
                            acc2 = new SignIn();
                            acc2.Register();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Запуск Pacman game...");
                            Thread.Sleep(1000);
                            Pacman.Main();
                            Console.WriteLine("\nГра Pacman завершена. Натисніть Enter, щоб повернутися на головний екран...");
                            Console.ReadLine();
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Завершення гри...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Будь ласка, виберіть від 1 до 5.");
                        break;
                }

                if (isSignUpSelected)
                {
                    Console.Clear();
                    Console.WriteLine("=========================================================");
                    Console.WriteLine("|                         SIGN                          |");
                    Console.WriteLine("=========================================================");
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Exit");
                    Console.Write("Enter your choice (1 or 2): ");

                    int select = GetUserChoice();

                    switch (select)
                    {
                        case 1:
                        if (isAlreadySignedUp)
                        {
                            Console.WriteLine("Ви вже зареєстровані. Перезапис користувача заборонено. Повернення в головне меню...");
                            Thread.Sleep(3000);
                            
                            Console.ReadLine();
                            break;
                        }
                        SignUp signUp = new SignUp();
                            signUp.Register();
                            isSignUpSelected = false;
                            isAlreadySignedUp = true;

                        break;
                        case 2:
                            isSignUpSelected = true;
                            break;
                        default:
                            Console.WriteLine("Невірний вибір. Будь ласка, виберіть 1 або 2.");
                            Console.ReadKey();
                            break;
                    }
                isSignUpSelected = false;

            }

            } while (true);   
    }
}