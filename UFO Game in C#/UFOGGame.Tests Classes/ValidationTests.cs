using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UFOGame.Tests
{
    [TestClass]
    public class ValidationTests
    {
        #region IsValidInput Method Test Cases
        /// <summary>
        /// Tests if the user input is given when the last frame is reached.
        /// </summary>
        [TestMethod]
        public void IsValidInput_ReachedLastUFOFrames_GameLost()
        {
            var result = Validation.IsValidInput("A", Program.correctGuessList, Program.incorrectGuessList, 6, Program.framesList, Program.dashList);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if the user input is an unacceptable character or a chain of strings.
        /// </summary>
        [TestMethod]
        public void IsValidInput_InvalidUserInput_ReturnsFalse()
        {
            var result = Validation.IsValidInput("", Program.correctGuessList, Program.incorrectGuessList, 3, Program.framesList, Program.dashList);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests if the user input is a repeat of correct guesses made so far.
        /// </summary>
        [TestMethod]
        public void IsValidInput_RepeatedCorrectGuess_ReturnsFalse()
        {
            Program.correctGuessList.Add('A');
            var result = Validation.IsValidInput("A", Program.correctGuessList, Program.incorrectGuessList, 3, Program.framesList, Program.dashList);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests if the user input is a repeat of incorrect guesses made so far.
        /// </summary>
        [TestMethod]
        public void IsValidInput_RepeatedIncorrectGuess_ReturnsFalse()
        {
            Program.incorrectGuessList.Add('A');
            var result = Validation.IsValidInput("A", Program.correctGuessList, Program.incorrectGuessList, 3, Program.framesList, Program.dashList);
            Assert.IsFalse(result);
        }
        #endregion
    }
}
