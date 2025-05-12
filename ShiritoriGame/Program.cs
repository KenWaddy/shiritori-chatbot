using System;
using System.Text;
using ShiritoriGame.Models;

namespace ShiritoriGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            var game = new Models.ShiritoriGame();
            
            DisplayWelcomeMessage();
            
            game.StartGame();
            Console.WriteLine($"ゲームスタート！最初の言葉: {game.LastWord}");
            
            while (!game.IsGameOver)
            {
                Console.Write("\nあなたの番 > ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
                
                string result = game.ProcessPlayerTurn(input);
                Console.WriteLine(result);
                
                DisplayUsedWords(game.GetUsedWords());
            }
            
            Console.WriteLine("\nゲーム終了！");
            if (!string.IsNullOrEmpty(game.Winner))
            {
                Console.WriteLine($"勝者: {game.Winner}");
            }
            
            Console.WriteLine("\nもう一度プレイしますか？ (y/n)");
            string playAgain = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
            
            if (playAgain == "y")
            {
                Console.Clear();
                Main(args); // Restart the game
            }
            else
            {
                Console.WriteLine("プレイしてくれてありがとう！");
            }
        }
        
        static void DisplayWelcomeMessage()
        {
            Console.WriteLine("===================================");
            Console.WriteLine("     日本語しりとりゲーム");
            Console.WriteLine("===================================");
            Console.WriteLine("\n【ルール】");
            Console.WriteLine("1. ひらがなのみを使用してください");
            Console.WriteLine("2. 前の言葉の最後の文字から始まる言葉を入力してください");
            Console.WriteLine("3. 「ん」で終わる言葉は使えません");
            Console.WriteLine("4. 同じ言葉を二度使うことはできません");
            Console.WriteLine("\n===================================\n");
        }
        
        static void DisplayUsedWords(string[] usedWords)
        {
            Console.WriteLine("\n【使用済みの言葉】");
            Console.WriteLine(string.Join(" → ", usedWords));
        }
    }
}
