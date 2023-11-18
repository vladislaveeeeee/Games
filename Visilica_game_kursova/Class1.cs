namespace Visilica_game_kursova
{
    public class Hangman
    {
        static Random random = new Random();
        public static void Main()
        {
            List<string> words = new List<string> { "dog", "cat", "fish", "lion", "tiger", "elephant", "giraffe", "bear", "pig", "hippo" };
            string random_word = words[random.Next(words.Count)];
            int attemps = 7;
            List<char> letters_used = new List<char>();
            List<char> correct_letters = new List<char>();
            char[] ruska = new char[random_word.Length];
            bool word_guessed = false;
            int correct_guesses = 0;
            string input;
            int count = 0;

            for (int i = 0; i < random_word.Length; i++)
            {
                ruska[i] = '_';
                Console.Write(" ");
                Console.Write(ruska[i]);
            }

            foreach (char letter in random_word)
            {
                correct_letters.Add(letter);
            }

            while (!word_guessed)
            {
                Console.WriteLine("\nEnter a letter : ");
                input = Console.ReadLine();

                if (input.Length != 1)
                {
                    Console.WriteLine("Please enter only one letter. Try again.");
                    continue;
                }

                char letter = char.ToLower(input[0]);

                Console.Clear();

                bool letter_already_used = false;

                for (int i = 0; i < letters_used.Count; i++)
                {
                    if (letters_used[i] == letter)
                    {
                        Console.Write("You have already used this letter. Try again.\n");
                        letter_already_used = true;
                        break;
                    }
                }

                if (!letter_already_used)
                {
                    letters_used.Add(letter);
                    
                }

                bool letter_found = false;

                for (int i = 0; i < random_word.Length; i++)
                {
                    if (letter == random_word[i] && letters_used.Contains(letter))
                    {
                        Console.Clear();
                        Console.Write("The letters you have already used: ");
                        foreach (char usedLetter in letters_used)
                        {
                            Console.Write(usedLetter + " ");
                        }
                        Console.WriteLine("\nNumber of mistakes " + count + "/" + attemps);
                        Visilica(count);
                        Console.WriteLine("\n\n");

                        ruska[i] = letter;
                        letter_found = true;
                        for (int j = 0; j < correct_letters.Count; j++)
                        {
                            if (letter == correct_letters[j])
                            {
                                correct_letters.RemoveAt(j);
                                correct_guesses++;
                                j--; 
                            }
                        }
                    }
                }

                if (!letter_found)
                {
                    Console.Write("The letters you have already used: ");
                    for (int i = 0; i < letters_used.Count; i++)
                    {
                        Console.Write(letters_used[i]);
                        Console.Write(" ");
                    }
                    Console.Write("\nThe letter " + letter + " is not in the word.");

                    if (!letter_already_used)
                    {
                        count++;
                    }
                    Console.Write("Number of mistakes " + count + "/" + attemps);
                    Visilica(count);
                    Console.Write("\n\n");
                    if (count == 7)
                    {
                        Console.Write("Correct word: " + random_word);
                        break;
                    }
                }

                for (int i = 0; i < random_word.Length; i++)
                {
                    Console.Write(ruska[i]);
                }

                Console.Write("\n");

                if (correct_guesses == random_word.Length) 
                {
                    word_guessed = true;
                    for (int i = 0; i < 10; i++)
                    {
                        if (i % 2 != 0) happy_person();
                        else happy_person1();
                        Console.Write("Correct word: " + random_word + "\nYou have guessed the word! \nCongratulations!\n");
                        Thread.Sleep(120);
                    }
                }



            }

        }

        static void step1()
        {
            Console.WriteLine("\n\n\n\n/ \\");
        }
        static void step2()
        {
            Console.WriteLine("\n |\n |\n |\n/ \\");
        }
        static void step3()
        {
            Console.WriteLine("\n _____\n |\n |\n |\n/ \\");
        }
        static void step4()
        {
            Console.WriteLine("\n  _____\n |    |\n |\n |\n/ \\");
        }
        static void step5()
        {
            Console.WriteLine("\n  _____\n |    |\n |    O\n |\n/ \\");
        }
        static void step6()
        {
            Console.WriteLine("\n  _____\n |    |\n |    O\n |   /|\\\n/ \\");
        }
        static void step7()
        {
            Console.WriteLine("\n  _____\n |    |\n |    O\n |   /|\\\n/ \\  / \\ \n\nThe end. You lose");
        }

        static void Visilica(int count)
        {
            if (count == 1) step1();
            if (count == 2) step2();
            if (count == 3) step3();
            if (count == 4) step4();
            if (count == 5) step5();
            if (count == 6) step6();
            if (count == 7) step7();
        }

        static void happy_person()
        {
            Console.Clear();
            Console.WriteLine(" O");
            Console.WriteLine("/|\\");
            Console.WriteLine("/ \\");
        }

        static void happy_person1()
        {
            Console.Clear();
            Console.WriteLine("\\O/");
            Console.WriteLine(" |");
            Console.WriteLine("/ \\");
        }
    }
}