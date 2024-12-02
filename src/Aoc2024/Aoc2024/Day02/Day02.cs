namespace Aoc2024.Day02
{
    public static class Day02
    {
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day02/input.txt");

            var reportResult = new List<string>();

            foreach (var line in lines)
            {
                var numbers = line.Split(" ").ToList();

                var levels = new Dictionary<int, int>();

                for (int i = 0; i < numbers.Count - 1; i++)
                {
                    var currentNumber = Convert.ToInt32(numbers[i]);
                    var nextNumber = Convert.ToInt32(numbers[i + 1]);

                    var level = CompareLevels(currentNumber, nextNumber);
                    levels.TryAdd(level, level);
                }

                if (levels.Count() == 1 && levels.First().Key != 0)
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
            Console.WriteLine("Number of Safe reports: " + reportResult.Where(c => c == "Unsafe").Count());
        }

        static int CompareLevels(int number1, int number2)
        {
            var result = number1 - number2;

            if (result == 0)
            {
                return 0;
            }
            if (result < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}

