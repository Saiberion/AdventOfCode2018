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

        static Dictionary<int, int> GetGuardsSleepTime(List<GuardLog> log)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();



            return new Dictionary<int, int>();
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("example.txt");
            List<GuardLog> guardLog = FillGuardLog(input);
            Dictionary<int, int> guardSleepTime = GetGuardsSleepTime(guardLog);
            //Console.WriteLine(string.Format("overlapping area: {0}", GetOverlappingArea(input)));
            //Console.WriteLine(string.Format("unique claim {0}", findUniqueClaim(input)));
            Console.ReadLine();
        }
    }
}
