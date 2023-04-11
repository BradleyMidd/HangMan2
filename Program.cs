using System.Threading.Tasks;
using System.IO;

namespace HangMan2
{
    internal class Program
    {
        // List of variables
        static Player player;
        static string answer;
        static List<string> displayWord = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                StartGame();
                PlayGame();
                EndGame();

            }
            catch
            {

                Console.WriteLine("Something went wrong please try again");
            }
        }

        private static void StartGame()
        {

            string[] words;
            try
            {
                words = File.ReadAllLines(@"C:\Users\bradl\Desktop\MyText.txt");
            }
            catch
            {
                words = new string[] { "Apple", "Banana", "Pear" };
            }

            Random rnd = new Random();
            answer = words[rnd.Next(words.Length)];
            

            for (int i = 0; i < answer.Length; i++)
            {
                displayWord.Add("-");
            }

            AskForUsersName();
        }

        static void AskForUsersName()
        {

            Console.WriteLine("Input your username:");
            string inputName = Console.ReadLine();

            // The user entered valid name
            player = new Player(inputName);

        }

        private static void PlayGame()
        {
            DisplayMaskedWord(null);
            AskForLetter();
        }


        static void DisplayMaskedWord(string guessedLetter)
        {

            Console.WriteLine(string.Join("", displayWord));

        }
        static void AskForLetter()
        {
            string guessedLetter;
            do
            {
                Console.WriteLine("Enter a letter!");
                Console.WriteLine($"Life: {player.Life}");
                Console.WriteLine($"Guessed letters: {String.Join(", ", player.Guessed)}");
                guessedLetter = Console.ReadLine();
                Console.Clear();

            } while (guessedLetter.Length != 1);

            if (!player.Guessed.Contains(char.Parse(guessedLetter)))
            {
                player.Guessed.Add(char.Parse(guessedLetter));
                player.guesses++;
                CheckLetter(guessedLetter);
            }
            else 
            {
                Console.WriteLine("That letter is already guessed");
                PlayGame();
            }

        }
        static void CheckLetter(string guessedLetter)
        {
            
            string[] listOfLetters = answer.Select(x => x.ToString()).ToArray();
            for (int i = 0; i < answer.Length; i++)
            {
                if (listOfLetters[i].ToLower() == guessedLetter.ToLower())
                {
                    displayWord[i] = listOfLetters[i];
                    player.CorrectGuess = true;

                }

            }


            if (!player.CorrectGuess)
            {
                player.Life--;
            }
            else 
            {
                player.Score++;
            }

            player.CorrectGuess = false;

            if (displayWord.Contains("-") && player.Life > 0)
            {
                PlayGame();
            }
        }


        private static void EndGame()
        {
            Console.Clear();
            if (player.Life == 0 && displayWord.Contains("-"))
            {
                Console.WriteLine($"You lose...");
                Console.WriteLine($"The correct word was: {answer}");

            }
            else
            {
                Console.WriteLine($"You win...");
                Console.WriteLine($"The correct word was: {answer}");

            }
            Console.WriteLine($"Game over. Thank you for playing {player.Name}");
            Console.WriteLine($"Total correct guessed letters: {player.Score}");
            Console.WriteLine($"Total guessed letters: {player.guesses}");


        }
    }
}