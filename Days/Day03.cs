using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    class Claim
    {
        public int ID { get; set; }
        public Rectangle Area { get; set; }
        public bool DoesOverlap { get; set; }

        public Claim(string descriptor)
        {
            string[] splitted = descriptor.Split(new char[] { '#', '@', ',', ':', 'x' });
            this.ID = int.Parse(splitted[1]);
            this.Area = new Rectangle(int.Parse(splitted[2]), int.Parse(splitted[3]), int.Parse(splitted[4]), int.Parse(splitted[5]));
        }
    }

    public class Day03 : Day
    {
        public Day03()
        {
            Part1Text = "overlapping area";
            Part2Text = "unique claim";

            Load("inputs/day03.txt");
        }

        static int GetOverlappingArea(List<string> input)
        {
            int[,] cloth = new int[2000, 2000];
            int area = 0;

            foreach (string s in input)
            {
                Claim claim = new Claim(s);
                for (int y = claim.Area.Y; y < (claim.Area.Y + claim.Area.Height); y++)
                {
                    for (int x = claim.Area.X; x < (claim.Area.X + claim.Area.Width); x++)
                    {
                        cloth[x, y]++;
                    }
                }
            }

            for (int y = 0; y < 2000; y++)
            {
                for (int x = 0; x < 2000; x++)
                {
                    if (cloth[x, y] > 1)
                    {
                        area++;
                    }
                }
            }
            return area;
        }

        static int FindUniqueClaim(List<string> input)
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

        override public void Solve()
        {
            Part1Solution = GetOverlappingArea(Input).ToString();
            Part2Solution = FindUniqueClaim(Input).ToString();
        }
    }
}
