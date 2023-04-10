using System.Threading.Tasks;

namespace HangMan2
{
    internal class Program
    {
        // List of variables
        static string? userName;
        static string answer = "great";
        static List<string> displayWord = new List<string>();
        static int guesses;
        static int life = 3;
        static int correctGuess;
        static List<char> guessed = new List<char>();


        static void Main(string[] args)
        {
            StartGame();
            PlayGame();
            EndGame();
        }

        private static void StartGame()
        {
            Console.WriteLine("Starting the game...");
            AskForUsersName();
        }

        static void AskForUsersName()
        {

            Console.WriteLine("Input your username:");
            string inputName = Console.ReadLine();

            if (inputName.Length >= 2)
            {
                // The user entered valid name
                userName = inputName;
            }
            else
            {
                // The user entered invalid name
                Console.Clear();
                Console.WriteLine("Username must contain at least 2 characters");
                AskForUsersName();
            }
        }

        private static void PlayGame()
        {
            DisplayMaskedWord(null, true);
        }

        static void DisplayMaskedWord(string guessedLetter, bool isStartGame)
        {
            string[] listOfLetters = answer.Select(x => x.ToString()).ToArray();
            for (int i = 0; i < answer.Length; i++)
            {
                if (isStartGame == true)
                {
                    displayWord.Add("-");
                }
                else
                {
                    if (listOfLetters[i] == guessedLetter && !guessed.Contains(char.Parse(guessedLetter)))
                    {
                        displayWord[i] = guessedLetter;
                        correctGuess++;
                    }

                }
            }
            if (isStartGame == false)
            {
                if (!guessed.Contains(char.Parse(guessedLetter)))
                {
                    guessed.Add(char.Parse(guessedLetter));
                }


                if (correctGuess == 0)
                {
                    life--;
                }

                correctGuess = 0;
            }


            Console.WriteLine(string.Join("", displayWord));
            if (displayWord.Contains("-") && life > 0)
            {
                AskForLetter();
            }


        }
        static void AskForLetter()
        {
            string guessedLetter;
            do
            {
                Console.WriteLine("Enter a letter!");
                Console.WriteLine($"Life: {life}");
                Console.WriteLine($"Guessed letters: {String.Join(", ", guessed)}");
                guessedLetter = Console.ReadLine();
                Console.Clear();

            } while (guessedLetter.Length != 1 && guessed.Contains(char.Parse(guessedLetter)));

            guesses++;
            DisplayMaskedWord(guessedLetter, false);
        }

        private static void EndGame()
        {
            if (life == 0 && displayWord.Contains("-"))
            {
                Console.WriteLine($"You lose...");
                Console.WriteLine($"The correct word was: {answer}");

            }
            else
            {
                Console.WriteLine($"You win...");
                Console.WriteLine($"The correct word was: {answer}");
            }
            Console.WriteLine($"Game over. Thank you for playing {userName}");
            Console.WriteLine($"Total guessed letters: {guesses}");

        }
    }
}