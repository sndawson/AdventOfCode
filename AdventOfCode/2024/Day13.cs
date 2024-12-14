using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public class Day13 : IDay
    {
        private const int aCost = 3;
        private const int bCost = 1;

        public int Part1(List<string> input)
        {
            long multiplier = 1;
            var totalTokenCost = RunProblem(input, multiplier);

            return (int)totalTokenCost;
        }

        public int Part2(List<string> input)
        {
            long multiplier = 10000000000000;
            var totalTokenCost = RunProblem(input, multiplier);

            Console.WriteLine(totalTokenCost.ToString());
            return (int)totalTokenCost;
        }

        private long RunProblem(List<string> input, long multiplier)
        {
            var a = (0, 0);
            var b = (0, 0);
            (long, long) prize = (0, 0);
            var totalTokenCost = 0;
            for (int i = 0; i < input.Count; i++)
            {
                var line = input[i];
                if (line != "")
                {
                    var matches = Regex.Matches(line, @"(\d)+(\d)+");

                    var x = int.Parse(matches[0].ToString());
                    var y = int.Parse(matches[1].ToString());
                    if (line.Contains('A'))
                    {
                        a = (x, y);
                    }
                    else if (line.Contains('B'))
                    {
                        b = (x, y);
                    }
                    else
                    {
                        prize = (x * multiplier, y * multiplier);
                    }

                    if (line.StartsWith("Prize"))
                    {
                        costCache = new Dictionary<(long, long), long?>();
                        var cost = CalculateLowestTokenCost(a, b, prize);
                        if (cost.HasValue)
                        {
                            totalTokenCost += (int)cost;
                        }
                    }
                }
            }

            return totalTokenCost;
        }

        // (x, y), cheapestCost
        private Dictionary<(long, long), long?> costCache;

        private long? CalculateLowestTokenCost((int,int) a, (int,int) b, (long,long) prize)
        {
            if (prize == (0,0))
            {
                return 0;
            }
            if (prize.Item1 < 0 || prize.Item2 < 0)
            {
                return null;
            }
            if (costCache.ContainsKey(prize))
            {
                return costCache[prize];
            }

            var prizeWithA = (prize.Item1 - a.Item1, prize.Item2 - a.Item2);
            var prizeWithB = (prize.Item1 - b.Item1, prize.Item2 - b.Item2);
            var pressA = CalculateLowestTokenCost(a, b, prizeWithA);
            var pressB = CalculateLowestTokenCost(a, b, prizeWithB);

            long? lowestCost;
            if (!pressA.HasValue && !pressB.HasValue)
            {
                lowestCost = null;
            }
            else if (!pressA.HasValue)
            {
                lowestCost = pressB + bCost;
            }
            else if (!pressB.HasValue)
            {
                lowestCost = pressA + aCost;
            }
            else
            {
                pressA += aCost;
                pressB += bCost;

                if (pressA > pressB)
                {
                    lowestCost = (int)pressB;
                }
                else
                {
                    lowestCost = (int)pressA;
                }
            }

            costCache.Add(prize, lowestCost);

            return lowestCost;
        }
    }
}
