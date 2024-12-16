namespace AdventOfCode._2024
{
    public class Day15 : IDay
    {
        private const char robotChar = '@';
        private const char wallChar = '#';
        private const char boxChar = 'O';
        private const char blankChar = '.';

        private const char boxCharLeft = '[';
        private const char boxCharRight = ']';

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

            map = RunSimulation(map, moves, robot);
            PrintMap(map);

            var gpsCoordsSum = CalculateSumOfGPSCoords(map, boxChar);

            return gpsCoordsSum;
        }

        public int Part2(List<string> input)
        {
            var map = new List<List<char>>();
            var parseMoves = false;
            var moves = "";
            var robot = (-1, -1);
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] != "" && !parseMoves)
                {
                    map.Add(new List<char>());
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        var charValue = input[i][j];
                        switch (charValue)
                        {
                            case boxChar:
                                map[i].Add(boxCharLeft);
                                map[i].Add(boxCharRight);
                                break;
                            case robotChar:
                                map[i].Add(robotChar);
                                map[i].Add(blankChar);
                                robot = (i, j * 2);
                                break;
                            default:
                                map[i].Add(charValue);
                                map[i].Add(charValue);
                                break;
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

            map = RunSimulation(map, moves, robot);
            PrintMap(map);

            var gpsCoordsSum = CalculateSumOfGPSCoords(map, boxCharLeft);

            return gpsCoordsSum;
        }

        private static void PrintMap(List<List<char>> map)
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

        private List<List<char>> RunSimulation(List<List<char>> map, string moves, (int,int) robot)
        {
            foreach (var move in moves)
            {
                var moveValue = move switch
                {
                    '^' => (-1, 0),
                    'v' => (1, 0),
                    '<' => (0, -1),
                    '>' => (0, 1),
                    _ => throw new Exception("Invalid move"),
                };
                var result = TryToMoveChar(map, robot, moveValue);
                robot = result.Item1;
                map = result.Item2;
            }
            return map;
        }

        private ((int, int), List<List<char>>) TryToMoveChar(List<List<char>> map, (int, int) currentPos, (int, int) moveValue)
        {
            var mapCopy = DeepCopyMap(map);

            var newPos = (currentPos.Item1 + moveValue.Item1, currentPos.Item2 + moveValue.Item2);
            var mapAtNewPos = mapCopy[newPos.Item1][newPos.Item2];
            switch (mapAtNewPos)
            {
                case blankChar:
                    return (MoveChar(mapCopy, currentPos, newPos), mapCopy);
                case wallChar:
                    return (currentPos, mapCopy);
                case boxChar:
                    var result = TryToMoveChar(mapCopy, newPos, moveValue);
                    if (result.Item1 != newPos)
                    {
                        mapCopy = result.Item2;
                        return (MoveChar(mapCopy, currentPos, newPos), mapCopy);
                    }
                    else
                    {
                        return (currentPos, mapCopy);
                    }
                case boxCharLeft:
                    var newPosRight = (newPos.Item1, newPos.Item2 + 1);
                    return TryToMoveBigBox(mapCopy, currentPos, newPosRight, newPos, moveValue);
                case boxCharRight:
                    var newPosLeft = (newPos.Item1, newPos.Item2 - 1);
                    return TryToMoveBigBox(mapCopy, currentPos, newPosLeft, newPos, moveValue);
                default:
                    throw new Exception("Invalid map char");
            }
        }

        private static (int,int) MoveChar(List<List<char>> map, (int,int) currentPos, (int,int) newPos)
        {
            var currentCharValue = map[currentPos.Item1][currentPos.Item2];
            map[newPos.Item1][newPos.Item2] = currentCharValue;
            map[currentPos.Item1][currentPos.Item2] = blankChar;
            return newPos;
        }

        private ((int, int), List<List<char>>) TryToMoveBigBox(List<List<char>> map, (int, int) currentPos, (int,int) newPosFirst, (int,int) newPosSecond, (int, int) moveValue)
        {
            var result = TryToMoveChar(map, newPosFirst, moveValue);
            if (result.Item1 == newPosFirst)
            {
                return (currentPos, map);
            }

            result = TryToMoveChar(result.Item2, newPosSecond, moveValue);
            if (result.Item1 != newPosSecond)
            {
                map = result.Item2;
                return (MoveChar(map, currentPos, newPosSecond), map);
            }
            else
            {
                return (currentPos, map);
            }
        }

        private static List<List<char>> DeepCopyMap(List<List<char>> map)
        {
            var listCopy = new List<List<char>>();
            for (int i = 0; i < map.Count; i++)
            {
                listCopy.Add(new List<char>());
                for (int j = 0; j < map[i].Count; j++)
                {
                    listCopy[i].Add(map[i][j]);
                }
            }
            return listCopy;
        }

        private static int CalculateSumOfGPSCoords(List<List<char>> map, char charToCount)
        {
            var gpsCoordsSum = 0;
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == charToCount)
                    {
                        var gpsCoord = (i * 100) + j;
                        gpsCoordsSum += gpsCoord;
                    }
                }
            }
            return gpsCoordsSum;
        }
    }
}
