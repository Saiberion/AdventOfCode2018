using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        static int FrequencyShifting(List<string> numberList)
        {
            int result = 0;
            foreach(string s in numberList)
            {
                result += int.Parse(s);
            }
            return result;
        }

        static int FrequencyShiftingFirstDuplicate(List<string> numberList)
        {
            int result = 0;
            bool foundDuplicate = false;
            List<int> previousFrequencies = new List<int>();
            previousFrequencies.Add(result);
            while (!foundDuplicate)
            {
                foreach (string s in numberList)
                {
                    result += int.Parse(s);
                    if (previousFrequencies.Contains(result))
                    {
                        foundDuplicate = true;
                        break;
                    }
                    else
                    {
                        previousFrequencies.Add(result);
                    }
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Console.WriteLine(string.Format("Final frequency: {0}", FrequencyShifting(input)));
            Console.WriteLine(string.Format("First duplicate frequency: {0}", FrequencyShiftingFirstDuplicate(input)));
            Console.ReadLine();
        }
    }
}
