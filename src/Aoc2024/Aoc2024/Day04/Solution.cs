using System.Text.RegularExpressions;

namespace Aoc2024.Day04
{
    public static class Solution
    {
        public static void Part01()
        {
            string[] horizontalLines = File.ReadAllLines("Day04/input.txt");

            int rows = horizontalLines.Length;
            int cols = horizontalLines[0].Length;

            string[] verticalLines = new string[cols];

            for (int col = 0; col < cols; col++)
            {
                char[] newRow = new char[rows];
                for (int row = 0; row < rows; row++)
                {
                    newRow[row] = horizontalLines[row][col];
                }
                verticalLines[col] = new string(newRow);
            }

            var totalCount = 0;

            foreach (string line in horizontalLines)
            {
                totalCount = totalCount + GetMatchingCount(line);
            }

            foreach (string line in verticalLines)
            {
                totalCount = totalCount + GetMatchingCount(line);
            }

            List<string> diagonals = GetAllDiagonals(horizontalLines);

            foreach (string line in diagonals)
            {
                totalCount = totalCount + GetMatchingCount(line);
            }

            Console.WriteLine("Total count:" + totalCount);
        }

        static List<string> GetAllDiagonals(string[] data)
        {
            var diagonals = new List<string>();
            var rowCount = data.Length;
            var colCount = data[0].Length;

            for (int start = 0; start < rowCount + colCount - 1; start++)
            {
                var diagonal = new List<char>();
                for (int i = 0; i < rowCount; i++)
                {
                    var j = start - i;
                    if (j >= 0 && j < colCount)
                    {
                        diagonal.Add(data[i][j]);
                    }
                }
                diagonals.Add(new string(diagonal.ToArray()));
            }

            for (int start = 1 - colCount; start < rowCount; start++)
            {
                var diagonal = new List<char>();
                for (int i = 0; i < rowCount; i++)
                {
                    var j = i - start;
                    if (j >= 0 && j < colCount)
                    {
                        diagonal.Add(data[i][j]);
                    }
                }
                diagonals.Add(new string(diagonal.ToArray()));
            }

            return diagonals;
        }

        static int GetMatchingCount(string text)
        {
            string[] patterns = { "XMAS", "SAMX" };

            var matchCount = 0;

            foreach (string pattern in patterns)
            {
                var matches = Regex.Matches(text, pattern);
                matchCount += matches.Count;
            }

            return matchCount;
        }

        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day04/input.txt");

            int rowCount = lines.Length;
            int columnCount = lines[0].Length;

            var totalCount = 0;
            
            // array reference

            // Console.WriteLine("expected A actual:" + lines[1][2]);

            // Console.WriteLine("expected X actual:" + lines[4][0]);

            // sliding window block approach

            int numberRowsRead = 0;
            for(int row = 0; row < rowCount-2; row++)
            {
                int row1 = row; int row2 = row + 1; int row3 = row + 2;

                for(int column = 0; column < columnCount - 2 ; column++) 
                {
                    int col1 = column; int col2 = column + 1; int col3 = column + 2;
                
                    //M.M
                    //.A.
                    //S.S

                    if(lines[row1][col1] == 'M' && lines[row1][col3] == 'M'
                                    && lines[row2][col2] == 'A'
                        && lines[row3][col1] == 'S' && lines[row3][col3] == 'S'){
                            totalCount++;
                        }

                    //S.M
                    //.A.
                    //S.M

                    if(lines[row1][col1] == 'S' && lines[row1][col3] == 'M'
                                    && lines[row2][col2] == 'A'
                        && lines[row3][col1] == 'S' && lines[row3][col3] == 'M'){
                            totalCount++;
                        }

                    //S.S
                    //.A.
                    //M.M

                    if(lines[row1][col1] == 'S' && lines[row1][col3] == 'S'
                                    && lines[row2][col2] == 'A'
                    && lines[row3][col1] == 'M' && lines[row3][col3] == 'M'){
                        totalCount++;
                    }

                    //M.S
                    //.A.
                    //M.S

                    if(lines[row1][col1] == 'M' && lines[row1][col3] == 'S'
                                    && lines[row2][col2] == 'A'
                    && lines[row3][col1] == 'M' && lines[row3][col3] == 'S'){
                        totalCount++;
                    }
                }
                numberRowsRead++;
            }

            
            //Console.WriteLine("numberRowsRead count:" + numberRowsRead);
            

            

            

            
    

            Console.WriteLine("Total count:" + totalCount);
        }
    }
}

