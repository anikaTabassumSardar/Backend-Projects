using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UFOGame.Tests
{
    [TestClass]
    public class ProgramTests
    {
        #region RandomWordsFromDictionary() Method Test Cases
        /// <summary>
        /// Tests if the word RandomWordsFromDictionary() Method property retrieves the word from the dictionary.
        /// </summary>
        [TestMethod]
        public void RandomWordsFromDictionary_RandomWordRetrieved_ReturnsRandomWord()
        {
            var result = Program.RandomWordsFromDictionary();
            var words = System.IO.File.ReadAllLines("DictionaryWords.txt");
            words.Select(word => word.Split('\n'))
                 .ToDictionary(key => key, val => val);

            Assert.IsTrue(words.Contains(result.ToLower()));
        }

        /// <summary>
        /// Tests if the method returns null.
        /// </summary>
        [TestMethod]
        public void RandomWordsFromDictionary_IsNotNull_ReturnsTrue()
        {
            var result = Program.RandomWordsFromDictionary();
            Assert.IsNotNull(result);
        }
        #endregion

        /// <summary>
        /// Tests if TrackTheIndicesOfTheChar Method returns the list of indices where the correct char is present in the word for the game.
        /// </summary>
        [TestMethod]
        public void TrackTheIndicesOfTheChar_GameStarts()
        {
            Program.dictionaryWord = "FLUFFY";
            var result = Program.TrackTheIndicesOfTheChar('F');
            Assert.AreEqual(result.Count, 3);
        }

        /// <summary>
        /// Tests if the UpdateTheDash() method updates the dashList accordingly with the user input.
        /// </summary>
        [TestMethod]
        public void UpdateTheDash_NotFirstRun_UpdatesTheListWithValues()
        {
            var indicesList = new List<int>() { 1 };
            Program.dictionaryWord = "HOLD";
            char input = 'O';

            foreach (char c in Program.dictionaryWord)
            {
                Program.dashList.Add('_');

            }

            var result = Program.UpdateTheDash(indicesList, input);
            Assert.AreEqual(result[1], 'O');
        }

        /// <summary>
        /// Tests if the method throws any exception.
        /// </summary>
        [TestMethod]
        public void StartGameTest()
        {
            try
            {
                Program.CheckTheGuess("A");
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void IncorrectGuessesStatusTest()
        {
            try
            {
                Program.IncorrectGuessesStatus('A');
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
