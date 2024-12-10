namespace AdventOfCode._2024
{
    public class Day06 : IDay
    {
        List<char> arrows = new List<char> { '^', '<', '>', 'v' };

        public int Part1(List<string> input)
        {
            // first find the guard
            var guardCoordinates = FindGuardCoordinates(input);

            // then walk the guard's path until they leave the area, marking the visited tiles
            var trackingBoard = CreateTrackingBoard(input);
            //var areaMapCopy = new char[10, 10];
            var i = guardCoordinates.Item1;
            var j = guardCoordinates.Item2;
            var guardDirection = input[i][j];
            while (true)
            {
                // first mark that we've been here
                trackingBoard[i][j] = 'X';

                (int, int) nextTile;
                switch (guardDirection)
                {
                    case '^':
                        nextTile = (i - 1, j);
                        break;
                    case '<':
                        nextTile = (i, j - 1);
                        break;
                    case '>':
                        nextTile = (i, j + 1);
                        break;
                    case 'v':
                        nextTile = (i + 1, j);
                        break;
                    default:
                        throw new Exception($"Illegal direction: {guardDirection}");
                }

                // the while loop was supposed to handle this, but I didn't set this up properly for that
                char nextTileValue;
                try
                {
                    nextTileValue = input[nextTile.Item1][nextTile.Item2];
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // the guard has left the area, so stop processing
                    break;
                }
                

                if (nextTileValue == '.' || arrows.Contains(nextTileValue))
                {
                    // go to next tile
                    i = nextTile.Item1;
                    j = nextTile.Item2;
                }
                else if (nextTileValue == '#')
                {
                    // obstacle, turn to the right
                    switch (guardDirection)
                    {
                        case '^':
                            guardDirection = '>';
                            break;
                        case '<':
                            guardDirection = '^';
                            break;
                        case '>':
                            guardDirection = 'v';
                            break;
                        case 'v':
                            guardDirection = '<';
                            break;
                        default:
                            throw new Exception($"Illegal direction: {guardDirection}");
                    }
                }
                else
                {
                    throw new Exception($"Illegal tile value: ${nextTileValue}");
                }

            }

            // finally count the number of visited tiles
            var guardPositions = 0;
            foreach (var row in trackingBoard)
            {
                foreach (var tile in row)
                {
                    Console.Write(tile.ToString());
                    if (tile == 'X')
                    {
                        guardPositions++;
                    }
                }
                Console.Write("\n");
            }

            return guardPositions;
        }

        public int Part2(List<string> input)
        {
            return 0;
        }

        private (int, int) FindGuardCoordinates(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (arrows.Contains(input[i][j]))
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        private List<List<char>> CreateTrackingBoard(List<string> input)
        {
            // I'm certain there are better ways to do this
            var board = new List<List<char>>();
            for (int i = 0; i < input.Count; i++)
            {
                board.Add(new List<char>());
                for (int j = 0; j < input[i].Length; j++)
                {
                    board[i].Add('.');
                }
            }
            return board;
        }
    }
}
