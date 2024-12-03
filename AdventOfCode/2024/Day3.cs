using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public class Day3 : IDay
    {
        public int Part1(List<string> input)
        {
            // should always be one line
            // sike, it actually isn't all one line, but we can fix that manually in the input easily enough...
            var inputStr = input[0];

            var result = 0;
            var match = Regex.Match(inputStr, @"mul\(\d{1,3},\d{1,3}\)");
            while (match.Success)
            {
                var mulStatement = match.Groups[0].Value;
                var numMatch = Regex.Match(mulStatement, @"\d{1,3}");
                var mulResult = 1;
                while (numMatch.Success)
                {
                    var numVal = int.Parse(numMatch.Groups[0].Value);
                    mulResult *= numVal;
                    numMatch = numMatch.NextMatch();
                }
                result += mulResult;
                match = match.NextMatch();
            }

            return result;
        }

        public int Part2(List<string> input)
        {
            // should always be one line
            // sike, it actually isn't all one line, but we can fix that manually in the input easily enough...
            var inputStr = input[0];

            var result = 0;
            var enabled = true;
            var match = Regex.Match(inputStr, @"(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don't\(\))");
            while (match.Success)
            {
                var statement = match.Groups[0].Value;
                if (statement.StartsWith("mul") && enabled)
                {
                    var numMatch = Regex.Match(statement, @"\d{1,3}");
                    var mulResult = 1;
                    while (numMatch.Success)
                    {
                        var numVal = int.Parse(numMatch.Groups[0].Value);
                        mulResult *= numVal;
                        numMatch = numMatch.NextMatch();
                    }
                    result += mulResult;
                }
                else if (statement.StartsWith("don't"))
                {
                    enabled = false;
                }
                else if (statement.StartsWith("do"))
                {
                    enabled = true;
                }
                match = match.NextMatch();
            }

            return result;
        }

    }
}
