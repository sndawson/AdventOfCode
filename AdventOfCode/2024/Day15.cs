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
            return -1;
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

            // run simulation
            foreach (var move in moves)
            {
                (int, int) moveValue;
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
                var temp = TryToMoveCharScaledUp(map, robot, moveValue);
                var newRobotPos = temp.Item1;
                map = temp.Item2;
                robot = newRobotPos;
            }
            PrintMap(map);

            var gpsCoordsSum = CalculateSumOfGPSCoords(map, boxCharLeft);

            return gpsCoordsSum;
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

        private ((int, int), List<List<char>>) TryToMoveCharScaledUp(List<List<char>> map, (int, int) currentPos, (int, int) moveValue)
        {
            var mapCopy = DeepCopyMap(map);
            var currentCharValue = mapCopy[currentPos.Item1][currentPos.Item2];
            var newPos = (currentPos.Item1 + moveValue.Item1, currentPos.Item2 + moveValue.Item2);
            var mapAtNewPos = mapCopy[newPos.Item1][newPos.Item2];
            switch (mapAtNewPos)
            {
                case blankChar:
                    mapCopy[newPos.Item1][newPos.Item2] = currentCharValue;
                    mapCopy[currentPos.Item1][currentPos.Item2] = blankChar;
                    return (newPos, mapCopy);
                case wallChar:
                    return (currentPos, mapCopy);
                case boxCharLeft:
                    var temp = TryToMoveCharScaledUp(mapCopy, newPos, moveValue);
                    var newBoxPosLeft = temp.Item1;
                    var newPosRight = (newPos.Item1, newPos.Item2 + 1);
                    
                    temp = TryToMoveCharScaledUp(temp.Item2, newPosRight, moveValue);
                    var newBoxPosRight = temp.Item1;
                    if (newBoxPosLeft != newPos && newBoxPosRight != newPosRight)
                    {
                        mapCopy = temp.Item2;
                        mapCopy[newPos.Item1][newPos.Item2] = currentCharValue;
                        mapCopy[currentPos.Item1][currentPos.Item2] = blankChar;
                        return (newPos, mapCopy);
                    }
                    else
                    {
                        return (currentPos, mapCopy);
                    }
                case boxCharRight:
                    var newPosLeft = (newPos.Item1, newPos.Item2 - 1);
                    temp = TryToMoveCharScaledUp(mapCopy, newPosLeft, moveValue);
                    newBoxPosLeft = temp.Item1;

                    temp = TryToMoveCharScaledUp(temp.Item2, newPos, moveValue);
                    newBoxPosRight = temp.Item1;
                    if (newBoxPosLeft != newPos && newBoxPosRight != newPos)
                    {
                        mapCopy = temp.Item2;
                        mapCopy[newPos.Item1][newPos.Item2] = currentCharValue;
                        mapCopy[currentPos.Item1][currentPos.Item2] = blankChar;
                        return (newPos, mapCopy);
                    }
                    else
                    {
                        return (currentPos, mapCopy);
                    }
                default:
                    throw new Exception("Invalid map char");
            }
        }

        private List<List<char>> DeepCopyMap(List<List<char>> map)
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

        private int CalculateSumOfGPSCoords(List<List<char>> map, char charToCount)
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
