namespace Aoc2024.Day02
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] lines = File.ReadAllLines("Day02/input.txt");

            var reportResult = GenerateReport(lines);

            Console.WriteLine("****************");
            Console.WriteLine("Number of Safe reports: " + reportResult.Where(c => c.IsSafe).Count());
            Console.WriteLine("Number of Unsafe reports: " + reportResult.Where(c => !c.IsSafe).Count());
        }

        static List<Report> GenerateReport(string[] lines)
        {
            var reportResult = new List<Report>();

            foreach (var line in lines)
            {
                var numbers = line.Split(" ").ToList();

                bool isSafe = true;

                var firstNumber = Convert.ToInt32(numbers[0]);
                var secondNumber = Convert.ToInt32(numbers[1]);

                var firstLevelDifference = firstNumber - secondNumber;

                if(firstLevelDifference == 0)
                {
                    isSafe = false;
                    reportResult.Add(new Report{ Line = line, IsSafe = false});
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
                            reportResult.Add(new Report{ Line = line, IsSafe = false});
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
                            reportResult.Add(new Report{ Line = line, IsSafe = false});
                            break;
                        }
                    }
                }
                else
                {
                    isSafe = false;
                    reportResult.Add(new Report{ Line = line, IsSafe = false});
                }

                if(isSafe)
                {
                    //reportResult.Add("Safe");
                    reportResult.Add(new Report{ Line = line, IsSafe = true});
                }
                // else
                // {
                //     //reportResult.Add("Unsafe");
                //     reportResult.Add(new Report{ Line = line, IsSafe = false});
                // }
            }

            return reportResult;
        }

        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day02/input.txt");

            var reportResult = GenerateReport(lines);

            Console.WriteLine("****************");
            Console.WriteLine("Number of Safe reports: " + reportResult.Where(c => c.IsSafe).Count());
            Console.WriteLine("Number of Unsafe reports: " + reportResult.Where(c => !c.IsSafe).Count());

            string[] unSafeLines = new string[reportResult.Where(c => !c.IsSafe).Count()];

            int index = 0;
            foreach(var report in reportResult.Where(c => !c.IsSafe).ToList())
            {
                unSafeLines[index] = report.Line;
                index++;
            }

            var reportResultLevel2 = GenerateReportLevel2(unSafeLines);

            Console.WriteLine("****************");
            Console.WriteLine("Number of Safe re-evaluate reports: " + reportResultLevel2.Where(c => c.IsSafe).Count());

            var total = reportResultLevel2.Where(c => c.IsSafe).Count() 
            + reportResult.Where(c => c.IsSafe).Count();
            Console.WriteLine("****************");
            Console.WriteLine("Total number of Safe reports: " + total);
        }

        static List<Report> GenerateReportLevel2(string[] lines)
        {
            var reportResult = new List<Report>();

            foreach (var line in lines)
            {
                var numbers = line.Split(" ").ToList();

                for(int x= 0; x < numbers.Count; x++)
                {
                    var copiedNumbers = new List<string>(numbers);
                    copiedNumbers.RemoveAt(x);
                    bool isSafe = true;

                    var firstNumber = Convert.ToInt32(copiedNumbers[0]);
                    var secondNumber = Convert.ToInt32(copiedNumbers[1]);

                    var firstLevelDifference = firstNumber - secondNumber;

                    if(firstLevelDifference == 0)
                    {
                        isSafe = false;
                        reportResult.Add(new Report{ Line = line, IsSafe = false});
                    }
                    else if(firstLevelDifference > 0 && firstLevelDifference <= 3)
                    {
                        var previousLevel = firstLevelDifference;

                        for (int i = 1; i < copiedNumbers.Count - 1; i++)
                        {
                            var currentNumber = Convert.ToInt32(copiedNumbers[i]);
                            var nextNumber = Convert.ToInt32(copiedNumbers[i + 1]);
                            
                            var currentLevel = currentNumber-nextNumber;

                            if(currentLevel > 0 && currentLevel <= 3)
                            {
                                previousLevel = currentLevel;
                            }
                            else
                            {
                                isSafe = false;
                                reportResult.Add(new Report{ Line = line, IsSafe = false});
                                break;
                            }
                        }
                    }
                    else if(firstLevelDifference < 0 && firstLevelDifference >= -3)
                    {
                        var previousLevel = firstLevelDifference;

                        for (int i = 1; i < copiedNumbers.Count - 1; i++)
                        {
                            var currentNumber = Convert.ToInt32(copiedNumbers[i]);
                            var nextNumber = Convert.ToInt32(copiedNumbers[i + 1]);
                            
                            var currentLevel = currentNumber-nextNumber;

                            if(currentLevel < 0 && currentLevel >= -3)
                            {
                                previousLevel = currentLevel;
                            }
                            else
                            {
                                isSafe = false;
                                reportResult.Add(new Report{ Line = line, IsSafe = false});
                                break;
                            }
                        }
                    }
                    else
                    {
                        isSafe = false;
                        reportResult.Add(new Report{ Line = line, IsSafe = false});
                    }

                    if(isSafe)
                    {
                        //reportResult.Add("Safe");
                        reportResult.Add(new Report{ Line = line, IsSafe = true});
                        break;
                    }
                    // else
                    // {
                    //     //reportResult.Add("Unsafe");
                    //     reportResult.Add(new Report{ Line = line, IsSafe = false});
                    // }
                }
                
            }

            return reportResult;
        }
    }

    public class Report
    {
        public string Line { get; set; }

        public bool IsSafe { get; set; }
    }
}

