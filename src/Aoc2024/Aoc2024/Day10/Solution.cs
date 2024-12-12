using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Aoc2024.Day10
{
    public static class Solution
    {
        public static void Part01()
        {
            Console.WriteLine("**** Day 10 - Part 01 ****");

            string[] trails = File.ReadAllLines("Day10/input.txt");

            int rowLengthIndex = trails.Length - 1;
            int colLengthIndex = trails[0].Length - 1;

            var listOfHighestPoints = new Dictionary<string, List<KeyValuePair<int, int>>>();

            for (int arrayRowIndex = 0; arrayRowIndex <= rowLengthIndex; arrayRowIndex++)
            {
                for (int arrayColIndex = 0; arrayColIndex <= colLengthIndex; arrayColIndex++)
                {
                    var currentPoint = trails[arrayRowIndex][arrayColIndex];
                    if (currentPoint == '0')
                    {
                        listOfHighestPoints.Add($"{arrayRowIndex}-{arrayColIndex}", null);
                    }
                }
            }

            var paths = new Dictionary<string, string>();

            foreach (var highestPoint in listOfHighestPoints)
            {
                var highestPointX = highestPoint.Key.Split("-")[0];
                var highestPointY = highestPoint.Key.Split("-")[1];

                var level9Point = new Point { X = highestPointX, Y = highestPointY };

                var point9ValueLog = level9Point+ "->" +
                                     trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)];
                Console.WriteLine($"Level 09 log: {point9ValueLog}");
                
                var level8IndexPoints = SearchPoints(level9Point, "1", trails);
                
                foreach (var level8IndexPoint in level8IndexPoints)
                {
                    var level8Point = new Point
                        { X = level8IndexPoint.Key.ToString(), Y = level8IndexPoint.Value.ToString() };

                    var point8ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                         + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)];
                    Console.WriteLine($"Level 08 log: {point8ValueLog}");
                    
                    var level7IndexPoints = SearchPoints(level8Point, "2", trails);

                    foreach (var level7IndexPoint in level7IndexPoints)
                    {
                        var level7Point = new Point
                            { X = level7IndexPoint.Key.ToString(), Y = level7IndexPoint.Value.ToString() };

                        var point7ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                             + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                             + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)];
                        Console.WriteLine($"Level 07 log: {point7ValueLog}");
                        
                        var level6IndexPoints = SearchPoints(level7Point, "3", trails);

                        foreach (var level6IndexPoint in level6IndexPoints)
                        {
                            var level6Point = new Point
                                { X = level6IndexPoint.Key.ToString(), Y = level6IndexPoint.Value.ToString() };

                            var point6ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                 + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                 + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                 + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)];
                            Console.WriteLine($"Level 06 log: {point6ValueLog}");
                            
                            var level5IndexPoints = SearchPoints(level6Point, "4", trails);

                            foreach (var level5IndexPoint in level5IndexPoints)
                            {
                                var level5Point = new Point
                                    { X = level5IndexPoint.Key.ToString(), Y = level5IndexPoint.Value.ToString() };

                                var point5ValueLog =level9Point+ "->" + trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                    + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                    + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                    + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                    + level5Point+ "->" +trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)];
                                Console.WriteLine($"Level 05 log: {point5ValueLog}");
                                
                                var level4IndexPoints = SearchPoints(level5Point, "5", trails);

                                foreach (var level4IndexPoint in level4IndexPoints)
                                {
                                    var level4Point = new Point
                                        { X = level4IndexPoint.Key.ToString(), Y = level4IndexPoint.Value.ToString() };

                                    var point4ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + ""
                                                         + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                         + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                         + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                         + level5Point+ "->" +trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)]
                                                         + level4Point+ "->" +trails[int.Parse(level4Point.X)][int.Parse(level4Point.Y)];
                                    Console.WriteLine($"Level 04 log: {point4ValueLog}");
                                    
                                    var level3IndexPoints = SearchPoints(level4Point, "6", trails);

                                    foreach (var level3IndexPoint in level3IndexPoints)
                                    {
                                        var level3Point = new Point
                                        {
                                            X = level3IndexPoint.Key.ToString(), Y = level3IndexPoint.Value.ToString()
                                        };

                                        var point3ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                             + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                             + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                             + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                             + level5Point+ "->" +trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)]
                                                             + level4Point+ "->" +trails[int.Parse(level4Point.X)][int.Parse(level4Point.Y)]
                                                             + level3Point+ "->" +trails[int.Parse(level3Point.X)][int.Parse(level3Point.Y)];
                                        Console.WriteLine($"Level 03 log: {point3ValueLog}");
                                        
                                        var level2IndexPoints = SearchPoints(level3Point, "7", trails);

                                        foreach (var level2IndexPoint in level2IndexPoints)
                                        {
                                            var level2Point = new Point
                                            {
                                                X = level2IndexPoint.Key.ToString(),
                                                Y = level2IndexPoint.Value.ToString()
                                            };
                                            
                                            var point2ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                                 + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                                 + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                                 + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                                 + level5Point+ "->" +trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)]
                                                                 + level4Point+ "->" +trails[int.Parse(level4Point.X)][int.Parse(level4Point.Y)]
                                                                 + level3Point+ "->" +trails[int.Parse(level3Point.X)][int.Parse(level3Point.Y)]
                                                                 + level2Point+ "->" +trails[int.Parse(level2Point.X)][int.Parse(level2Point.Y)];
                                            Console.WriteLine($"Level 02 log: {point2ValueLog}");
                                            
                                            var level1IndexPoints = SearchPoints(level2Point, "8", trails);
                                            
                                            foreach (var level1IndexPoint in level1IndexPoints)
                                            {
                                                var level1Point = new Point
                                                {
                                                    X = level1IndexPoint.Key.ToString(),
                                                    Y = level1IndexPoint.Value.ToString()
                                                };
                                            
                                                var point1ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                                     + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                                     + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                                     + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                                     + level5Point+ "->" +trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)]
                                                                     + level4Point+ "->" +trails[int.Parse(level4Point.X)][int.Parse(level4Point.Y)]
                                                                     + level3Point+ "->" +trails[int.Parse(level3Point.X)][int.Parse(level3Point.Y)]
                                                                     + level2Point+ "->" +trails[int.Parse(level2Point.X)][int.Parse(level2Point.Y)]
                                                                     + level1Point+ "->" +trails[int.Parse(level1Point.X)][int.Parse(level1Point.Y)];
                                                Console.WriteLine($"Level 01 log: {point1ValueLog}");
                                                
                                                var level0IndexPoints = SearchPoints(level1Point, "9", trails);

                                                foreach (var level0IndexPoint in level0IndexPoints)
                                                {
                                                    var level0Point = new Point
                                                    {
                                                        X = level0IndexPoint.Key.ToString(),
                                                        Y = level0IndexPoint.Value.ToString()
                                                    };
                                                    var point0ValueLog = level9Point+ "->" +trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                                         + level8Point+ "->" +trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                                         + level7Point+ "->" +trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                                         + level6Point+ "->" +trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                                         + level5Point+ "->" +trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)]
                                                                         +level4Point+ "->" + trails[int.Parse(level4Point.X)][int.Parse(level4Point.Y)]
                                                                         +level3Point+ "->" + trails[int.Parse(level3Point.X)][int.Parse(level3Point.Y)]
                                                                         + level2Point+ "->" +trails[int.Parse(level2Point.X)][int.Parse(level2Point.Y)]
                                                                         +level1Point+ "->" + trails[int.Parse(level1Point.X)][int.Parse(level1Point.Y)]
                                                                         +level0Point+ "->" + trails[int.Parse(level0Point.X)][int.Parse(level0Point.Y)];
                                                    Console.WriteLine($"Level 0 log: {point0ValueLog}");

                                                    var point0Value = trails[int.Parse(level9Point.X)][int.Parse(level9Point.Y)] + "" 
                                                                         + trails[int.Parse(level8Point.X)][int.Parse(level8Point.Y)]
                                                                         + trails[int.Parse(level7Point.X)][int.Parse(level7Point.Y)]
                                                                         + trails[int.Parse(level6Point.X)][int.Parse(level6Point.Y)]
                                                                         + trails[int.Parse(level5Point.X)][int.Parse(level5Point.Y)]
                                                                         + trails[int.Parse(level4Point.X)][int.Parse(level4Point.Y)]
                                                                         + trails[int.Parse(level3Point.X)][int.Parse(level3Point.Y)]
                                                                         + trails[int.Parse(level2Point.X)][int.Parse(level2Point.Y)]
                                                                         + trails[int.Parse(level1Point.X)][int.Parse(level1Point.Y)]
                                                                         + trails[int.Parse(level0Point.X)][int.Parse(level0Point.Y)];
                                                    Console.WriteLine($"Point value: {point0Value}");
                                                    
                                                    // Part 01
                                                    // var trailKey = level0Point.ToString() + level9Point.ToString();
                                                    //
                                                    // paths.TryAdd(trailKey, point0ValueLog);
                                                    
                                                    // Part 02
                                                    // if (point0Value == "0123456789")
                                                    // {
                                                    //     paths.TryAdd(point0ValueLog, point0Value);
                                                    // }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (var path in paths)
            {
                Console.WriteLine(path);
            }
            Console.WriteLine($"trailheads score: {paths.Count}");
        }

        static List<KeyValuePair<int, int>> SearchPoints(Point currentPoint, string searchPoint, string[] trails)
        {
            var nextHillPoints = new List<KeyValuePair<int, int>>();
            
            int rowLengthIndex = trails.Length - 1;
            int colLengthIndex = trails[0].Length - 1;
            
            int pointX = int.Parse(currentPoint.X);
            int pointY = int.Parse(currentPoint.Y);
            
            // top point
            if (pointX - 1 >= 0)
            {
                if (trails[pointX - 1][pointY] == searchPoint[0])
                {
                    nextHillPoints.Add(new KeyValuePair<int, int>(pointX - 1, pointY));
                }
            }
            // bottom point
            if (pointX + 1 <= rowLengthIndex)
            {
                if (trails[pointX + 1][pointY] == searchPoint[0])
                {
                    nextHillPoints.Add(new KeyValuePair<int, int>(pointX + 1, pointY));
                }
            }
            // left point 
            if (pointY - 1 >= 0)
            {
                if (trails[pointX][pointY - 1] == searchPoint[0])
                {
                    nextHillPoints.Add(new KeyValuePair<int, int>(pointX, pointY - 1));
                }
            }
            // right point
            if (pointY + 1 <= colLengthIndex)
            {
                if (trails[pointX][pointY + 1] == searchPoint[0])
                {
                    nextHillPoints.Add(new KeyValuePair<int, int>(pointX, pointY + 1));
                }
            }
            
            return nextHillPoints;
        }
    }

    public class Point
    {
        public string X { get; set; }
        public string Y { get; set; }

        public override string ToString()
        {
            return $"[{X}-{Y}]";
        }
    }
}