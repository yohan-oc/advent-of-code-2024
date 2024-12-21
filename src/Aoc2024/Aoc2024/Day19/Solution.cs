using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2024.Day19
{
    public static class Solution
    {
        public static void Part01()
        {
            Console.WriteLine("**** Day 13 - Part 01 ****");

            string[] lines = File.ReadAllLines("Day19/input.txt");

            var towelPatterns = new HashSet<string>();

            foreach (var pattern in lines[0].Split(","))
            {
                towelPatterns.Add(pattern.Trim());
            }
            
            int possibleDesigns = 0;
            
            foreach (var line in lines.Skip(2))
            {
                if (CanConstructDesign(line, towelPatterns))
                {
                    possibleDesigns++;
                }
            }
            
            Console.WriteLine($"Total :{possibleDesigns}");
        }
        
        static bool CanConstructDesign(string design, HashSet<string> patterns)
        {
            if (string.IsNullOrEmpty(design))
                return true;

            foreach (var pattern in patterns)
            {
                if (design.StartsWith(pattern))
                {
                    var remaining = design.Substring(pattern.Length);
                    if (CanConstructDesign(remaining, patterns))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void Part02()
        {
            Console.WriteLine("**** Day 13 - Part 02 ****");

            string[] lines = File.ReadAllLines("Day19/input.txt");

            var towelPatterns = new HashSet<string>();

            foreach (var pattern in lines[0].Split(","))
            {
                towelPatterns.Add(pattern.Trim());
            }
            
            long count = 0;

            int index = 1;
            
            foreach (var line in lines.Skip(2))
            {
                var cache = new Dictionary<string, long>();
                long result = CalculateDesigns(line, towelPatterns, cache);
                count = count + result;

                Console.WriteLine($"{index} of {lines.Length}");
                index++;
            }
            
            Console.WriteLine($"Total :{count}");
        }
        
        
        static long CalculateDesigns(string design, HashSet<string> patterns, Dictionary<string, long> cache)
        {
            if (cache.ContainsKey(design))
                return cache[design];
            
            if (string.IsNullOrEmpty(design))
                return 1;

            long count = 0;

            foreach (var pattern in patterns)
            {
                if (design.StartsWith(pattern))
                {
                    var remaining = design.Substring(pattern.Length);
                    count = count + CalculateDesigns(remaining, patterns, cache);
                }
            }

            cache[design] = count;
            return count;
        }
    }
}