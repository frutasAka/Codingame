using System;
using System.Collections.Generic;
using System.Text;

class Solution
{
    static string[] MorseLetters = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.."};

    static string InputMorseString = "";
    static Dictionary<string, int> Occurrences = new Dictionary<string, int>();
    static long[] Combos;
    static int Max = 0;

    public static void Main(string[] args)
    {
        InputMorseString = Console.ReadLine();
        int numberWords = Convert.ToInt32(Console.ReadLine());
        Combos = new long[InputMorseString.Length];
        for (int i = 0; i < numberWords; i++)
        {
            Morph(Console.ReadLine());
        }
        Console.WriteLine(TryCombos(0));
    }

    public static void Morph(string word)
    {
        StringBuilder morphed = new StringBuilder();
        for (int i = 0; i < word.Length; i++)
        {
            morphed.Append(MorseLetters[word[i] - 'A']);
        }
        string morphedStr = morphed.ToString();
        if (InputMorseString.Contains(morphedStr))
        {
            if (Occurrences.ContainsKey(morphedStr))
            {
                Occurrences[morphedStr]++;
            }
            else
            {
                Max = Math.Max(Max, morphedStr.Length);
                Occurrences[morphedStr] = 1;
            }
        }
    }

    public static long TryCombos(int start)
    {
        if (start == InputMorseString.Length)
        {
            return 1L;
        }
        if (Combos[start] != 0)
        {
            return Combos[start] - 1L;
        }
        long result = 0L;
        for (int i = 1; i <= Max && start + i <= InputMorseString.Length; i++)
        {
            string substr = InputMorseString.Substring(start, i);
            if (Occurrences.TryGetValue(substr, out int freq))
            {
                result += (long)freq * TryCombos(start + i);
            }
        }
        Combos[start] = result + 1L;
        return result;
    }
}
