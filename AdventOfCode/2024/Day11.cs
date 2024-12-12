namespace AdventOfCode._2024
{
    public class Day11 : IDay
    {
        public int Part1(List<string> input)
        {
            return (int)Part1(input, 25);
        }

        public long Part1(List<string> input, int blinks)
        {
            // input should be one line
            var initialStones = input[0];

            var stones = initialStones.Split(' ').ToList();

            var blinkCounter = 0;
            while (blinkCounter < blinks)
            {
                Console.WriteLine($"Blink counter: {blinkCounter}");
                for (int i = 0; i < stones.Count; i++)
                {
                    var stone = stones[i];
                    if (stone == "0")
                    {
                        stones[i] = "1";
                    }
                    else if (stone.Length % 2 == 0)
                    {
                        var firstNewStone = stone.Substring(0, stone.Length / 2);
                        var secondNewStone = long.Parse(stone.Substring(stone.Length / 2, stone.Length / 2)).ToString();
                        stones[i] = firstNewStone;
                        stones.Insert(i + 1, secondNewStone);
                        i++;
                    }
                    else
                    {
                        var stoneValue = long.Parse(stone);
                        stones[i] = (stoneValue * 2024).ToString();
                    }
                }
                blinkCounter++;
            }


            return stones.Count;
        }

        public int Part2(List<string> input)
        {
            return (int)Part2(input, 75);
        }

        public long Part2(List<string> input, int blinks)
        {
            // input should be one line
            var initialStones = input[0];

            var stones = initialStones.Split(' ').ToList();

            long totalStones = 0;
            foreach (var stone in stones)
            {
                totalStones += GetNumberOfStones(stone, blinks, 1);
            }
            
            Console.WriteLine(totalStones);
            return totalStones;
        }

        // (stone, iteration), number of stones at 75
        private Dictionary<(string, int), long> stoneCache = new Dictionary<(string, int), long>();

        private long GetNumberOfStones(string input, int maxIterations, int currentIteration)
        {
            var stones = PerformTransformation(input);
            if (currentIteration == maxIterations)
            {
                return stones.Count;
            }

            long totalStonesCount = 0;
            foreach (var stone in stones)
            {
                if (stoneCache.ContainsKey((stone, currentIteration)))
                {
                    totalStonesCount += stoneCache[(stone, currentIteration)];
                }
                else
                {
                    var numberOfStones = GetNumberOfStones(stone, maxIterations, currentIteration + 1);
                    totalStonesCount += numberOfStones;
                    stoneCache.Add((stone, currentIteration), numberOfStones);
                }
            }

            return totalStonesCount;
        }

        private List<string> PerformTransformation(string stone)
        {
            var stones = new List<string>();
            if (stone == "0")
            {
                stones.Add("1");
            }
            else if (stone.Length % 2 == 0)
            {
                var firstNewStone = stone.Substring(0, stone.Length / 2);
                var secondNewStone = long.Parse(stone.Substring(stone.Length / 2, stone.Length / 2)).ToString();
                stones.Add(firstNewStone);
                stones.Add(secondNewStone);
            }
            else
            {
                var stoneValue = long.Parse(stone);
                stones.Add((stoneValue * 2024).ToString());
            }
            return stones;
        }

    }
}
