using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Aoc2024.Day06
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day06/input.txt");

            int rowLengthIndex = lines.Length - 1;
            int colLengthIndex = lines[0].Length - 1;
            
            var startPos = new KeyValuePair<int, int>();

            int rowIndexCount = 0;

            foreach (string line in lines)
            {
                if(line.Contains("^"))
                {
                    int colIndex = line.IndexOf("^");

                    startPos = new KeyValuePair<int, int>(rowIndexCount, colIndex);
                    break;
                }
                rowIndexCount++;
            }

            bool patrolling = true;

            int stepCount = 0;

            var curentDirection = Direction.Up;

            var curentPosition = startPos;
            
            string firstLocation = $"{curentPosition.Key}, {curentPosition.Value}";
            
            var vistedPositions = new Dictionary<string, string>();
            vistedPositions.Add(firstLocation, firstLocation);
            
            
            while(patrolling)
            {
                int rowIndex = curentPosition.Key;
                int colIndex = curentPosition.Value;
                if (
                       (rowIndex == 0 && curentDirection == Direction.Up) 
                    || (colIndex == 0 && curentDirection == Direction.Left) 
                    || (rowIndex == rowLengthIndex && curentDirection == Direction.Down) 
                    || (colIndex == colLengthIndex && curentDirection == Direction.Right))
                {
                    patrolling = false;
                }
                else
                {
                    // array reference
                    // Console.WriteLine("expected # actual:" + lines[0][4]);
                    // Console.WriteLine("expected # actual:" + lines[1][9]);
                    
                    if (curentDirection == Direction.Up)
                    {
                        int rowNextIndex = rowIndex - 1;
                        int colNextIndex = colIndex;
                        if (lines[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
                            string positionString = $"{curentPosition.Key}, {curentPosition.Value}";
                            vistedPositions.TryAdd(positionString, positionString);
                        }
                        else
                        {
                            curentDirection = GetDirection(curentDirection);
                        }
                    }
                    else if (curentDirection == Direction.Right)
                    {
                        int rowNextIndex = rowIndex;
                        int colNextIndex = colIndex + 1;
                        if (lines[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
                            string positionString = $"{curentPosition.Key}, {curentPosition.Value}";
                            vistedPositions.TryAdd(positionString, positionString);
                        }
                        else
                        {
                            curentDirection = GetDirection(curentDirection);
                        }
                    }
                    else if (curentDirection == Direction.Down)
                    {
                        int rowNextIndex = rowIndex + 1;
                        int colNextIndex = colIndex;
                        if (lines[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
                            string positionString = $"{curentPosition.Key}, {curentPosition.Value}";
                            vistedPositions.TryAdd(positionString, positionString);
                        }
                        else
                        {
                            curentDirection = GetDirection(curentDirection);
                        }
                    }
                    else // if (curentDirection == Direction.Left)
                    {
                        int rowNextIndex = rowIndex;
                        int colNextIndex = colIndex - 1;
                        if (lines[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
                            string positionString = $"{curentPosition.Key}, {curentPosition.Value}";
                            vistedPositions.TryAdd(positionString, positionString);
                        }
                        else
                        {
                            curentDirection = GetDirection(curentDirection);
                        }
                    }
                }
            }
            
            Console.WriteLine("Step Count: " + stepCount);
            Console.WriteLine("Distinct Step Count: " + vistedPositions.Count);
            //Console.WriteLine("Position : " + startPos.Key + " " + startPos.Value);
        }

        static Direction GetDirection(Direction direction)
        {
            if (direction == Direction.Up)
            {
                return Direction.Right;
            }
            if (direction == Direction.Right)
            {
                return Direction.Down;
            }
            if (direction == Direction.Down)
            {
                return Direction.Left;
            }
            return Direction.Up;
        }

        public static void Part02()
        {
            string[] rules = File.ReadAllLines("Day06/testData.txt");

            

            Console.WriteLine("Total: ");
        }
    }

    public enum Direction 
    {
        Up,
        Down,
        Left,
        Right
    }
    
    
}

