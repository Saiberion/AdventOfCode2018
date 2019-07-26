using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    public class Day11 : Day
    {
        public Day11()
        {
            Part1Text = "Fuel cell highes power (3x3)";
            Part2Text = "";

            Load("inputs/day11.txt");
        }

        int GetFuelCellPower(int x, int y, int gridSerial)
        {
            int rackID = x + 10;
            int startingPowerLevel = rackID * y;
            int powerLevel = startingPowerLevel + gridSerial;
            powerLevel *= rackID;
            if (powerLevel < 100)
            {
                powerLevel = 0;
            }
            else
            {
                powerLevel /= 100;
                powerLevel %= 10;
            }
            return powerLevel - 5;
        }

        int[,] GetFilledGrid(int gridSerial)
        {
            int[,] grid = new int[300, 300];

            for (int y = 0; y < 300; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    grid[x, y] = GetFuelCellPower(x + 1, y + 1, gridSerial);
                }
            }

            return grid;
        }

        int[] GetHighestPowerCells(int[,] grid, int squaresize)
        {
            int[] result = new int[3];
            int maxPower = int.MinValue;

            for (int y = 0; y < grid.GetLength(1) - (squaresize - 1); y++)
            {
                for (int x = 0; x < grid.GetLength(0) - (squaresize - 1); x++)
                {
                    int power = 0;

                    for(int y1 = 0; y1 < squaresize; y1++)
                    {
                        for (int x1 = 0; x1 < squaresize; x1++)
                        {
                            power += grid[x + x1, y + y1];
                        }
                    }

                    if (maxPower < power)
                    {
                        maxPower = power;
                        result[0] = x + 1;
                        result[1] = y + 1;
                        result[2] = power;
                    }
                }
            }

            return result;
        }

        int[] GetHighestPowerCellsSquare(int[,] grid)
        {
            int[] result = new int[3];
            int maxPower = int.MinValue;
            for (int s = 1; s < grid.GetLength(0); s++)
            {
                int[] powercell = GetHighestPowerCells(grid, s);
                if (maxPower < powercell[2])
                {
                    maxPower = powercell[2];
                    result[0] = powercell[0];
                    result[1] = powercell[1];
                    result[2] = s;
                }
            }
            return result;
        }

        override public void Solve()
        {
            int[,] grid = GetFilledGrid(int.Parse(Input[0]));

            int[] highestPowerCell = GetHighestPowerCells(grid, 3);
            //int[] highestPowerCellSquare = GetHighestPowerCellsSquare(grid);

            Part1Solution = string.Format("{0},{1}", highestPowerCell[0], highestPowerCell[1]);
            //Part2Solution = string.Format("{0},{1},{2}", highestPowerCellSquare[0], highestPowerCellSquare[1], highestPowerCellSquare[2]);
        }
    }
}
