using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Aoc2024.Day05
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] rules = File.ReadAllLines("Day05/rules.txt");

            string[] updates = File.ReadAllLines("Day05/updates.txt");
            
            // var rulesDictionary = new Dictionary<int, List<int>>();

            var xys = new List<KeyValuePair<int, int>>();

            foreach (string rule in rules)
            {
                var xy = rule.Split("|");

                var x = Convert.ToInt32(xy[0]);
                var y = Convert.ToInt32(xy[1]);

                xys.Add(new KeyValuePair<int, int>(x,y));
            }

            // foreach (var xy in xys)
            // {
            //     if(!rulesDictionary.ContainsKey(xy.Key))
            //     {
            //         rulesDictionary.Add(xy.Key, xys.Where(z => z.Key == xy.Key).Select(c=>c.Value).ToList());
            //     }
            // }

            // foreach(var dict in rulesDictionary)
            // {
            //     Console.WriteLine("Key: " + dict.Key + " Values: " + string.Join(", ", dict.Value));
            // }

            var validUpdates = new List<string>();

            foreach(var update in updates)
            {
                var numbers = update.Split(",").Select(int.Parse).ToList();

                bool hasRuleBreak = false;
                foreach (var rule in xys)
                {
                    if(numbers.Count(x=> x == rule.Key) > 0 && numbers.Count(x=> x == rule.Value) > 0)
                    {
                        int xIndex = numbers.IndexOf(rule.Key);
                        int yIndex = numbers.IndexOf(rule.Value);

                        if(xIndex > yIndex)
                        {
                            hasRuleBreak = true;
                            break;
                        }
                    }
                }

                if (!hasRuleBreak)
                {
                    validUpdates.Add(update);
                }
            }

            var total = 0;

            foreach(var update in validUpdates)
            {
                var numbers = update.Split(",").Select(int.Parse).ToList();

                int midIndex = (numbers.Count - 1) / 2;

                total = total + numbers[midIndex];
            }

            Console.WriteLine("Total: " + total);
        }

        

        public static void Part02()
        {
            string[] rules = File.ReadAllLines("Day05/rules.txt");

            string[] updates = File.ReadAllLines("Day05/updates.txt");

            var xys = new List<KeyValuePair<int, int>>();

            foreach (string rule in rules)
            {
                var xy = rule.Split("|");

                var x = Convert.ToInt32(xy[0]);
                var y = Convert.ToInt32(xy[1]);

                xys.Add(new KeyValuePair<int, int>(x,y));
            }

            var canBeUpdated = new List<string>();

            foreach(var update in updates)
            {
                var numbers = update.Split(",").Select(int.Parse).ToList();

                bool hasRuleBreak = false;

                var brokenRules = new List<KeyValuePair<int, int>>();

                var appliedRules = new List<KeyValuePair<int, int>>();

                foreach (var rule in xys)
                {
                    if(numbers.Count(x=> x == rule.Key) > 0 && numbers.Count(x=> x == rule.Value) > 0)
                    {
                        appliedRules.Add(new KeyValuePair<int, int>(rule.Key, rule.Value));

                        int xIndex = numbers.IndexOf(rule.Key);
                        int yIndex = numbers.IndexOf(rule.Value);

                        if(xIndex > yIndex)
                        {
                            hasRuleBreak = true;
                            
                            brokenRules.Add(new KeyValuePair<int, int>(rule.Key, rule.Value));
                        }
                    }
                }

                if (hasRuleBreak)
                {
                    //var newUpdate = update;
                    //brokenRules.Reverse();
                    // Console.WriteLine(appliedRules);
                    // foreach (var rule in brokenRules)
                    // {
                    //     newUpdate = newUpdate.Replace(rule.Key.ToString(), "x");
                    //     newUpdate = newUpdate.Replace(rule.Value.ToString(), "y");

                    //     newUpdate = newUpdate.Replace("y", rule.Key.ToString());
                    //     newUpdate = newUpdate.Replace("x", rule.Value.ToString());
                    // }
                    
                    //canBeUpdated.Add(newUpdate);
                    var newUpdate = UpdatedRules(appliedRules);
                    canBeUpdated.Add(newUpdate);
                    Console.WriteLine(newUpdate);
                }
            }

            var total = 0;

            foreach(var update in canBeUpdated)
            {
                var numbers = update.Split(",").Select(int.Parse).ToList();

                int midIndex = (numbers.Count - 1) / 2;

                total = total + numbers[midIndex];
            }

            Console.WriteLine("Total: " + total);
        }

        static string UpdatedRules(List<KeyValuePair<int, int>> rules)
        {
            var rulesDictionary = new Dictionary<int, List<int>>();

            foreach (var xy in rules)
            {
                if(!rulesDictionary.ContainsKey(xy.Key))
                {
                    rulesDictionary.Add(xy.Key, rules.Where(z => z.Key == xy.Key).Select(c=>c.Value).ToList());
                }
            }

            var orderedRules = rulesDictionary.OrderByDescending(z=>z.Value.Count);

            var updatedOrder = "";

            foreach(var dict in orderedRules)
            {
                updatedOrder =  updatedOrder + dict.Key.ToString() + ",";
            }

            updatedOrder = updatedOrder + orderedRules.Last().Value.First();

            return updatedOrder;
        }
    }
}

