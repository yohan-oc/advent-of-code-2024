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
            
            Console.WriteLine($"Total tokens:{possibleDesigns}");
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
        }
    }
}