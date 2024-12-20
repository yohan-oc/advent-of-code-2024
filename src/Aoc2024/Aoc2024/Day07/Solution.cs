﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Aoc2024.Day07
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day07/input.txt");

            Int64 rowLengthIndex = lines.Length - 1;
            Int64 colLengthIndex = lines[0].Length - 1;
            
            var validCaliberations = new Dictionary<string, Int64>();

            foreach (var line in lines)
            {
                string[] values = line.Split(": ");
                
                Int64 expectedResult = Int64.Parse(values[0]);
                
                var numbers = values[1].Split(" ").Select(Int64.Parse).ToArray();

                var numberOfOperators = numbers.Length - 1;
                var totalCombinations = (int)Math.Pow(2, numbers.Length - 1);
                
                var operatorCombinations = new List<string>();

                for (int i = 0; i < totalCombinations; i++)
                {
                    string binary = Convert.ToString(i, 2).PadLeft(numberOfOperators, '0');
                    string combination = binary.Replace('0', '+').Replace('1', '*');
                    
                    operatorCombinations.Add(combination);
                }

                //var allExpressions = new List<string>();
                
                foreach (var combination in operatorCombinations)
                {
                    var operatorExpression = string.Join(" x ", numbers);
                    int operatorIndex = 0;
                    for(int i = 0; i < operatorExpression.Length - 1; i++)
                    {
                        if (operatorExpression[i] == 'x')
                        {
                            char[] additionChars = operatorExpression.ToCharArray();
                            
                            additionChars[i] = combination[operatorIndex];
                            operatorExpression = new string(additionChars);
                            operatorIndex++;
                        }
                    }
                    //allExpressions.Add(operatorExpression);
                    
                    // foreach (var expression in allExpressions)
                    // {
                    var actualValue = EvaluateLeftToRight(operatorExpression);

                    if (expectedResult == actualValue)
                    {
                        validCaliberations.TryAdd(line, expectedResult);
                    }
                    //}
                }
            }

            // foreach (var caliberation in validCaliberations)
            // {
            //     Console.WriteLine(caliberation);
            // }
            Console.WriteLine("Count: " + validCaliberations.Count);
            Console.WriteLine("Total: " + validCaliberations.Sum(c => c.Value));

            Int64 total = 0;

            foreach (var item in validCaliberations)
            {
                total = total + item.Value;
            }
            
            Console.WriteLine("Cal: " + total);
        }
        
        static Int64 EvaluateLeftToRight(string expression)
        {
            string[] values = expression.Split(' ');
            
            Int64 result = Int64.Parse(values[0]);
            
            for (Int64 i = 1; i < values.Length; i += 2)
            {
                string op = values[i];        
                Int64 nextValue = Int64.Parse(values[i + 1]);

                switch (op)
                {
                    case "+":
                        result = result +  nextValue;
                        break;
                    case "*":
                        result = result * nextValue;
                        break;
                    default:
                        throw new Exception($"Invalid operator" + op);
                }
            }

            return result;
        }
        
        
        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day07/testData.txt");

            Int64 rowLengthIndex = lines.Length - 1;
            Int64 colLengthIndex = lines[0].Length - 1;
            
            var validCalibrations = new Dictionary<string, Int64>();

            foreach (var line in lines)
            {
                string[] values = line.Split(": ");
                
                Int64 expectedResult = Int64.Parse(values[0]);
                
                var numbers = values[1].Split(" ").Select(Int64.Parse).ToArray();

                var numberOfOperators = numbers.Length - 1;
                var totalCombinations = (int)Math.Pow(2, numbers.Length - 1);
                
                var operatorCombinations = new List<string>();

                for (int i = 0; i < totalCombinations; i++)
                {
                    string binary = Convert.ToString(i, 2).PadLeft(numberOfOperators, '0');
                    string combination = binary.Replace('0', '+').Replace('1', '*');
                    
                    operatorCombinations.Add(combination);
                }
                
                foreach (var combination in operatorCombinations)
                {
                    var operatorExpression = string.Join(" x ", numbers);
                    int operatorIndex = 0;
                    for(int i = 0; i < operatorExpression.Length - 1; i++)
                    {
                        if (operatorExpression[i] == 'x')
                        {
                            char[] additionChars = operatorExpression.ToCharArray();
                            
                            additionChars[i] = combination[operatorIndex];
                            operatorExpression = new string(additionChars);
                            operatorIndex++;
                        }
                    }
                    var actualValue = EvaluateLeftToRight(operatorExpression);

                    if (expectedResult == actualValue)
                    {
                        validCalibrations.TryAdd(line, expectedResult);
                    }
                }
            }

            // logic starts here
            
            var invalidCalibrations = new Dictionary<string, Int64>();
            
            var reEvaluatedCalibrations = new Dictionary<string, Int64>();

            foreach (var line in lines)
            {
                if (!validCalibrations.ContainsKey(line))
                {
                    string[] values = line.Split(": ");
                
                    Int64 expectedResult = Int64.Parse(values[0]);
                    invalidCalibrations.TryAdd(line, expectedResult);
                }
            }
            
            foreach (var calibration in invalidCalibrations)
            {
                string[] values = calibration.Key.Split(": ");
                
                Int64 expectedResult = Int64.Parse(values[0]);
                
                var numbers = values[1].Split(" ").Select(Int64.Parse).ToArray();

                var numberOfOperators = numbers.Length - 1;
                var totalCombinations = (int)Math.Pow(2, numbers.Length - 1);
                
                // var results = GenerateAllOutputs(values[1].Replace(" ", ""));
                //
                // foreach (var result in results)
                // {
                //     Console.WriteLine(result);
                // }
                // Console.WriteLine("-------------------");
                // if (results.Count(c => c == expectedResult) == 1)
                // {
                //     reEvaluatedCalibrations.Add(calibration.Key, calibration.Value);
                // }
            }
            
            Console.WriteLine("Count: " + reEvaluatedCalibrations.Count);
            Console.WriteLine("Total: " + reEvaluatedCalibrations.Sum(c => c.Value));

            Int64 total = 0;

            foreach (var item in reEvaluatedCalibrations)
            {
                total = total + item.Value;
            }
            
            Console.WriteLine("Cal: " + total);
        }

        public static void TestPart02()
        {
            int[] numbers = new int[] { 17, 8, 14 };

            var results = BuildConcatenations(numbers);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            //Console.WriteLine(result);
        }

        // static List<string> GetConcatenations(int[] numbers)
        // {
        //     List<string> results = new List<string>();
        //
        //     foreach (var number in numbers)
        //     {
        //         var comb1 = string.Join(" ", numbers);
        //         
        //         var comb2 = 
        //     }
        //     return results;
        // }
        
        static List<string> BuildConcatenations(int[] numbers)
        {
            var results = new List<string>();
            
            if (numbers.Length == 2)
            {
                var baseResult = new List<string>();
                
                Console.WriteLine("Base result: " + string.Join("", numbers));
                Console.WriteLine("Base result: " + EvaluateLeftToRight(string.Join(" + ", numbers)));
                Console.WriteLine("Base result: " + EvaluateLeftToRight(string.Join(" * ", numbers)));
                
                baseResult.Add(string.Join("", numbers));
                baseResult.Add(EvaluateLeftToRight(string.Join(" + ", numbers)).ToString());
                baseResult.Add(EvaluateLeftToRight(string.Join(" * ", numbers)).ToString());
                
                return baseResult;
            }
            else
            {
                for (int i = 0; i< numbers.Length - 1 ; i++)
                {
                    var newArray = numbers.Skip(i).ToArray();
                    var returnResult = BuildConcatenations(newArray);

                    results.AddRange(returnResult);
                    foreach (var result in returnResult)
                    {
                        Console.WriteLine("Return result: " + $"{numbers[0]}{result}");
                        Console.WriteLine("Return result: " + EvaluateLeftToRight($"{numbers[0]} + {result}").ToString());
                        Console.WriteLine("Return result: " + EvaluateLeftToRight($"{numbers[0]} * {result}").ToString());
                    
                        results.Add($"{numbers[0]}{result}");
                        results.Add(EvaluateLeftToRight($"{numbers[0]} + {result}").ToString());
                        results.Add(EvaluateLeftToRight($"{numbers[0]} * {result}").ToString());
                    }
                }
                
                
            }
            return results;
        }
    }
}