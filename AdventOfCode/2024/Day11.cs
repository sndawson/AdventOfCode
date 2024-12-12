namespace AdventOfCode._2024
{
    public class Day11 : IDay
    {
        public int Part1(List<string> input)
        {
            return (int)Part1(input, 25);
        }

        public static long Part1(List<string> input, int blinks)
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
            var result = Part1(input, 75);
            Console.WriteLine(result);
            return (int)result;
        }

    }
}
