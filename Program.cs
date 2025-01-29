using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static HashSet<string> LoadWords(string filename = "words.txt")
    {
        return new HashSet<string>(File.ReadAllLines(filename).Select(word => word.Trim().ToLower()));
    }

    static Dictionary<int, List<string>> GenerateWords(string letters, HashSet<string> wordSet)
    {
        var validWords = new Dictionary<int, HashSet<string>>();

        for (int length = 2; length <= letters.Length; length++)
        {
            foreach (var perm in GetPermutations(letters, length))
            {
                string word = new string(perm.ToArray());
                if (wordSet.Contains(word))
                {
                    if (!validWords.ContainsKey(word.Length))
                        validWords[word.Length] = new HashSet<string>();

                    validWords[word.Length].Add(word);
                }
            }
        }

        return validWords.ToDictionary(pair => pair.Key, pair => pair.Value.OrderBy(w => w).ToList());
    }

    static IEnumerable<IEnumerable<char>> GetPermutations(string letters, int length)
    {
        return Permute(letters.ToCharArray(), length);
    }

    static IEnumerable<IEnumerable<T>> Permute<T>(T[] array, int length)
    {
        if (length == 1) return array.Select(t => new T[] { t });

        return Permute(array, length - 1)
            .SelectMany(t => array.Where(e => !t.Contains(e)),
                        (t, e) => t.Concat(new T[] { e }));
    }

    static void DisplayWords(Dictionary<int, List<string>> groupedWords)
    {
        foreach (var length in groupedWords.Keys.OrderByDescending(k => k))
        {
            Console.WriteLine($"\n{length}-letter words:");
            Console.WriteLine(string.Join(", ", groupedWords[length]));
        }
    }

    static void Main()
    {
        var wordSet = LoadWords();
        Console.Write("Enter the letters: ");
        string letters = Console.ReadLine().ToLower();

        var groupedWords = GenerateWords(letters, wordSet);

        if (groupedWords.Count > 0)
            DisplayWords(groupedWords);
        else
            Console.WriteLine("No words found!");
    }
}
