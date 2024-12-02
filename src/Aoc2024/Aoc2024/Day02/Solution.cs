namespace Aoc2024.Day02
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day02/input.txt");

            var reportResult = new List<string>();

            foreach (var line in lines)
            {
                var numbers = line.Split(" ").ToList();

                var levels = new Dictionary<int, int>();

                bool isSafe = true;

                var firstNumber = Convert.ToInt32(numbers[0]);
                var secondNumber = Convert.ToInt32(numbers[1]);

                var firstLevelDifference = firstNumber - secondNumber;

                if(firstLevelDifference == 0)
                {
                    isSafe = false;
                }
                else if(firstLevelDifference > 0 && firstLevelDifference <= 3)
                {
                    var previousLevel = firstLevelDifference;

                    for (int i = 1; i < numbers.Count - 1; i++)
                    {
                        var currentNumber = Convert.ToInt32(numbers[i]);
                        var nextNumber = Convert.ToInt32(numbers[i + 1]);
                        
                        var currentLevel = currentNumber-nextNumber;

                        if(currentLevel > 0 && currentLevel <= 3)
                        {
                            previousLevel = currentLevel;
                        }
                        else
                        {
                            isSafe = false;
                            break;
                        }
                    }
                }
                else if(firstLevelDifference < 0 && firstLevelDifference >= -3)
                {
                    var previousLevel = firstLevelDifference;

                    for (int i = 1; i < numbers.Count - 1; i++)
                    {
                        var currentNumber = Convert.ToInt32(numbers[i]);
                        var nextNumber = Convert.ToInt32(numbers[i + 1]);
                        
                        var currentLevel = currentNumber-nextNumber;

                        if(currentLevel < 0 && currentLevel >= -3)
                        {
                            previousLevel = currentLevel;
                        }
                        else
                        {
                            isSafe = false;
                            break;
                        }
                    }
                }
                else
                {
                    isSafe = false;
                }

                if(isSafe)
                {
                    reportResult.Add("Safe");
                }
                else
                {
                    reportResult.Add("Unsafe");
                }
            }

            Console.WriteLine("****************");
            Console.WriteLine("Number of Safe reports: " + reportResult.Where(c => c == "Safe").Count());
            Console.WriteLine("Number of Unsafe reports: " + reportResult.Where(c => c == "Unsafe").Count());
        }

        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day02/testData.txt");

            var reportResult = new List<string>();

            foreach (var line in lines)
            {
                var numbers = line.Split(" ").ToList();

                var levels = new Dictionary<int, int>();

                bool isSafe = true;

                var firstNumber = Convert.ToInt32(numbers[0]);
                var secondNumber = Convert.ToInt32(numbers[1]);

                var firstLevelDifference = firstNumber - secondNumber;

                if(firstLevelDifference == 0)
                {
                    isSafe = false;
                }
                else if(firstLevelDifference > 0 && firstLevelDifference <= 3)
                {
                    var previousLevel = firstLevelDifference;

                    for (int i = 1; i < numbers.Count - 1; i++)
                    {
                        var currentNumber = Convert.ToInt32(numbers[i]);
                        var nextNumber = Convert.ToInt32(numbers[i + 1]);
                        
                        var currentLevel = currentNumber-nextNumber;

                        if(currentLevel > 0 && currentLevel <= 3)
                        {
                            previousLevel = currentLevel;
                        }
                        else
                        {
                            isSafe = false;
                            break;
                        }
                    }
                }
                else if(firstLevelDifference < 0 && firstLevelDifference >= -3)
                {
                    var previousLevel = firstLevelDifference;

                    for (int i = 1; i < numbers.Count - 1; i++)
                    {
                        var currentNumber = Convert.ToInt32(numbers[i]);
                        var nextNumber = Convert.ToInt32(numbers[i + 1]);
                        
                        var currentLevel = currentNumber-nextNumber;

                        if(currentLevel < 0 && currentLevel >= -3)
                        {
                            previousLevel = currentLevel;
                        }
                        else
                        {
                            isSafe = false;
                            break;
                        }
                    }
                }
                else
                {
                    isSafe = false;
                }

                if(isSafe)
                {
                    reportResult.Add("Safe");
                }
                else
                {
                    reportResult.Add("Unsafe");
                }
            }

            foreach (var report in reportResult)
            {
                Console.WriteLine(report);
            }
            Console.WriteLine("****************");
            Console.WriteLine("Number of Safe reports: " + reportResult.Where(c => c == "Safe").Count());
            Console.WriteLine("Number of Unsafe reports: " + reportResult.Where(c => c == "Unsafe").Count());
        }
    }
}

