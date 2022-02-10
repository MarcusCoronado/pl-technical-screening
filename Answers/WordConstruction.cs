using System;
using System.Collections.Generic;

namespace Answers;

public static class WordConstruction
{
    public static string[] FindWordConstructions(string[] words, string input)
    {
        var formedWords = new List<string>();
    
        // Make an array containing the alphabet, and the frequency of each character in the input string
        var freq = new int[26];
        foreach (var c in input)
        {
            // fun little character math to get the index within the array
            freq[c - 'a']++;
        }

        foreach (var word in words)
        {
            // No point in checking the word if it is longer than the input
            if (word.Length > input.Length)
            {
                continue;
            }

            var isWordBad = false;
            var cFreq = (int[])freq.Clone();
            
            foreach (var c in word)
            {
                // if a character isn't in the frequency array or there's no more instances, mark it as bad
                if (cFreq[c - 'a'] == 0)
                {
                    isWordBad = true;
                    break;
                }
                
                cFreq[c - 'a']--;
            }

            if (!isWordBad)
                formedWords.Add(word);
        }

        Console.WriteLine($"Constructed Words: [{string.Join(", ", formedWords)}]\n");
        
        return formedWords.ToArray();
    }
}