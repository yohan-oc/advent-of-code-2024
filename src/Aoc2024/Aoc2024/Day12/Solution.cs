using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2024.Day12
{
    public static class Solution
    {
        public static void Part01()
        {
            Console.WriteLine("**** Day 12 - Part 01 ****");

            string[] lines = File.ReadAllLines("Day12/input.txt");

            int rows = lines.Length;
            int cols = lines.Max(str => str.Length);
            
            char[,] grid = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string currentString = lines[i];
                for (int j = 0; j < currentString.Length; j++)
                {
                    grid[i, j] = currentString[j];
                }
            }
            
            var totalPrice = 0;
            
            Dictionary<char, List<(int area, int perimeter)>> regionDetails = CalculateRegionDetails(grid);

            foreach (var region in regionDetails)
            {
                int price = 0;
                foreach (var subregion in region.Value)
                {
                    int subregionPrice = subregion.area * subregion.perimeter;
                    totalPrice += subregionPrice;
                }
                totalPrice = totalPrice + price;
            }
            
            Console.WriteLine($"Total price: {totalPrice}");
        }
        
        static Dictionary<char, List<(int area, int perimeter)>> CalculateRegionDetails(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            bool[,] visited = new bool[rows, cols];
            Dictionary<char, List<(int area, int perimeter)>> result = new Dictionary<char, List<(int area, int perimeter)>>();

            // Directions for neighbors (top, right, bottom, left)
            int[] dRow = { -1, 0, 1, 0 };
            int[] dCol = { 0, 1, 0, -1 };

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (!visited[r, c])
                    {
                        char region = grid[r, c];
                        int area = 0, perimeter = 0;
                        FloodFill(grid, visited, r, c, region, ref area, ref perimeter, dRow, dCol);

                        if (!result.ContainsKey(region))
                            result[region] = new List<(int, int)>();

                        result[region].Add((area, perimeter));
                    }
                }
            }

            return result;
        }

        static void FloodFill(char[,] grid, bool[,] visited, int r, int c, char region, ref int area, ref int perimeter, int[] dRow, int[] dCol)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((r, c));
            visited[r, c] = true;

            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();
                area++;

                for (int i = 0; i < 4; i++)
                {
                    int newRow = row + dRow[i];
                    int newCol = col + dCol[i];

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        if (grid[newRow, newCol] == region)
                        {
                            if (!visited[newRow, newCol])
                            {
                                queue.Enqueue((newRow, newCol));
                                visited[newRow, newCol] = true;
                            }
                        }
                        else
                        {
                            perimeter++;
                        }
                    }
                    else
                    {
                        perimeter++;
                    }
                }
            }
        }
        
        public static void Part02()
        {
            Console.WriteLine("**** Day 12 - Part 02 ****");

            string numbersString = File.ReadAllLines("Day12/input.txt")[0];

            
            
            Console.WriteLine($"Total price: ");
        }

    }
}