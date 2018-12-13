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

    class Worker
    {
        public int ID { get; set; }
        public string WorkingOnInstr { get; set; }
        public int BusyTime { get; set; }

        public Worker(int id)
        {
            this.ID = id;
            this.WorkingOnInstr = string.Empty;
            this.BusyTime = 0;
        }
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

        static Worker GetAvailableWorker(List<Worker> worker)
        {
            Worker work = null;

            foreach(Worker w in worker)
            {
                if (w.WorkingOnInstr.Equals(string.Empty) && (w.BusyTime == 0))
                {
                    work = w;
                    break;
                }
            }

            return work;
        }

        static int GetInstructionTime(Dictionary<string, List<string>> instructionNodes, int baseTime, int workerCount)
        {
            int timeCounter = 0;
            List<Worker> worker = new List<Worker>();

            for (int i = 0; i < workerCount; i++)
            {
                worker.Add(new Worker(i));
            }

            while (true)
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
                foreach(string instr in availableInstructions)
                {
                    Worker availableWorker = GetAvailableWorker(worker);
                    if (availableWorker != null)
                    {
                        availableWorker.WorkingOnInstr = instr;
                        availableWorker.BusyTime = instr[0] - 'A' + baseTime + 1;
                        instructionNodes.Remove(availableWorker.WorkingOnInstr);
                    }
                    else
                    {
                        break;
                    }
                }

                timeCounter++;
                bool allIdle = true;
                foreach (Worker w in worker)
                {
                    if (w.BusyTime > 0)
                    {
                        allIdle = false;
                        w.BusyTime--;
                    }
                    
                    if (!w.WorkingOnInstr.Equals(string.Empty) && (w.BusyTime == 0))
                    {
                        foreach (KeyValuePair<string, List<string>> kvp in instructionNodes)
                        {
                            kvp.Value.Remove(w.WorkingOnInstr);
                        }
                        w.WorkingOnInstr = string.Empty;
                    }
                }
                if (allIdle)
                {
                    break;
                }
            }

            return --timeCounter;
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Dictionary<string, List<string>> instructionNodes = GetInstructionOrder(input);
            string instrOrder = GetInstructionOrder(instructionNodes);
            instructionNodes = GetInstructionOrder(input);
            int instrTime = GetInstructionTime(instructionNodes, 60, 5);

            Console.WriteLine(string.Format("Instruction order: {0}", instrOrder));
            Console.WriteLine(string.Format("Instruction time with helpers: {0}", instrTime));
            Console.ReadLine();
        }
    }
}
