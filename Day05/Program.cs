using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static string PolymerReaction(string polymer)
        {
            int length = polymer.Length;
            for (int i = 0; i < length - 1; i++)
            {
                if (char.IsLower(polymer[i]))
                {
                    if (char.IsUpper(polymer[i + 1]))
                    {
                        if (char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1]))
                        {
                            polymer = polymer.Remove(i, 2);
                            length -= 2;
                            i -= 2;
                            if (i < 0)
                            {
                                i = -1;
                            }
                            continue;
                        }
                    }
                }
                else // Upper
                {
                    if (char.IsLower(polymer[i + 1]))
                    {
                        if (char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1]))
                        {
                            polymer = polymer.Remove(i, 2);
                            length -= 2;
                            i -= 2;
                            if (i < 0)
                            {
                                i = -1;
                            }
                            continue;
                        }
                    }
                }
            }
            return polymer;
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Dictionary<char, int> availablePolyUnits = new Dictionary<char, int>();
            List<char> availablePolyUnitsKeys = new List<char>();

            string poly = PolymerReaction(input[0]);

            foreach (char c in input[0])
            {
                if (!availablePolyUnits.Keys.Contains(char.ToLower(c)))
                {
                    availablePolyUnits.Add(char.ToLower(c), 0);
                    availablePolyUnitsKeys.Add(char.ToLower(c));
                }
            }

            foreach (char c in availablePolyUnitsKeys)
            {
                string s = input[0].Replace(c.ToString(), "");
                s = s.Replace(char.ToUpper(c).ToString(), "");

                string reduced = PolymerReaction(s);
                availablePolyUnits[c] = reduced.Length;
            }

            int shortest = int.MaxValue;
            foreach (int i in availablePolyUnits.Values)
            {
                if (i < shortest)
                {
                    shortest = i;
                }
            }

            Console.WriteLine(string.Format("reduced polymer length {0}", poly.Length));
            Console.WriteLine(string.Format("shortest polymer {0}", shortest));
            Console.ReadLine();
        }
    }
}
