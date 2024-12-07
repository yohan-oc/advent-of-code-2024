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
            
            string firstLocationKey = $"{curentPosition.Key}, {curentPosition.Value}";
            
            var vistedPositions = new Dictionary<string, string>();
            vistedPositions.Add(firstLocationKey, firstLocationKey);
            
            
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
        
        public enum Direction 
        {
            Up,
            Down,
            Left,
            Right
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
            string[] lines = File.ReadAllLines("Day06/input.txt");
            
            int obstructionsCount = 0;
            
            int rowLengthIndex = lines.Length - 1;
            int colLengthIndex = lines[0].Length - 1;
            
            for(int arrayRowIndex = 0; arrayRowIndex <= rowLengthIndex; arrayRowIndex++)
            {
                for(int arrayColIndex = 0;  arrayColIndex <= colLengthIndex; arrayColIndex++)
                {
                    var currentChar = lines[arrayRowIndex][arrayColIndex];
                    if (currentChar != '#' && currentChar != '^')
                    {
                        string[] newRoute = lines.ToArray();
                        
                        char[] chars = newRoute[arrayRowIndex].ToCharArray();
                        
                        chars[arrayColIndex] = '#';
                        
                        newRoute[arrayRowIndex] = new string(chars);

                        var loopRouteFound = CheckRouteParadox(newRoute);

                        if (loopRouteFound)
                        {
                            obstructionsCount++;
                        }
                    }
                }
            }
            
            Console.WriteLine("Paradox count: "+ obstructionsCount);
        }

        static bool CheckRouteParadox(string[] routeArray)
        {
            int rowLengthIndex = routeArray.Length - 1;
            int colLengthIndex = routeArray[0].Length - 1;
            
            int maxLimit = rowLengthIndex * colLengthIndex;
            
            var startPos = new KeyValuePair<int, int>();

            int rowIndexCount = 0;

            foreach (string line in routeArray)
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
            
            var loopFound = false;
            
            while(patrolling)
            {
                if (stepCount > maxLimit)
                {
                    loopFound = true;
                    patrolling = false;
                }
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
                        if (routeArray[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
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
                        if (routeArray[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
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
                        if (routeArray[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
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
                        if (routeArray[rowNextIndex][colNextIndex] != '#')
                        {
                            //Console.WriteLine(curentDirection);
                            curentPosition = new KeyValuePair<int, int>(rowNextIndex, colNextIndex);
                            stepCount++;
                        }
                        else
                        {
                            curentDirection = GetDirection(curentDirection);
                        }
                    }
                }
            }

            return loopFound;
        }
    }
}

