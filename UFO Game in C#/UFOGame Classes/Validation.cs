using System;
using System.Collections.Generic;
using System.Linq;

namespace UFOGame
{
    public class Validation
    {
        #region User Input Validation
        /// <summary>
        /// Validates user-input to check for both validity and repetition.
        /// </summary>
        /// <param name="correctGuessList">List of correct guesses the user has already made.</param>
        /// <param name="incorrectGuessList">List of incorrect guesses the user has already made.</param>
        /// <returns></returns>
        public static bool IsValidInput(string userInput, List<char> correctGuessList, List<char> incorrectGuessList, int currFramesIndex,List<string>frameList, List<char> dashList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            bool isValid = true;

            if (dashList.Contains('_') && currFramesIndex == frameList.Count - 1)
            {
                Program.GameLost();
            }
            else if(userInput == "" || userInput.Length > 1 || !userInput.All(char.IsLetter))
            {
                Console.WriteLine("\nI cannot understand your input. Please guess a single letter.");
                isValid = false;
            }
            else if(correctGuessList.Contains(userInput[0]) || incorrectGuessList.Contains(userInput[0]))
            {
                Console.WriteLine("\nYou can only guess that letter once, please try again.");
                isValid = false;
            }
            return isValid;
        }
        #endregion
    }
}
