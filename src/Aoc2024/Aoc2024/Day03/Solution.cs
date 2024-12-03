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

                var dict = new Dictionary<string, string>();
                foreach (Match match in matches)
                {
                    dict.Add(match.Value, match.Value);
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

        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day03/input.txt");

            string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

            string doPattern = @"do\(\)";

            string dontPattern = @"don't\(\)";


            var total = 0;
            int indexBoost = 0;

            var allStatments = new List<Instruction>();
            
            foreach(var line in lines)
            {
                MatchCollection mulMatches = Regex.Matches(line, mulPattern);

                foreach (Match match in mulMatches)
                {
                    // string strip = match.Value.Replace("mul(", "").Replace(")", "");

                    // int firstNumber = Convert.ToInt32(strip.Split(",")[0]);
                    // int secondNumber = Convert.ToInt32(strip.Split(",")[1]);

                    // total = total + (firstNumber*secondNumber);  
                    // Console.WriteLine("Index: " + (indexBoost + match.Index) + " " +match.Value);

                    var modifiedIndex = indexBoost + match.Index;
                    allStatments.Add(new Instruction{Index = modifiedIndex, Statement = match.Value, Type = StatementType.Mul });
                }

                MatchCollection doMatches = Regex.Matches(line, doPattern);

                foreach (Match match in doMatches)
                {
                    var modifiedIndex = indexBoost + match.Index;
                    allStatments.Add(new Instruction{Index = modifiedIndex, Statement = match.Value, Type = StatementType.Do });
                }

                MatchCollection dontMatches = Regex.Matches(line, dontPattern);

                foreach (Match match in dontMatches)
                {
                    var modifiedIndex = indexBoost + match.Index;
                    allStatments.Add(new Instruction{Index = modifiedIndex, Statement = match.Value, Type = StatementType.Dont });
                }

                indexBoost = indexBoost + 10000;
            }

            bool enabled = true;

            foreach(var item in allStatments.OrderBy(x=> x.Index))
            {
                if(item.Type == StatementType.Dont)
                {
                    enabled = false;
                }
                else if(item.Type == StatementType.Do)
                {
                    enabled = true;
                }

                if(enabled && item.Type == StatementType.Mul)
                {
                    string strip = item.Statement.Replace("mul(", "").Replace(")", "");

                    int firstNumber = Convert.ToInt32(strip.Split(",")[0]);
                    int secondNumber = Convert.ToInt32(strip.Split(",")[1]);

                    total = total + (firstNumber*secondNumber);  
                }
                
            }

            Console.WriteLine("Total: "+ total);
        }
    }

    public class Instruction 
    {
        public StatementType Type { get; set; }  
        public int Index { get; set; }  

        public required string Statement { get; set; }
    }

    public enum StatementType
    {
        Do,
        Dont,
        Mul
    }
}

