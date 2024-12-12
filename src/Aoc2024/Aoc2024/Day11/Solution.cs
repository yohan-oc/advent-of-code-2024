using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
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
        
        public static void Part02()
        {
            Console.WriteLine("**** Day 11 - Part 01 ****");
            
            string numbersString = File.ReadAllLines("Day11/input.txt")[0];
            
            int numberOfBlinks = 35;
        
            var numbers = numbersString.Split(" ").Select(Int64.Parse).ToList();

            var orderedStones = new List<Int64>();

            Int64 index = 0;
            foreach (var number in numbers)
            {
                orderedStones.Add(number);
            }

            orderedStones = new List<Int64>(GetStones(orderedStones, numberOfBlinks));

            var level1numberOfStones = orderedStones.Count;

            var level1Size = 10;

            int chunkSize = level1numberOfStones / level1Size;
            
            var level1stoneSet1 = new List<Int64>(orderedStones.Take(chunkSize).ToList());
            
            var level1stoneSet2 = new List<Int64>(orderedStones.Skip(chunkSize).Take(chunkSize).ToList());
            
            var level1stoneSet3 = new List<Int64>(orderedStones.Skip(chunkSize).Skip(chunkSize).Take(chunkSize).ToList());
            
            var level1stoneSet4 = new List<Int64>(orderedStones.Skip(chunkSize).Skip(chunkSize).Skip(chunkSize).Take(chunkSize).ToList());
            
            var level1stoneSet5 = new List<Int64>(orderedStones.Skip(chunkSize).Skip(chunkSize).Skip(chunkSize).Skip(chunkSize).ToList());

            var level1newStones1 = new List<Int64>(GetStones(level1stoneSet1, level1Size));
            
            var level1newStones2 = new List<Int64>(GetStones(level1stoneSet2, level1Size));
            
            var level1newStones3 = new List<Int64>(GetStones(level1stoneSet3, level1Size));
            
            var level1newStones4 = new List<Int64>(GetStones(level1stoneSet4, level1Size));
            
            var level1newStones5 = new List<Int64>(GetStones(level1stoneSet5, level1Size));
            
            var totalCount = level1newStones1.Count + level1newStones2.Count + 
                             level1newStones3.Count + level1newStones4.Count + + level1newStones5.Count;
            
            
            
            Console.WriteLine($"Blinks count: {numberOfBlinks + level1Size} -> Stone count: {totalCount}");
        }

        static List<Int64> GetStones(List<Int64> stones, int blinkSize)
        {
            var orderedStones = new List<Int64>(stones);
            Int64 index = 0;
            
            for (int blink = 0; blink < blinkSize; blink++)
            {
                var finalStones = new List<Int64>();

                foreach (var stone in orderedStones)
                {
                    var newStones = GetNewStonesByRules(stone);

                    foreach (var newStone in newStones)
                    {
                        finalStones.Add(newStone.EngravedValue);
                    }
                }
                
                orderedStones = new List<Int64>(finalStones);

                //PrintStones(orderedStones);
                Console.WriteLine($"Counting blink {blink}");
            }

            return orderedStones;
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