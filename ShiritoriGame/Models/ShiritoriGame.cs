using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiritoriGame.Models
{
    public class ShiritoriGame
    {
        private List<string> _usedWords;
        
        private Dictionary<char, List<string>> _botWords = null!;
        
        private Random _random;

        public bool IsGameOver { get; private set; }
        public string LastWord { get; private set; } = string.Empty;
        public string? Winner { get; private set; }

        public ShiritoriGame()
        {
            _usedWords = new List<string>();
            _random = new Random();
            IsGameOver = false;
            InitializeBotWords();
        }

        public void StartGame(string startWord = "しりとり")
        {
            _usedWords.Clear();
            IsGameOver = false;
            Winner = null;
            
            LastWord = startWord;
            _usedWords.Add(startWord);
        }

        public string ProcessPlayerTurn(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return "単語を入力してください。"; // Please enter a word
            }

            var (isValid, errorMessage) = WordValidator.ValidateWord(
                word, 
                LastWord, 
                _usedWords.ToArray());

            if (!isValid)
            {
                return errorMessage;
            }

            _usedWords.Add(word);
            LastWord = word;

            return ProcessBotTurn();
        }

        private string ProcessBotTurn()
        {
            char lastChar = LastWord[LastWord.Length - 1];
            
            if (_botWords.TryGetValue(lastChar, out List<string>? availableWords) && availableWords != null)
            {
                var validWords = availableWords
                    .Where(w => !_usedWords.Contains(w) && !w.EndsWith("ん"))
                    .ToList();

                if (validWords.Count > 0)
                {
                    string botWord = validWords[_random.Next(validWords.Count)];
                    _usedWords.Add(botWord);
                    LastWord = botWord;
                    
                    return $"ボット: {botWord}";
                }
            }

            IsGameOver = true;
            Winner = "プレイヤー"; // Player
            return "ボットは言葉が思いつきません。あなたの勝ちです！"; // Bot can't think of a word. You win!
        }

        private void InitializeBotWords()
        {
            _botWords = new Dictionary<char, List<string>>
            {
                { 'あ', new List<string> { "あめ", "あき", "あさ", "あそび" } },
                { 'い', new List<string> { "いぬ", "いえ", "いた", "いちご" } },
                { 'う', new List<string> { "うみ", "うた", "うし", "うさぎ" } },
                { 'え', new List<string> { "えき", "えほん", "えんぴつ", "えいが" } },
                { 'お', new List<string> { "おかし", "おと", "おにぎり", "おもちゃ" } },
                { 'か', new List<string> { "かさ", "かみ", "かばん", "かえる" } },
                { 'き', new List<string> { "きつね", "きって", "きのこ", "きもの" } },
                { 'く', new List<string> { "くつ", "くま", "くるま", "くち" } },
                { 'け', new List<string> { "けいと", "けしごむ", "けが", "けむり" } },
                { 'こ', new List<string> { "こども", "こえ", "こおり", "こうえん" } },
                { 'さ', new List<string> { "さかな", "さる", "さくら", "さとう" } },
                { 'し', new List<string> { "しま", "しんぶん", "しお", "しろ" } },
                { 'す', new List<string> { "すし", "すな", "すいか", "すずめ" } },
                { 'せ', new List<string> { "せんせい", "せかい", "せみ", "せっけん" } },
                { 'そ', new List<string> { "そら", "そと", "そうじ", "そば" } },
                { 'た', new List<string> { "たこ", "たまご", "たべもの", "たいよう" } },
                { 'ち', new List<string> { "ちず", "ちから", "ちいさい", "ちかてつ" } },
                { 'つ', new List<string> { "つき", "つくえ", "つみき", "つばさ" } },
                { 'て', new List<string> { "てがみ", "てんき", "てぶくろ", "てつ" } },
                { 'と', new List<string> { "とり", "とけい", "とまと", "とびら" } },
                { 'な', new List<string> { "なつ", "なまえ", "なみ", "なし" } },
                { 'に', new List<string> { "にわ", "にく", "にじ", "にんじん" } },
                { 'ぬ', new List<string> { "ぬの", "ぬま", "ぬりえ", "ぬいぐるみ" } },
                { 'ね', new List<string> { "ねこ", "ねつ", "ねぎ", "ねずみ" } },
                { 'の', new List<string> { "のり", "のうと", "のみもの", "のはら" } },
                { 'は', new List<string> { "はな", "はし", "はこ", "はと" } },
                { 'ひ', new List<string> { "ひと", "ひこうき", "ひまわり", "ひつじ" } },
                { 'ふ', new List<string> { "ふね", "ふゆ", "ふでばこ", "ふうせん" } },
                { 'へ', new List<string> { "へや", "へび", "へそ", "へいわ" } },
                { 'ほ', new List<string> { "ほし", "ほん", "ほうき", "ほたる" } },
                { 'ま', new List<string> { "まど", "まち", "まつり", "まくら" } },
                { 'み', new List<string> { "みず", "みかん", "みち", "みどり" } },
                { 'む', new List<string> { "むし", "むら", "むぎ", "むすめ" } },
                { 'め', new List<string> { "めがね", "めだか", "めいろ", "めん" } },
                { 'も', new List<string> { "もり", "もも", "もち", "もんだい" } },
                { 'や', new List<string> { "やま", "やさい", "やかん", "やきゅう" } },
                { 'ゆ', new List<string> { "ゆき", "ゆび", "ゆめ", "ゆうびんきょく" } },
                { 'よ', new List<string> { "よる", "よこ", "ようふく", "よもぎ" } },
                { 'ら', new List<string> { "らくだ", "らいおん", "らっぱ", "らくがき" } },
                { 'り', new List<string> { "りんご", "りす", "りぼん", "りょうり" } },
                { 'る', new List<string> { "るすばん", "るびー", "るーれっと", "るーむ" } },
                { 'れ', new List<string> { "れもん", "れたす", "れいぞうこ", "れんが" } },
                { 'ろ', new List<string> { "ろうそく", "ろぼっと", "ろけっと", "ろば" } },
                { 'わ', new List<string> { "わに", "わた", "わらい", "わごむ" } }
            };
        }

        public string[] GetUsedWords()
        {
            return _usedWords.ToArray();
        }
    }
}
