using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    class Instruction
    {
        public string Name { get; set; }
        public List<string> PreCondition { get; set; }
    }

    class Program
    {
        static Dictionary<string, List<string>> GetInstructionOrder(List<string> input)
        {
            Dictionary<string, List<string>> instructionNodes = new Dictionary<string, List<string>>();
            foreach (string s in input)
            {
                string[] splitted = s.Split(new char[] { ' ' });
                string preCond = splitted[1];
                string instrNode = splitted[7];

                if (!instructionNodes.ContainsKey(preCond))
                {
                    instructionNodes.Add(preCond, new List<string>());
                }

                if (!instructionNodes.ContainsKey(instrNode))
                {
                    instructionNodes.Add(instrNode, new List<string>());
                }

                instructionNodes[instrNode].Add(preCond);
            }

            return instructionNodes;
        }

        static string GetInstructionOrder(Dictionary<string, List<string>> instructionNodes)
        {
            StringBuilder sb = new StringBuilder();

            while (instructionNodes.Count > 0)
            {
                List<string> availableInstructions = new List<string>();
                foreach (KeyValuePair<string, List<string>> kvp in instructionNodes)
                {
                    if (kvp.Value.Count == 0)
                    {
                        availableInstructions.Add(kvp.Key);
                    }
                }

                availableInstructions.Sort();
                sb.Append(availableInstructions[0]);
                instructionNodes.Remove(availableInstructions[0]);

                foreach (KeyValuePair<string, List<string>> kvp in instructionNodes)
                {
                    kvp.Value.Remove(availableInstructions[0]);
                }
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Dictionary<string, List<string>> instructionNodes = GetInstructionOrder(input);
            string instrOrder = GetInstructionOrder(instructionNodes);

            Console.WriteLine(string.Format("Instruction order: {0}", instrOrder));
            Console.ReadLine();
        }
    }
}
