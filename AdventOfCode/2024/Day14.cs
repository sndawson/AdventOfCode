using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public class Day14 : IDay
    {
        public int Part1(List<string> input)
        {
            return Part1(input, 101, 103, 100);
            //return Part1(input, 11, 7, 100);
        }

        public int Part1(List<string> input, int width, int height, int seconds)
        {
            // first parse
            var robots = new List<Robot>();
            foreach (var line in input)
            {
                var matches = Regex.Matches(line, @"(-?\d+)");
                var pX = int.Parse(matches[0].Value);
                var pY = int.Parse(matches[1].Value);
                var vX = int.Parse(matches[2].Value);
                var vY = int.Parse(matches[3].Value);
                var robot = new Robot((pX,pY), (vX, vY));
                robots.Add(robot);
            }
            PrintBoard(robots, width, height);

            // then calculate board location for each robot after required seconds have elapsed
            foreach (var robot in robots)
            {
                var position = robot.position;
                var velocity = robot.velocity;
                var newX = (position.Item1 + (velocity.Item1 * seconds));
                var newY = (position.Item2 + (velocity.Item2 * seconds));
                var newPosition = (mod(newX, width), mod(newY, height));
                robot.position = newPosition;
            }
            PrintBoard(robots, width, height);

            // then count robots in each quadrant
            var xCentre = Math.Floor((double)(width / 2));
            var yCentre = Math.Floor((double)(height / 2));
            var quadrantCount = new List<int> { 0, 0, 0, 0 };
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var robotsAtPos = robots.Count(x => x.position == (j, i));
                    if (robotsAtPos < 1)
                    {
                        continue;
                    }
                    if (i < yCentre && j < xCentre)
                    {
                        quadrantCount[0] += robotsAtPos;
                    }
                    else if (i > yCentre && j < xCentre)
                    {
                        quadrantCount[1] += robotsAtPos;
                    }
                    else if (i < yCentre && j > xCentre)
                    {
                        quadrantCount[2] += robotsAtPos;
                    }
                    else if (i > yCentre && j > xCentre)
                    {
                        quadrantCount[3] += robotsAtPos;
                    }
                }
            }
            var safetyFactor = quadrantCount[0] * quadrantCount[1] * quadrantCount[2] * quadrantCount[3];

            return safetyFactor;
        }

        public int Part2(List<string> input)
        {
            return 0;
        }

        private int mod(int a, int b)
        {
            int result = a % b;
            if ((result < 0 && b > 0) || (result > 0 && b < 0))
            {
                result += b;
            }
            return result;
        }

        private void PrintBoard(List<Robot> robots, int width, int height)
        {
            Console.WriteLine("Board:");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var robotsAtPos = robots.Count(x => x.position == (j, i));
                    if (robotsAtPos > 0)
                    {
                        Console.Write(robotsAtPos);
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }
    }
    
    class Robot
    {
        public (int, int) position;
        public (int, int) velocity;

        public Robot ((int,int) position, (int,int) velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
    }
}
