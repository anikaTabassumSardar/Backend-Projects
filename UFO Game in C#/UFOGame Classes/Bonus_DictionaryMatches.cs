using System.Collections.Generic;
using System.Linq;

namespace UFOGame
{
    public class Bonus_DictionaryMatches
    {
        public static List<string> matches = new List<string>();

        /// <summary>
        /// Returns a list of matches of dictionary words against the user's guesses so far.
        /// </summary>
        public static int DictionaryMatches(Dictionary<int, char> charactersDictionary, List<char> dashes)
        {
            if (charactersDictionary.Count == 0)
            {
                return 0;
            }
            var words = System.IO.File.ReadAllLines("DictionaryWords.txt");
            words.Select(word => word.Split('\n'))
                     .ToDictionary(key => key, val => val);

            bool containsAllCharsInOrder = false;

            //If our matches list is updated once from the dictionary, we do not touch the dictionary file again. We use the small matches list to iterate through it to see further matches.
            if (matches.Count == 0)
            {
                foreach(var word in words)
                {
                    if (dashes.Count == word.Length)
                    {
                        for(int i = 0; i < charactersDictionary.Count; i++)
                        {
                            var key = charactersDictionary.Keys.ElementAt(i);
                            if (word.ToUpper().Contains(charactersDictionary[key]) && char.ToUpper(word[key]) == charactersDictionary[key])
                            {
                                containsAllCharsInOrder = true;
                            }
                            else
                            {
                                containsAllCharsInOrder = false;
                                break;
                            }
                        }

                        if (containsAllCharsInOrder)
                        {
                            matches.Add(word.ToUpper());
                        }
                    }
                }
            }
            else
            {
                foreach(var match in matches.ToList())
                {
                    if (dashes.Count == match.Length)
                    {
                        for(int j = 0; j < charactersDictionary.Count; j++)
                        {
                            var key = charactersDictionary.Keys.ElementAt(j);

                            if (match.ToUpper().Contains(charactersDictionary[key]) && match[key] == charactersDictionary[key])
                            {
                                containsAllCharsInOrder = true;
                            }
                            else
                            {
                                containsAllCharsInOrder = false;
                                break;
                            }
                        }

                        if (!containsAllCharsInOrder)
                        {
                            matches.Remove(match);
                        }
                    }
                }
            }
            return matches.Count;
        }

        /// <summary>
        /// Returns a dictionary with key, value pairs of each index position of the chars updated in the dashList
        /// </summary>
        public static Dictionary<int, char> ProduceDictionaryOfChars(List<char> dashList)
        {
            Dictionary<int, char> charactersDictionary = new Dictionary<int, char>();

            for (int i = 0; i < dashList.Count; i++)
            {
                if (dashList[i] != '_')
                {
                    charactersDictionary.Add(i, dashList[i]);
                }
            }
            return charactersDictionary;
        }
    }
}
