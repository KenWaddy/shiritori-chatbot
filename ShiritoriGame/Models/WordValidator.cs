using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShiritoriGame.Models
{
    public class WordValidator
    {
        private static readonly Regex HiraganaRegex = new Regex(@"^[\u3040-\u309F]+$");

        public static (bool isValid, string errorMessage) ValidateWord(string word, string previousWord, string[] usedWords)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return (false, "単語を入力してください。"); // Please enter a word
            }

            if (!HiraganaRegex.IsMatch(word))
            {
                return (false, "ひらがなだけを使ってください。"); // Please use hiragana only
            }

            if (word.EndsWith("ん"))
            {
                return (false, "'ん'で終わる言葉は使えません。"); // Words ending with 'ん' are not allowed
            }

            if (usedWords != null && usedWords.Contains(word))
            {
                return (false, "その言葉はすでに使われています。"); // That word has already been used
            }

            if (!string.IsNullOrEmpty(previousWord) && 
                !word.StartsWith(previousWord[previousWord.Length - 1].ToString()))
            {
                return (false, $"言葉は「{previousWord[previousWord.Length - 1]}」で始まる必要があります。"); 
            }

            return (true, string.Empty);
        }
    }
}
