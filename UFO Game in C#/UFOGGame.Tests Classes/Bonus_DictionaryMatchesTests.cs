using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UFOGame.Tests
{
    [TestClass]
    public class Bonus_DictionaryMatchesTests
    {
        /// <summary>
        /// Testing to see if the correct number of elements are added to the list
        /// </summary>
        [TestMethod]
        public void ProduceDictionaryOfChars_ShouldAddOnlyCharsToList()
        {
            Program.dashList = new List<char>() { '_', 'A', 'I', 'I' };
            var result = Bonus_DictionaryMatches.ProduceDictionaryOfChars(Program.dashList);
            Assert.AreEqual(result.Count, 3);  
        }

        /// <summary>
        /// Passing an empty dashlist to test how the method handles it.
        /// </summary>
        [TestMethod]
        public void ProduceDictionaryOfChars_NullListProvided_ShouldNotThrowErrors()
        {
            Program.dashList = new List<char>() { };
            var result = Bonus_DictionaryMatches.ProduceDictionaryOfChars(Program.dashList);
            Assert.AreEqual(result.Count, 0);
        }

        /// <summary>
        /// Passing an empty dashList to test how the method handles it without throwing error.
        /// </summary>
        [TestMethod]
        public void DictionaryMatches_EmptyDashes_ShouldNotThrowError()
        {
            Program.dashList = new List<char>() { };
            var dictionary = Bonus_DictionaryMatches.ProduceDictionaryOfChars(Program.dashList);
            var result = Bonus_DictionaryMatches.DictionaryMatches(dictionary, Program.dashList);
            Assert.AreEqual(result, 0);
        }

        /// <summary>
        /// Testing to see if the result is properly calculated as per the dictionary matches
        /// </summary>
        [TestMethod]
        public void DictionaryMatches_MatchListCleared_ShouldNotThrowError()
        {
            Bonus_DictionaryMatches.matches.Clear();
            Program.dashList = new List<char>() {'A','C','C' , '_' , '_' , '_' , '_' , '_' , '_' , '_' };
            var dictionary = Bonus_DictionaryMatches.ProduceDictionaryOfChars(Program.dashList);
            var result = Bonus_DictionaryMatches.DictionaryMatches(dictionary, Program.dashList);
            Assert.AreEqual(result, 2);
        }
    }
}
