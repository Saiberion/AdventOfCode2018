using AoCHelpers;
using System;
using System.Collections.Generic;

namespace Day02
{
    class Program
    {
        static int find2MatchingDigits(string input)
        {
            Dictionary<char, int> matches = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (!matches.ContainsKey(c))
                {
                    matches.Add(c, 0);
                }
                matches[c]++;
            }
            foreach (int i in matches.Values)
            {
                if (i == 2)
                {
                    return 1;
                }
            }
            return 0;
        }

        static int find3MatchingDigits(string input)
        {
            Dictionary<char, int> matches = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (!matches.ContainsKey(c))
                {
                    matches.Add(c, 0);
                }
                matches[c]++;
            }
            foreach (int i in matches.Values)
            {
                if (i == 3)
                {
                    return 1;
                }
            }
            return 0;
        }

        static int calculateChecksum(List<string> input)
        {
            int[] chkPart = new int[2]; // 0: Exactly 2 matching digits - 1: Exactly 3 matching digits

            foreach (string s in input)
            {
                chkPart[0] += find2MatchingDigits(s);
                chkPart[1] += find3MatchingDigits(s);
            }

            return chkPart[0] * chkPart[1];
        }

        static string findClosestIDCommons(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                for( int j = i + 1; j < input.Count; j++)
                {
                    char[] c1, c2;
                    c1 = input[i].ToCharArray();
                    c2 = input[j].ToCharArray();
                    int fromStart = 0, fromEnd = c1.Length;

                    for (int k = 0; k < c1.Length; k++)
                    {
                        if (c1[k] != c2[k])
                        {
                            fromStart = k;
                            break;
                        }
                    }
                    for (int k = c1.Length - 1; k >= 0; k--)
                    {
                        if (c1[k] != c2[k])
                        {
                            fromEnd = k;
                            break;
                        }
                    }
                    if (fromStart == fromEnd)
                    {
                        string s = new string(c1);
                        return s.Remove(fromStart, 1);
                    }
                }
            }

            return string.Empty;
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Console.WriteLine(string.Format("Checksum: {0}", calculateChecksum(input)));
            Console.WriteLine(string.Format("Common digits of closest IDs: {0}", findClosestIDCommons(input)));
            Console.ReadLine();
        }
    }
}
