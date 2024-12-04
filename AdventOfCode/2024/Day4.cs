using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public class Day4 : IDay
    {
        public int Part1(List<string> input)
        {
            var occurrences = 0;
            //start by just finding the occurrences on one line
            foreach (var line in input)
            {
                occurrences += CountXMASMatches(line);
            }
            // certainly not the most efficient, but then figure out all the other lines?
            foreach (var line in GetVerticalLines(input))
            {
                occurrences += CountXMASMatches(line);
            }
            foreach (var line in GetTopLeftDownDiagonalLines(input))
            {
                occurrences += CountXMASMatches(line);
            }
            foreach (var line in GetBottomLeftUpDiagonalLines(input))
            {
                occurrences += CountXMASMatches(line);
            }

            return occurrences;
        }

        public int Part2(List<string> input)
        {
            return 0;
        }

        private int CountXMASMatches(string input)
        {
            var xmasMatches = Regex.Matches(input, @"XMAS").Count;
            var samxMatches = Regex.Matches(input, @"SAMX").Count;
            return xmasMatches + samxMatches;
        }

        private List<string> GetVerticalLines(List<string> input)
        {
            var verticalLines = new List<string>(new string[input.Count]);
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    // char array would be more efficient so it's not recreating the string
                    verticalLines[j] += input[i][j];
                }
            }
            return verticalLines;
        }

        private List<string> GetTopLeftDownDiagonalLines(List<string> input)
        {
            var numberOfDiagonalLines = input.Count + input[0].Length - 1;
            var diagonalLines = new List<string>(new string [numberOfDiagonalLines]);

            for (int i = 0; i < input.Count; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        int a = i;
                        int b = j;
                        while (a < input.Count && b < input[i].Length)
                        {
                            diagonalLines[i + j] += input[a][b];
                            a++;
                            b++;
                        }
                    }
                }
                else
                {
                    int a = i;
                    int b = 0;
                    while (a < input.Count && b < input[i].Length)
                    {
                        diagonalLines[i + input[i].Length - 1] += input[a][b];
                        a++;
                        b++;
                    }
                }
            }

            return diagonalLines;
        }

        private List<string> GetBottomLeftUpDiagonalLines(List<string> input)
        {
            var numberOfDiagonalLines = input.Count + input[0].Length - 1;
            var diagonalLines = new List<string>(new string[numberOfDiagonalLines]);

            for (int i = input.Count - 1; i >= 0; i--)
            {
                if (i == input.Count - 1)
                {
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        int a = i;
                        int b = j;
                        while (a >= 0 && b < input[i].Length)
                        {
                            diagonalLines[i + j] += input[a][b];
                            a--;
                            b++;
                        }
                    }
                }
                else
                {
                    int a = i;
                    int b = 0;
                    while (a >= 0 && b < input[i].Length)
                    {
                        diagonalLines[i] += input[a][b];
                        a--;
                        b++;
                    }
                }
            }

            return diagonalLines;
        }

    }
}
