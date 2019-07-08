using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    public class Day01 : Day
    {
        public Day01()
        {
            Part1Text = "Final frequency";
            Part2Text = "First duplicate frequency";

            Load("inputs/day01.txt");
        }

        override public void Solve()
        {
            int result = 0;
            foreach (string s in Input)
            {
                result += int.Parse(s);
            }
            Part1Solution = result.ToString();

            result = 0;
            bool foundDuplicate = false;
            HashSet<int> previousFrequencies = new HashSet<int>
            {
                result
            };
            while (!foundDuplicate)
            {
                foreach (string s in Input)
                {
                    result += int.Parse(s);

                    if (!previousFrequencies.Add(result))
                    {
                        foundDuplicate = true;
                        break;
                    }
                }
            }
            Part2Solution = result.ToString();
        }
    }
}
