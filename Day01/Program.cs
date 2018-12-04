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

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Console.WriteLine(string.Format("Final frequency: {0}", FrequencyShifting(input)));
            Console.ReadLine();
        }
    }
}
