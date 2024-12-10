namespace AdventOfCode._2024
{
    public class Day09 : IDay
    {
        private int freeSpaceId = -1;

        public int Part1(List<string> input)
        {
            // guaranteed to only be one line
            var diskMap = input[0];

            var diskList = ParseIntoDiskList(diskMap);
            FillFreeSpaces(diskList);
            var checksum = ComputeChecksum(diskList);

            Console.WriteLine(checksum.ToString());
            return (int)checksum;
        }

        public int Part2(List<string> input)
        {
            // guaranteed to only be one line
            var diskMap = input[0];

            var diskList = ParseIntoDiskListOfTuples(diskMap);
            FillFreeSpacesWithoutFragmentation(diskList);
            var checksum = ComputeChecksum(diskList);

            Console.WriteLine(checksum.ToString());
            return (int)checksum;
        }

        private List<int> ParseIntoDiskList(string diskMap)
        {
            var diskList = new List<int>();
            var isFile = true;
            var fileId = 0;
            foreach (var digit in diskMap)
            {
                var size = int.Parse(digit.ToString());
                if (isFile)
                {
                    for (int i = 0; i < size; i++)
                    {
                        diskList.Add(fileId);
                    }
                    fileId++;
                    isFile = false;
                }
                else // TODO: DRY this
                {
                    for (int i = 0; i < size; i++)
                    {
                        diskList.Add(freeSpaceId);
                    }
                    isFile = true;
                }
            }

            return diskList;
        }
        
        private List<(int,int)> ParseIntoDiskListOfTuples(string diskMap)
        {
            var diskList = new List<(int,int)>();
            var isFile = true;
            var fileId = 0;
            foreach (var digit in diskMap)
            {
                var size = int.Parse(digit.ToString());
                if (isFile)
                {
                    diskList.Add((fileId,size));
                    fileId++;
                    isFile = false;
                }
                else // TODO: DRY this
                {
                    diskList.Add((freeSpaceId,size));
                    isFile = true;
                }
            }

            return diskList;
        }

        private void FillFreeSpaces(List<int> diskList)
        {
            for (int i = diskList.Count - 1; i > 0; i--)
            {
                if (diskList[i] != freeSpaceId)
                {
                    var indexOfNextFreeSpace = diskList.IndexOf(freeSpaceId);
                    if (indexOfNextFreeSpace < i)
                    {
                        diskList[indexOfNextFreeSpace] = diskList[i];
                        diskList[i] = freeSpaceId;
                    }
                }
            }
        }

        private void FillFreeSpacesWithoutFragmentation(List<(int,int)> diskList)
        {
            for (int i = diskList.Count - 1; i >0; i--)
            {
                if (diskList[i].Item1 != freeSpaceId)
                {
                    var amountOfSpaceNeeded = diskList[i].Item2;
                    var indexOfNextFreeBlockWithSpace = GetIndexOfNextFreeBlockWithSpace(diskList, amountOfSpaceNeeded);
                    if (indexOfNextFreeBlockWithSpace < i && indexOfNextFreeBlockWithSpace != -1)
                    {
                        var spaceInFreeBlock = diskList[indexOfNextFreeBlockWithSpace].Item2;
                        if (spaceInFreeBlock == amountOfSpaceNeeded)
                        {
                            diskList[indexOfNextFreeBlockWithSpace] = (diskList[i].Item1, spaceInFreeBlock);
                            diskList[i] = (freeSpaceId, spaceInFreeBlock);
                        }
                        else
                        {
                            // reduce size of free block and insert new file block before free block
                            diskList[indexOfNextFreeBlockWithSpace] = (freeSpaceId, spaceInFreeBlock - amountOfSpaceNeeded);
                            diskList.Insert(indexOfNextFreeBlockWithSpace, (diskList[i].Item1, diskList[i].Item2));
                            // also increment i since we added an element
                            i++;
                        }
                        // mark the old spot as free
                        diskList[i] = (freeSpaceId, amountOfSpaceNeeded);
                    }
                }
            }
            // There's a bug here somewhere where it's setting the last free space to be too large
            // But it shouldn't actually affect the results
        }

        private int GetIndexOfNextFreeBlockWithSpace(List<(int,int)> diskList, int amountOfSpaceNeeded)
        {
            // returns -1 if no match found
            var index = diskList.FindIndex(x => x.Item1 == freeSpaceId && x.Item2 >= amountOfSpaceNeeded);
            return index;
        }

        private long ComputeChecksum(List<int> diskList)
        {
            long checksum = 0;
            for (int i = 0; i < diskList.Count; i++)
            {
                var blockValue = diskList[i];
                if (blockValue != freeSpaceId)
                {
                    checksum += i * diskList[i];
                }
            }

            return checksum;
        }

        private long ComputeChecksum(List<(int,int)> diskList)
        {
            long checksum = 0;
            int i = 0;
            foreach (var block in diskList)
            {
                if (block.Item1 == freeSpaceId)
                {
                    i += block.Item2;
                }
                else
                {
                    for (int j = 0; j < block.Item2; j++)
                    {
                        checksum += i * block.Item1;
                        i++;
                    }
                }
            }

            return checksum;
        }
    }
}
