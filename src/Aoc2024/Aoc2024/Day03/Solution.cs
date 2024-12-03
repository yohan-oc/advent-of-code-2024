using System.Text.RegularExpressions;

namespace Aoc2024.Day03
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day03/input.txt");

            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

            var total = 0;
            int index = 1;

            foreach(var line in lines)
            {
                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    string strip = match.Value.Replace("mul(", "").Replace(")", "");

                    int firstNumber = Convert.ToInt32(strip.Split(",")[0]);
                    int secondNumber = Convert.ToInt32(strip.Split(",")[1]);

                    total = total + (firstNumber*secondNumber);  
                    Console.WriteLine("Index: " + index + " " +match.Value);
                    index++;
                }
            }
            

            Console.WriteLine("Total: "+ total);
        }

    }
}

