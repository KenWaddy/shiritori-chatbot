using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ShiritoriGame.Models
{
    public class WordValidator
    {
        private static readonly Regex HiraganaRegex = new Regex(@"^[\u3040-\u309F]+$");

        public static string NormalizeKana(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder result = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                switch (c)
                {
                    case 'が': result.Append('か'); break;
                    case 'ぎ': result.Append('き'); break;
                    case 'ぐ': result.Append('く'); break;
                    case 'げ': result.Append('け'); break;
                    case 'ご': result.Append('こ'); break;
                    case 'ざ': result.Append('さ'); break;
                    case 'じ': result.Append('し'); break;
                    case 'ず': result.Append('す'); break;
                    case 'ぜ': result.Append('せ'); break;
                    case 'ぞ': result.Append('そ'); break;
                    case 'だ': result.Append('た'); break;
                    case 'ぢ': result.Append('ち'); break;
                    case 'づ': result.Append('つ'); break;
                    case 'で': result.Append('て'); break;
                    case 'ど': result.Append('と'); break;
                    case 'ば': result.Append('は'); break;
                    case 'び': result.Append('ひ'); break;
                    case 'ぶ': result.Append('ふ'); break;
                    case 'べ': result.Append('へ'); break;
                    case 'ぼ': result.Append('ほ'); break;
                    
                    case 'ぱ': result.Append('は'); break;
                    case 'ぴ': result.Append('ひ'); break;
                    case 'ぷ': result.Append('ふ'); break;
                    case 'ぺ': result.Append('へ'); break;
                    case 'ぽ': result.Append('ほ'); break;
                    
                    default: result.Append(c); break;
                }
            }

            return result.ToString();
        }

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

            if (!string.IsNullOrEmpty(previousWord))
            {
                char lastChar = previousWord[previousWord.Length - 1];
                string normalizedLastChar = NormalizeKana(lastChar.ToString());
                
                string firstChar = word[0].ToString();
                string normalizedFirstChar = NormalizeKana(firstChar);
                
                if (normalizedFirstChar != normalizedLastChar)
                {
                    return (false, $"言葉は「{lastChar}」で始まる必要があります。");
                }
            }

            return (true, string.Empty);
        }
    }
}
