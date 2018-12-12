using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    enum EGuardAction
    {
        eGuardActionBeginsShift,
        eGuardActionFallsAsleep,
        eGuardActionWakesUp,
    }

    class GuardLog
    {
        public DateTime TimeStamp { get; set; }
        public int GuardID { get; set; }
        public EGuardAction GuardAction { get; set; }

        public GuardLog(string input)
        {
            string[] splittedEntry = input.Split(new char[] { '[', ']' });
            this.TimeStamp = DateTime.ParseExact(splittedEntry[1], "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            if (splittedEntry[2].Contains("Guard #"))
            {
                string[] extraSplit = splittedEntry[2].Split(new char[] { ' ' });
                this.GuardID = int.Parse(extraSplit[2].Remove(0, 1));
                this.GuardAction = EGuardAction.eGuardActionBeginsShift;
            }
            else if (splittedEntry[2].Contains("falls"))
            {
                this.GuardAction = EGuardAction.eGuardActionFallsAsleep;
            }
            else if (splittedEntry[2].Contains("wakes"))
            {
                this.GuardAction = EGuardAction.eGuardActionWakesUp;
            }
        }
    }

    class Program
    {
        static List<GuardLog> FillGuardLog(List<string> input)
        {
            List<GuardLog> sortedGuardLog = new List<GuardLog>();

            foreach (string s in input)
            {
                int i;
                GuardLog gl = new GuardLog(s);

                for (i = 0; i < sortedGuardLog.Count; i++)
                {
                    if (gl.TimeStamp < sortedGuardLog[i].TimeStamp)
                    {
                        break;
                    }
                }
                sortedGuardLog.Insert(i, gl);
            }

            int currentID = 0;
            foreach(GuardLog g in sortedGuardLog)
            {
                if (g.GuardAction == EGuardAction.eGuardActionBeginsShift)
                {
                    currentID = g.GuardID;
                }
                else
                {
                    g.GuardID = currentID;
                }
            }
            return sortedGuardLog;
        }

        static Dictionary<int, int[]> CreateGuardSleepChart(List<GuardLog> log)
        {
            Dictionary<int, int[]> dict = new Dictionary<int, int[]>();

            int sleepStart = 0;
            int sleepEnd = 0;
            int currentGuard = 0;
            foreach (GuardLog gl in log)
            {
                switch (gl.GuardAction)
                {
                    case EGuardAction.eGuardActionBeginsShift:
                        currentGuard = gl.GuardID;
                        if (!dict.Keys.Contains(currentGuard))
                        {
                            dict.Add(currentGuard, new int[60]);
                        }
                        break;
                    case EGuardAction.eGuardActionFallsAsleep:
                        sleepStart = gl.TimeStamp.Minute;
                        break;
                    case EGuardAction.eGuardActionWakesUp:
                        sleepEnd = gl.TimeStamp.Minute;

                        for (int i = sleepStart; i < sleepEnd; i++)
                        {
                            dict[currentGuard][i]++;
                        }
                        break;
                }
            }

            return dict;
        }

        static int[] GetMostAsleepStrat1(Dictionary<int, int[]> sleepChart)
        {
            Dictionary<int, int> sleepTime = new Dictionary<int, int>();

            foreach(int g in sleepChart.Keys)
            {
                sleepTime.Add(g, 0);
                for (int i = 0; i < sleepChart[g].Length; i++)
                {
                    sleepTime[g] += sleepChart[g][i];
                }
            }

            int sleptMinutes = 0;
            int sleepyGuard = 0;

            foreach (KeyValuePair<int, int> kvp in sleepTime)
            {
                if (kvp.Value > sleptMinutes)
                {
                    sleepyGuard = kvp.Key;
                    sleptMinutes = kvp.Value;
                }
            }

            int sleepyMinute = 0;
            int sleepyMinuteTime = 0;
            for (int i = 0; i < sleepChart[sleepyGuard].Length; i++)
            {
                if (sleepChart[sleepyGuard][i] > sleepyMinuteTime)
                {
                    sleepyMinuteTime = sleepChart[sleepyGuard][i];
                    sleepyMinute = i;
                }
            }

            return new int[] { sleepyGuard, sleepyMinute };
        }

        static int[] GetMostAsleepStrat2(Dictionary<int, int[]> sleepChart)
        {
            //Dictionary<int, int> sleepTime = new Dictionary<int, int>();

            int sleepyMinuteTime = -1;
            int sleepyMinute = -1;
            int sleepyGuard = -1;
            foreach (int g in sleepChart.Keys)
            {
                for (int i = 0; i < sleepChart[g].Length; i++)
                {
                    if (sleepChart[g][i] > sleepyMinuteTime)
                    {
                        sleepyMinuteTime = sleepChart[g][i];
                        sleepyMinute = i;
                        sleepyGuard = g;
                    }
                }
            }

            return new int[] { sleepyGuard, sleepyMinute };
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            List<GuardLog> guardLog = FillGuardLog(input);
            Dictionary<int, int[]> sleepChart = CreateGuardSleepChart(guardLog);

            int[] resultStrat1 = GetMostAsleepStrat1(sleepChart);
            int[] resultStrat2 = GetMostAsleepStrat2(sleepChart);

            Console.WriteLine(string.Format("Strat 1: chosen guard * sleepiest minute = {0}", resultStrat1[0] * resultStrat1[1]));
            Console.WriteLine(string.Format("strat 2: chosen guard * sleepiest minute = {0}", resultStrat2[0] * resultStrat2[1]));
            Console.ReadLine();
        }
    }
}
