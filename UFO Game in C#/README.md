# UFO: The Game
**Author:** <br> Anika Neela

**Game Description:** <br> Invaders from outer space have arrived and are abducting humans using tractor beams. Earn your medal of honor by cracking the codeword to stop the abduction!

**#Prerequisites:**<br>
-Language used: C# <br>
-Software: Microsoft Visual Studio <br>
-Version: 4.6.1 <br>
-Console Application (.NET Framework) <br>
-DictionaryWords.txt <br>

**#Directions to Run the Project in Visual Studio:**<br>
-Unzip the project solution.<br>
-Open the solution Codeacademy_BackendChallenge_Neela in Microsoft Visual Studio and hit'Start' to run the application/game.<br>
-The first screen shows an overview of the game. Hit 'Enter' on keyboard to start the game or any other key to exit the application.<br><br>
-The next console screen shows:<br>
---UFO frames are incremented as the users make incorrect guesses.<br>
---Incorrect Guesses made by users so far. If the users didn't make any guesses or made only correct guesses, it would say 'None' under Incorrect Guesses. If incorrect guesses are made, the guesses are listed below. <br>
---Codeword is followed by dashes that allows users to get an idea of how many letters make up the word for the game. As the users make correct guesses, the dashes are replaced with the correct letters. The codeword is randomly generated from the DictionaryWords.txt file.
---Users are prompted to enter one letter at a time as guess attempts.<br>
---Number of dictionary matches are also provided in the console. The number is constantly updated with the updated correct guesses made by the user.<br>
---If UFO frames reach the last frames' figure in the list, the user is informed that they have lost the game and are asked to PlayAgain() option.<br>
---If the user guesses all the correct letters to replace the dashes before the last frame of UFO is reached, the user is informed that the user has won the game and are again given the chance to PlayAgain().<br>
---If the user presses, Y to PlayAgain() the game, the console is cleared to start another round of game. If the user presses N, the console prints "Goodbye" and can press any other keys after it to exit the console.<br>
---The game is color coded for instantly knowing the game status/progress. If incorrect guesses are provided, the message and frames appear in red. If correct guesses are provided, the message and frames appear in green.<br>
---Once the game is either won or lost, the correct codeword is revealed to the user consistently.<br>

**#Validations:**<br>
---Users are not allowed to repeat their correct guesses NOR their incorrect guesses.<br>
---User-input is sanitized to be consistent by being Uppercase().<br>
---Users are prompted with proper messages if they try to attempt to write anything other than accepted single-letter rule.<br>
---Users are prompted with proper messages if they try to repeat their guesses.<br>

**#Unit Tests and Methods naming conventions**<br>
---The application is properly documented as well as commented throughout. Method names are intended to be self-explanatory, but they are also documented so that if there is any confusion, hovering over the method names will give an explanation of the method's task/goal.<br>
---#region blocks are also used in the classes so that multiple similar-grouped methods can be expanded as well as collapsed.<br>
---Unit tests are in the UFOGame.Tests project and all start with the name of the method that is targetted to be tested. To run the tests, click 'Test'> 'Run All Tests'. If any problem occurs while running tests, right click on the batch of tests and 'Run Selected Tests' the tests again. If Test Explorer window is not open, click 'Test' > 'Windows' > 'Test Explorer'.<br>


