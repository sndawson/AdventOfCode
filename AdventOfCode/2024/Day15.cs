namespace AdventOfCode._2024
{
    public class Day15 : IDay
    {
        private const char robotChar = '@';
        private const char wallChar = '#';
        private const char boxChar = 'O';
        private const char blankChar = '.';

        public int Part1(List<string> input)
        {
            var map = new List<List<char>>();
            var parseMoves = false;
            var moves = "";
            var robot = (-1,-1);
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] != "" && !parseMoves)
                {
                    map.Add(new List<char>());
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        var charValue = input[i][j];
                        map[i].Add(charValue);
                        if (charValue == robotChar)
                        {
                            robot = (i, j);
                        }
                    }
                }
                else if (input[i] == "")
                {
                    parseMoves = true;
                }
                else
                {
                    moves += input[i];
                }
            }
            PrintMap(map);

            // run simulation
            foreach (var move in moves)
            {
                (int,int) moveValue;
                switch (move)
                {
                    case '^':
                        moveValue = (-1, 0);
                        break;
                    case 'v':
                        moveValue = (1, 0);
                        break;
                    case '<':
                        moveValue = (0, -1);
                        break;
                    case '>':
                        moveValue = (0, 1);
                        break;
                    default:
                        throw new Exception("Invalid move");
                }
                var newRobotPos = TryToMoveChar(map, robot, moveValue);
                robot = newRobotPos;  
            }
            PrintMap(map);

            // calculate sum of box GPS coordinates
            var gpsCoordsSum = 0;
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == boxChar)
                    {
                        var gpsCoord = (i * 100) + j;
                        gpsCoordsSum += gpsCoord;
                    }
                }
            }

            return gpsCoordsSum;
        }

        public int Part2(List<string> input)
        {
            return 0;
        }

        private void PrintMap(List<List<char>> map)
        {
            Console.WriteLine("Map:");
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    Console.Write(map[i][j]);
                }
                Console.WriteLine();
            }
        }

        private (int,int) TryToMoveChar(List<List<char>> map, (int,int) currentPos, (int,int) moveValue)
        {
            var currentCharValue = map[currentPos.Item1][currentPos.Item2];
            var newPos = (currentPos.Item1 + moveValue.Item1, currentPos.Item2 + moveValue.Item2);
            var mapAtNewPos = map[newPos.Item1][newPos.Item2];
            switch (mapAtNewPos)
            {
                case blankChar:
                    map[newPos.Item1][newPos.Item2] = currentCharValue;
                    map[currentPos.Item1][currentPos.Item2] = blankChar;
                    return newPos;
                case wallChar:
                    return currentPos;
                case boxChar:
                    // TODO: DRY
                    var newBoxPos = TryToMoveChar(map, newPos, moveValue);
                    if (newBoxPos != newPos)
                    {
                        map[newPos.Item1][newPos.Item2] = currentCharValue;
                        map[currentPos.Item1][currentPos.Item2] = blankChar;
                        return newPos;
                    }
                    else
                    {
                        return currentPos;
                    }
                default:
                    throw new Exception("Invalid map char");
            }
        }

    }
}
