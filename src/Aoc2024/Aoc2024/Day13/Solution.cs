using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2024.Day13
{
    public static class Solution
    {
        public static void Part01()
        {
            Console.WriteLine("**** Day 13 - Part 01 ****");
            
            const int TOKEN_COST_A = 3;
            const int TOKEN_COST_B = 1;
            
            var machines = new List<Machine>();
            
            string[] lines = File.ReadAllLines("Day13/input.txt");
            
            int fileSize = lines.Length;

            int index = 0;

            Button buttonA = null;
            Button buttonB = null;
            
            while (index < fileSize)
            {
                var line = lines[index];

                if (line.Contains("Button A: "))
                {
                    // Button A: X+94, Y+34
                    line = line.Replace("Button A:", "").Replace(" ","").Replace("X+", "").Replace("Y+", "");

                    int x = int.Parse(line.Split(",")[0]);
                    int y = int.Parse(line.Split(",")[1]);

                    buttonA = new Button(x, y);
                }
                if (line.Contains("Button B: "))
                {
                    // Button B: X+22, Y+67
                    line = line.Replace("Button B: ", "").Replace(" ","").Replace("X+", "").Replace("Y+", "");

                    int x = int.Parse(line.Split(",")[0]);
                    int y = int.Parse(line.Split(",")[1]);

                    buttonB = new Button(x, y);
                }

                if (line.Contains("Prize: "))
                {
                    // Prize: X=8400, Y=5400
                    
                    line = line.Replace("Prize: ", "").Replace(" ","").Replace("X=", "").Replace("Y=", "");
                    
                    int xPrice = int.Parse(line.Split(",")[0]);
                    int yPrice = int.Parse(line.Split(",")[1]);
                    
                    var machine = new Machine(buttonA, buttonB, xPrice, yPrice);
                    
                    machines.Add(machine);

                    buttonA = null;
                    buttonB = null;
                }
                index++;
            }

            Int64 totalTokens = 0;
            foreach (var machine in machines)
            {
                var result = CalculateDeterminent(machine.ButtonA, machine.ButtonB, machine.PrizeX, machine.PrizeY);

                if (result.Item1 != -1 && (result.Item2 != -1))
                {
                    Console.WriteLine($"ButtonA times: {result.Item1} ButtonA times: {result.Item2}");

                    Int64 tokens = TOKEN_COST_A * result.Item1 + TOKEN_COST_B * result.Item2;
                    totalTokens = totalTokens + tokens;
                }
            }
            // var result = CalculateDeterminent(buttonA, buttonB, 7870, 6450);

            Console.WriteLine($"Total tokens: {totalTokens}");
        }
        
        public static void Part02()
        {
            Console.WriteLine("**** Day 13 - Part 02 ****");
            
            const int TOKEN_COST_A = 3;
            const int TOKEN_COST_B = 1;
            
            var machines = new List<Machine>();
            
            string[] lines = File.ReadAllLines("Day13/input.txt");
            
            int fileSize = lines.Length;

            int index = 0;

            Button buttonA = null;
            Button buttonB = null;
            
            while (index < fileSize)
            {
                var line = lines[index];

                if (line.Contains("Button A: "))
                {
                    // Button A: X+94, Y+34
                    line = line.Replace("Button A:", "").Replace(" ","").Replace("X+", "").Replace("Y+", "");

                    Int64 x = Int64.Parse(line.Split(",")[0]);
                    Int64 y = Int64.Parse(line.Split(",")[1]);

                    buttonA = new Button(x, y);
                }
                if (line.Contains("Button B: "))
                {
                    // Button B: X+22, Y+67
                    line = line.Replace("Button B: ", "").Replace(" ","").Replace("X+", "").Replace("Y+", "");

                    Int64 x = Int64.Parse(line.Split(",")[0]);
                    Int64 y = Int64.Parse(line.Split(",")[1]);

                    buttonB = new Button(x, y);
                }

                if (line.Contains("Prize: "))
                {
                    // Prize: X=8400, Y=5400
                    
                    line = line.Replace("Prize: ", "").Replace(" ","").Replace("X=", "").Replace("Y=", "");
                    
                    Int64 xPrice = Int64.Parse(line.Split(",")[0]) + 10000000000000;
                    Int64 yPrice = Int64.Parse(line.Split(",")[1]) + 10000000000000;
                    
                    var machine = new Machine(buttonA, buttonB, xPrice, yPrice);
                    
                    machines.Add(machine);

                    buttonA = null;
                    buttonB = null;
                }
                index++;
            }

            Int64 totalTokens = 0;
            foreach (var machine in machines)
            {
                var result = CalculateDeterminent(machine.ButtonA, machine.ButtonB, machine.PrizeX, machine.PrizeY);

                if (result.Item1 != -1 && (result.Item2 != -1))
                {
                    //Console.WriteLine($"ButtonA: {machine.ButtonA.XValue}-{machine.ButtonA.YValue} ButtonB : {machine.ButtonB.XValue}-{machine.ButtonB.YValue}");
                    //Console.WriteLine($"ButtonA times: {result.Item1} ButtonA times: {result.Item2}");

                    var tokens = TOKEN_COST_A * result.Item1 + TOKEN_COST_B * result.Item2;
                    totalTokens = totalTokens + tokens;
                }
            }
            // var result = CalculateDeterminent(buttonA, buttonB, 7870, 6450);

            Console.WriteLine($"Total tokens: {totalTokens}");
        }

        static (Int64, Int64) CalculateDeterminent(Button buttonA, Button buttonB, Int64 X, Int64 Y)
        {
            Int64 a1 = buttonA.XValue, a2 = buttonA.YValue;
            
            Int64 b1 = buttonB.XValue, b2 = buttonB.YValue;
            
            Int64 c1 = X, c2 = Y;
            
            Int64 determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                return (-1, -1);
            }
            
            Int64 determinantX = c1 * b2 - c2 * b1;
            Int64 determinantY = a1 * c2 - a2 * c1;
            
            if (determinantX % determinant != 0 || determinantY % determinant != 0)
            {
                return (-1, -1);
            }
            
            Int64 a = determinantX / determinant;
            Int64 b = determinantY / determinant;

            return (a, b);
        }
    }

    public class Machine
    {
        public Machine(Button buttonA, Button buttonB, Int64 x, Int64 y)
        {
            ButtonA = buttonA;
            ButtonB = buttonB;
            PrizeX = x;
            PrizeY = y;
        }
        public Button ButtonA { get; set; }
        
        public Button ButtonB { get; set; }
        
        public Int64 PrizeX { get; set; }
        
        public Int64 PrizeY { get; set; }
    }
    public class Button
    {
        public Int64 XValue { get; set; }

        public Int64 YValue { get; set; }

        public Button(Int64 xValue, Int64 yValue)
        {
            XValue = xValue;
            YValue = yValue;
            
        }
    }
    
}