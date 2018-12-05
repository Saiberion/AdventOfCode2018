using AoCHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Day03
{
    class  Claim
    {
        public int ID { get; set; }
        public Rectangle Area { get; set; }
        public bool DoesOverlap { get; set; }

        public Claim(string descriptor)
        {
            string[] splitted = descriptor.Split(new char[] {'#', '@', ',', ':', 'x'});
            this.ID = int.Parse(splitted[1]);
            this.Area = new Rectangle(int.Parse(splitted[2]), int.Parse(splitted[3]), int.Parse(splitted[4]), int.Parse(splitted[5]));
        }
    }

    class Program
    {
        static int GetOverlappingArea(List<string> input)
        {
            int[,] cloth = new int[2000, 2000];
            int area = 0;
            
            foreach(string s in input)
            {
                Claim claim = new Claim(s);
                for (int y = claim.Area.Y; y < (claim.Area.Y + claim.Area.Height); y++)
                {
                    for (int x = claim.Area.X; x < (claim.Area.X + claim.Area.Width); x++)
                    {
                        cloth[x,y]++;
                    }
                }
            }

            for (int y = 0; y < 2000;  y++)
            {
                for (int x = 0; x < 2000; x++)
                {
                    if (cloth[x,y] > 1)
                    {
                        area++;
                    }
                }
            }
            return area;
        }

        static int findUniqueClaim(List<string> input)
        {
            List<Claim> claims = new List<Claim>();
            foreach (string s in input)
            {
                claims.Add(new Claim(s));
            }

            for (int i = 0; i < claims.Count; i++)
            {
                for (int j = 0; j < claims.Count; j++)
                {
                    if (i != j)
                    {
                        if (claims[i].Area.IntersectsWith(claims[j].Area))
                        {
                            claims[i].DoesOverlap = true;
                            claims[j].DoesOverlap = true;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < claims.Count; i++)
            {
                if (!claims[i].DoesOverlap)
                {
                    return claims[i].ID;
                }
            }
            return 0;
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Console.WriteLine(string.Format("overlapping area: {0}", GetOverlappingArea(input)));
            Console.WriteLine(string.Format("unique claim {0}", findUniqueClaim(input)));
            Console.ReadLine();
        }
    }
}
