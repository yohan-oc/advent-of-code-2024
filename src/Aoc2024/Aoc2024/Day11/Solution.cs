using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Aoc2024.Day11
{
    public static class Solution
    {
        public static void Part01()
        {
            Console.WriteLine("**** Day 11 - Part 01 ****");

            string numbersString = File.ReadAllLines("Day11/input.txt")[0];

            var numbers = numbersString.Split(" ").Select(Int64.Parse).ToList();

            var orderedStones = new Dictionary<Int64, Stone>();

            Int64 index = 0;
            foreach (var number in numbers)
            {
                orderedStones.Add(index++, new Stone { EngravedValue = number, RuleType = RuleType.None });
            }

           
            int numberOfBlinks = 75;
            
            index = 0;
            for (int blink = 0; blink < numberOfBlinks; blink++)
            {
                var finalStones = new SortedList<Int64, Stone>();

                foreach (var stone in orderedStones)
                {
                    var newStones = GetNewStonesByRules(stone.Value.EngravedValue);

                    foreach (var newStone in newStones)
                    {
                        finalStones.Add(index++, newStone);
                    }
                }
                
                orderedStones = new Dictionary<Int64, Stone>(finalStones);

                //PrintStones(orderedStones);
                Console.WriteLine($"Counting blink {blink}");
            }
            
            Console.WriteLine($"Blinks count: {numberOfBlinks} -> Stone count: {orderedStones.Count}");
        }

        static void PrintStones(SortedList<int, Stone> stones)
        {
            foreach (var stone in stones)
            {
                Console.Write(stone.Value.EngravedValue + " ");
            }
            Console.WriteLine();
        }

        static List<Stone> GetNewStonesByRules(Int64 number)
        {
            var results = new List<Stone>();
            string numberString = number.ToString();

            // If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
            if (numberString.Length == 1 && number == 0)
            {
                results.Add(new Stone{ EngravedValue = 1, RuleType = RuleType.Replace});

                return results;
            }
            // If the stone is engraved with a number that has an even number of digits, it is replaced by two stones.
            // The left half of the digits are engraved on the new left stone,
            // and the right half of the digits are engraved on the new right stone.
            // (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
            if (numberString.Length % 2 == 0)
            {
                int mid = numberString.Length / 2;

                string left = numberString.Substring(0, mid);
                string right = numberString.Substring(mid);

                results.Add(new Stone{ EngravedValue = Int64.Parse(left), RuleType = RuleType.Replace});
                results.Add(new Stone{ EngravedValue = Int64.Parse(right), RuleType = RuleType.Replace});

                return results;
            }
            
            // If none of the other rules apply, the stone is replaced by a new stone;
            // the old stone's number multiplied by 2024 is engraved on the new stone.

            var newValue = number * 2024;
            
            results.Add(new Stone{ EngravedValue = newValue, RuleType = RuleType.Replace});

            return results;
        }
    }

    public class Stone
    {
        public Int64 EngravedValue { get; set; }

        public RuleType RuleType { get; set; }
    }
    
    public enum RuleType
    {
        None,
        Keep,
        Replace
    }
    
}