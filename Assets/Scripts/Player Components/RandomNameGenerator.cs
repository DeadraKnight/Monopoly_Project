using System.IO;
using UnityEngine;
using Random = System.Random;

public static class RandomNameGenerator
{
    public static string GenerateRandomName()
    {
        string dirpath = Path.GetFullPath("Assets/Scripts/Player Components/");
        
        string adjective = GetRandomWordFromTextFile(Path.Combine(dirpath, "adjectives.txt"));
        string noun = GetRandomWordFromTextFile(Path.Combine(dirpath, "nouns.txt"));
        
        Debug.Log($"Random adjective: {adjective} Random noun: {noun}");
        return adjective + noun;
    }

    private static string GetRandomWordFromTextFile(string filepath)
    {
        string[] lines;
        string randomLine;
        Random random = new Random();
        
        Debug.Log($"File.Exists {filepath} = {File.Exists(filepath)}");
        if (File.Exists(filepath))
        {
            lines = File.ReadAllLines(filepath);
            randomLine = lines[random.Next(lines.Length)];
            Debug.Log($"file accessed. RandomLine = {randomLine}");
        }
        else
        {
            randomLine = null;
        }
        return randomLine;
    }
}
