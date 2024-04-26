using System.IO;
using UnityEngine;
using Random = System.Random;

public static class RandomNameGenerator
{
    public static string GenerateRandomName()
    {
        //gets path of project then appends "Assets/Scripts/Player Components/
        string dirpath = Path.GetFullPath("Assets/Scripts/Player Components/");
        
        //gets random word from adjectives.txt file stored in "Assets/Scripts/Player Components/"
        string adjective = GetRandomWordFromTextFile(Path.Combine(dirpath, "adjectives.txt"));
        //gets random word from noun.txt file stored in "Assets/Scripts/Player Components/"
        string noun = GetRandomWordFromTextFile(Path.Combine(dirpath, "nouns.txt"));
        
        Debug.Log($"Random adjective: {adjective} Random noun: {noun}");
        //username
        return adjective + noun;
    }

    private static string GetRandomWordFromTextFile(string filepath)
    {
        string[] lines;
        string randomLine;
        Random random = new Random();
        
        //used to debug if file was found 
        Debug.Log($"File.Exists {filepath} = {File.Exists(filepath)}");
        if (File.Exists(filepath))
        {
            //all the lines from the file
            lines = File.ReadAllLines(filepath);
            //gets random line
            randomLine = lines[random.Next(lines.Length)];
            Debug.Log($"file accessed. RandomLine = {randomLine}");
        }
        //file not found 
        else
        {
            randomLine = null;
        }
        //returns random word or null
        return randomLine;
    }
}
