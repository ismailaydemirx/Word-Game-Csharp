using System;
using System.IO;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        bool playAgain = true;
        double score = 0;

        Console.WriteLine("Welcome to the Word Game!");

        string filePath = @"C:\Users\BAYDEMIR\Desktop\benimkocumnet\Word Game\Word Game\words.txt";
        List<string> words = LoadWordsFromFile(filePath);

        while (playAgain)
        {
            // Kelime Oluşturma Adımı
            string originalWord = GenerateRandomWord(words);

            if (originalWord == null)
            {
                Console.WriteLine("Error: No words available to guess. Exiting the game.");
                break;
            }

            string shuffledWord = ShuffleWord(originalWord);

            Console.WriteLine("Shuffled Word: " + shuffledWord);

            // Kullanıcının tahminini almak
            Console.Write("Guess the word: ");
            string userGuess = Console.ReadLine();

            // Doğru tahmin kontrolü
            if (userGuess.ToLower() == originalWord.ToLower())
            {
                Console.WriteLine("Congratulations! You found the correct word.");
                score += originalWord.Length;
            }
            else
            {
                Console.WriteLine("Incorrect guess. Try again!");
                score -= 0.25;
            }

            // Puanı ekrana yazdırma
            Console.WriteLine("Your score: " + score);

            // Devam etmek isteyip istemediğini sorma
            Console.Write("Do you want to play again? (Y/N): ");
            string playAgainInput = Console.ReadLine().ToUpper();

            if (playAgainInput != "Y")
                playAgain = false;

            Console.WriteLine();
        }

        Console.WriteLine("Thanks for playing. Goodbye!");
        Console.ReadLine();
    }

    // Dosyadan kelimeleri yükleme
    public static List<string> LoadWordsFromFile(string filePath)
    {
        List<string> words = new List<string>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                words.Add(line.Trim());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading words from file: " + ex.Message);
        }

        return words;
    }

    // Rastgele bir kelime oluşturma
    public static string GenerateRandomWord(List<string> words)
    {
        if (words.Count == 0)
            return null;

        Random random = new Random();
        int index = random.Next(words.Count);
        return words[index];
    }

    // Kelimeyi karıştırma
    public static string ShuffleWord(string word)
    {
        char[] letters = word.ToCharArray();
        Random random = new Random();
        int n = letters.Length;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            char temp = letters[k];
            letters[k] = letters[n];
            letters[n] = temp;
        }

        return new string(letters);
    }
}
