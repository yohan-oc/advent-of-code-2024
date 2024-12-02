using System;
namespace Aoc2024.Day01
{
	public static class Solution
	{
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day01/input.txt");

            var reportResult = new List<string>();

            var leftSideNumbers = new List<int>();
            var rightSideNumbers = new List<int>();

            foreach (var line in lines)
            {
                var numbers = line.Split("   ").ToList();

                var firstNumber = Convert.ToInt32(numbers[0]);
                var secondNumber = Convert.ToInt32(numbers[1]);

                leftSideNumbers.Add(firstNumber);
                rightSideNumbers.Add(secondNumber);
            }

            leftSideNumbers = leftSideNumbers.OrderBy(c => c).ToList();
            rightSideNumbers = rightSideNumbers.OrderBy(c => c).ToList();

            var totaltDistance = 0;

            for (int i = 0; i < leftSideNumbers.Count; i++)
            {
                var distance = Math.Abs(leftSideNumbers[i] - rightSideNumbers[i]);
                Console.WriteLine(distance);
            }

            Console.WriteLine("Total distance: " + totaltDistance);

        }

        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day01/input.txt");

            var reportResult = new List<string>();

            var leftSideNumbers = new List<int>();
            var rightSideNumbers = new List<int>();

            foreach (var line in lines)
            {
                var numbers = line.Split("   ").ToList();

                var firstNumber = Convert.ToInt32(numbers[0]);
                var secondNumber = Convert.ToInt32(numbers[1]);

                leftSideNumbers.Add(firstNumber);
                rightSideNumbers.Add(secondNumber);
            }

            var totaltDistance = 0;

            for (int i = 0; i < leftSideNumbers.Count; i++)
            {
                var distance = leftSideNumbers[i] * rightSideNumbers.Where(c=>c == leftSideNumbers[i]).Count();
                
                totaltDistance = totaltDistance + distance;

            }

            Console.WriteLine("Total distance: " + totaltDistance);

        }
    }
}

