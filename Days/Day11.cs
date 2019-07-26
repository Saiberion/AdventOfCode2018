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
                    // power is now sum of elements between (0, 0) and bottom right corner of the target square
                    int power = grid[x + squaresize - 1, y + squaresize - 1];

                    // Remove elements between (0, 0) and (x-1, y+squaresize-1)
                    if (x > 0)
                    {
                        power -= grid[x - 1, y + squaresize - 1];
                    }

                    // Remove elements between (0, 0) and (x+squaresize-1, y-1)
                    if (y > 0)
                    {
                        power -= grid[x + squaresize - 1, y - 1];
                    }

                    // Add grid[x-1][y-1] as elements between (0, 0) and (x-1, y-1) are subtracted twice
                    if ((x > 0) && (y > 0))
                    {
                        power += grid[x - 1, y - 1];
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

        int[,] GetSumGrid(int[,] grid)
        {
            int[,] sum = new int[grid.GetLength(0), grid.GetLength(1)];
            // Copy first row of grid[,] to sum[,] 
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                sum[0, i] = grid[0, i];
            }

            // Do column wise sum 
            for (int i = 1; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    sum[i, j] = grid[i, j] + sum[i - 1, j];
                }
            }

            // Do row wise sum 
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 1; j < grid.GetLength(1); j++)
                {
                    sum[i, j] += sum[i, j - 1];
                }
            }

            return sum;
        }

        override public void Solve()
        {
            int[,] grid = GetFilledGrid(int.Parse(Input[0]));
            int[,] sum = GetSumGrid(grid);

            int[] highestPowerCell = GetHighestPowerCells(sum, 3);
            int[] highestPowerCellSquare = GetHighestPowerCellsSquare(sum);

            Part1Solution = string.Format("{0},{1}", highestPowerCell[0], highestPowerCell[1]);
            Part2Solution = string.Format("{0},{1},{2}", highestPowerCellSquare[0], highestPowerCellSquare[1], highestPowerCellSquare[2]);
        }
    }
}
