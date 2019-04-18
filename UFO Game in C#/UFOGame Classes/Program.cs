using System;
using System.Collections.Generic;
using System.Linq;

namespace UFOGame
{
    public class Program
    {
        #region Global Variables
        public static List<string> framesList = UFOFramesGenerator.UfoFrames();    // List of UFO frames
        public static int currentFramesIndex = 0;  // currentIndex of the UFO frames
        public static List<char> dashList = new List<char>();  // list of dashes for the dictionary word
        public static List<char> incorrectGuessList = new List<char>(); // list of incorrect guesses so far
        public static List<char> correctGuessList = new List<char>(); // list of correct guesses so far
        public static string userInput;
        public static bool isValid;    // bool flag to check if the user input is valid
        public static string dictionaryWord; // word randomly picked from the given dictionary
        public static int trackWin;
        public static int trackLoss;
        public static string[] tracking = { trackWin.ToString(), trackLoss.ToString() };
        public static bool resetIsCalled = false;
        #endregion

        /*How many people were abducted and how many were saved?*/

        static void Main(string[] args)
        {
            trackWin = 0;
            trackLoss = 0;

            Introduction();

            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Environment.Exit(0);
            }

            StartGame();
        }

        #region Begin The Game
        /// <summary>
        /// Prints indroduction as well as directions for the game.
        /// </summary>
        public static void Introduction()
        {
            var track = System.IO.File.ReadAllLines("WinOrLoss.txt");
            track.Select(word => word.Split('\n'))
                     .ToList();
            trackWin = Convert.ToInt32(track[0]);
            trackLoss = Convert.ToInt32(track[1]);
            Console.WriteLine("UFO: The Game" + "\n" + "Coded by: Anika Neela" + "\n" + framesList.ElementAt(0));
            Console.WriteLine("Invaders from outer space have arrived and are abducting humans using tractor beams. Earn your medal of honor by cracking the codeword to stop the abduction!\n");
            Console.WriteLine($"How to play: \nGuess one letter at a time of a codeword represented by blank placeholders for each letter.\nIf the letter does not exist in the codeword, the person is pulled in closer to the UFO by the tractor beam.\nIf the letter exists, the blanks that correspond to the position of those letters in the codeword are replaced by the letter.\nIf all the letters of the codeword are revealed before the person is pulled into the UFO, you win.Otherwise, the UFO abducts the person and you lose.\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To Start the game, hit ENTER. Press any other key to exit.");
        }

        /// <summary>
        /// Retrieves a word randomly from the given dictionary file for the game.
        /// </summary>
        /// <returns></returns>
        public static string RandomWordsFromDictionary()
        {
            var words = System.IO.File.ReadAllLines("DictionaryWords.txt");
            words.Select(word => word.Split('\n'))
                 .ToDictionary(key => key, val => val);

            Random rnd = new Random();
            string randomWord = words.ElementAt(rnd.Next(0, words.Length));

            return randomWord.ToUpper();
        }

        /// <summary>
        /// Starts the game and sets up the format to keep score of correct and incorrect guesses.
        /// </summary>
        public static void StartGame()
        {
            ResetValues();
            Console.WriteLine();
            GetUserInput();

            while (dashList.Contains('_') && currentFramesIndex != framesList.Count - 1)
            {
                CheckTheGuess(userInput);
                GetUserInput();
            }
        }

        /// <summary>
        /// Resets the values of all variables as well as clears the console for a new game.
        /// </summary>
        public static void ResetValues()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            correctGuessList.Clear();
            incorrectGuessList.Clear();
            dashList.Clear();
            currentFramesIndex = 0;
            Bonus_DictionaryMatches.matches.Clear();
            dictionaryWord = RandomWordsFromDictionary();
            Console.WriteLine($"GAME STARTED!\n UFO: The Game \n Instructions: Save us from alien abduction by guessing letters in the codeword.");
            Console.WriteLine(framesList.ElementAt(0));

            Console.ForegroundColor = ConsoleColor.Red;
            IncorrectGuessesStatus(char.MinValue, true);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Codeword:");

            foreach (char c in dictionaryWord)
            {
                dashList.Add('_');
                Console.Write("_ ");
            }
        }
        #endregion

        #region User Guesses
        /// <summary>
        /// Prompts user to enter their guess/input and validates it.
        /// </summary>
        public static void GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nPlease enter your guess: ");

            isValid = Validation.IsValidInput(userInput = Console.ReadLine().ToUpper(), correctGuessList, incorrectGuessList, currentFramesIndex, framesList, dashList);

            while (!isValid)
            {
                GetUserInput();
            }
        }

        /// <summary>
        /// Evaluates if the user guess is correct or not. Then it calls the respective methods for each scenarios.
        /// </summary>
        public static void CheckTheGuess(string input)
        {
            List<int> indices = TrackTheIndicesOfTheChar(input[0]);
            dashList = UpdateTheDash(indices, input[0]);

            if (dictionaryWord.Contains(input[0]))
            {
                correctGuessList.Add(input[0]);
                CorrectGuessDisplay();
                IncorrectGuessesStatus(input[0], true);
            }
            else
            {
                incorrectGuessList.Add(input[0]);
                InCorrectGuessDisplay();
                IncorrectGuessesStatus(input[0]);
            }

            PrintTheUpdatedDashes();
        }

        /// <summary>
        /// Prints and updates the incorrect guesses.
        /// </summary>
        /// <param name="firstRunOrCorrectGuess">Checks if it is the first round of the game or if it is a correct guess.</param>
        public static void IncorrectGuessesStatus(char input, bool firstRunOrCorrectGuess = false)
        {
            string displayAllIncorrectGuesses = "";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect Guesses:");

            if (firstRunOrCorrectGuess && incorrectGuessList.Count() == 0)
            {
                Console.WriteLine("None");
            }

            foreach (char guess in incorrectGuessList)
            {
                displayAllIncorrectGuesses += guess + " ";
            }

            Console.WriteLine(displayAllIncorrectGuesses);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints the correct result dialog as well as keep the UFO frames consistent for the user in the console.
        /// </summary>
        /// <param name="currUFOIndex">Index of the current index of the UFO frames to ensure that the frames remain the same in terms of correct guess.</param>
        public static void CorrectGuessDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (dashList.Count(x => x.ToString().Contains('_')) == 0 && currentFramesIndex != framesList.Count - 1)
            {
                GameWon();
            }
            else
            {
                Console.WriteLine("\nCorrect! You're closer to cracking the codeword.");
                Console.WriteLine(framesList.ElementAt(currentFramesIndex));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Prints the incorrect result dialog as well as updates the UFO frames for the user in the console.
        /// </summary>
        public static void InCorrectGuessDisplay()
        {
            if (dashList.Contains('_') && currentFramesIndex == (framesList.Count - 2))
            {
                GameLost();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nIncorrect! The tractor beam pulls the person in further.");
            Console.WriteLine(framesList.ElementAt(++currentFramesIndex));
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion

        #region Dashes
        /// <summary>
        /// Returns the list of indices where the correct char is present in the word for the game.
        /// </summary>
        /// <returns>A list of indices of the characters in the correct word.</returns>
        public static List<int> TrackTheIndicesOfTheChar(char input)
        {
            List<int> indicesOfCharsPresentList = new List<int>();

            for (int i = 0; i < dictionaryWord.Length; i++)
            {
                if (input == dictionaryWord[i])
                {
                    indicesOfCharsPresentList.Add(i);
                }
            }
            return indicesOfCharsPresentList;
        }

        /// <summary>
        /// Returns a list of chars with updated dashes and values when necessary.
        /// </summary>
        /// <param name="indices">List of indices that help to locate where the correct guessed char should be placed.</param>
        /// <param name="firstRun">Bool flag to check if it is the first run of the game. If yes, then print just the dashes. If not, update the dashes with chars from the correct guesses.</param>
        /// <returns>Updated list of dashes with the proper values.</returns>
        public static List<char> UpdateTheDash(List<int> indicesList, char input, bool firstRun = false)
        {
            if (!firstRun)
            {
                foreach (int index in indicesList)
                {
                    dashList[index] = input;
                }
            }

            if (dashList.Count(x => x.ToString().Contains('_')) == 0 && currentFramesIndex != framesList.Count - 1)
            {
                GameWon();
            }
            return dashList;
        }

        /// <summary>
        /// Prints the list of dashes/values.
        /// </summary>
        public static void PrintTheUpdatedDashes()
        {
            Console.WriteLine();
            Console.WriteLine("Codeword:");

            foreach (char dash in dashList)
            {
                Console.Write($"{dash} ");
            }

            Console.WriteLine();
            PrintMatchesFromDictionary();
            TrackWinORLoss();
        }
        #endregion

        #region Game Status
        /// <summary>
        /// Prints when the game is won successfully (aka. when the user wins).
        /// </summary>
        public static void GameWon()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCorrect! You saved the person and earned a medal of honor!");
            Console.WriteLine($"The codeword is: {dictionaryWord}.");
            trackWin++;
            PlayAgain();
        }

        /// <summary>
        /// Prints when the game is lost.
        /// </summary>
        public static void GameLost()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nIncorrect! Seems like the alien abducted you.");
            Console.WriteLine($"The codeword is: {dictionaryWord}.");
            Console.WriteLine(framesList.ElementAt(++currentFramesIndex));
            trackLoss++;
            PlayAgain();
        }

        /// <summary>
        /// Prompts the user with options to play the game again.
        /// </summary>
        public static void PlayAgain()
        {
            Console.Write("\nWould you like to play again (Y/N)?");
            userInput = Console.ReadLine().ToUpper();

            if (userInput == "N")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nGoodbye!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (userInput == "Y")
            {   
                StartGame();

            }
            else
            {
                Console.WriteLine("I cannot understand your input. Please write either Y or N.");
                PlayAgain();
            }
        }
        #endregion

        public static void TrackWinORLoss()
        {
            tracking[0] = trackWin.ToString();
            tracking[1] = trackLoss.ToString();
          
            System.IO.File.WriteAllLines("WinOrLoss.txt", tracking);
            var track = System.IO.File.ReadAllLines("WinOrLoss.txt");
            track.Select(word => word.Split('\n'))
                     .ToList();
            Console.WriteLine($"You have saved {track[0]} people and sadly, {track[1]} people have been abducted.");
        }

        public static void PrintMatchesFromDictionary()
        {
            var charactersDictionary = Bonus_DictionaryMatches.ProduceDictionaryOfChars(dashList);
            Console.WriteLine();
            var matches = Bonus_DictionaryMatches.DictionaryMatches(charactersDictionary, dashList);
            Console.Write("Number of dictionary matches: ");
            Console.WriteLine(matches);
        }
    }
}
