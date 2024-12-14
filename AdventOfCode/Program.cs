using Mono.Options;
using AdventOfCode._2024;

namespace AdventOfCode
{
    public class AdventOfCodeMain
    {
#if DEBUG
        static bool printDebugLogs = true;
#else
        static bool printDebugLogs = false;
#endif

        static void Main(string[] args)
        {
            string inputFilePath = "";
            string inputFileName = "";
            int dayNum;

            var optionSet = new OptionSet {
                { "i|inputFileName=",       "the input file name",          i => inputFileName = i },
                { "v|verbose",              "print debug logs",             v => { if (v != null) printDebugLogs = true; } },
            };

            List<string> extra;
            try
            {
                extra = optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("AdventOfCode.exe: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `AdventOfCode.exe --help' for more information.");
                return;
            }

            dayNum = GetDay();
            inputFilePath = GetInputFilePath(dayNum);

            Console.WriteLine($"Reading file: {inputFilePath}");
            var fileLines = ReadFile(inputFilePath);

            string objectToInstantiate = $"AdventOfCode._2024.Day{dayNum}, AdventOfCode";
            var objectType = Type.GetType(objectToInstantiate);
            dynamic dayClass = Activator.CreateInstance(objectType) as IDay;

            // TODO: time how long each part takes
            Console.WriteLine($"Part 1: {dayClass.Part1(fileLines)}");
            Console.WriteLine($"Part 2: {dayClass.Part2(fileLines)}");
        }

        public static void PrintDebugLog(string lineToPrint)
        {
            if (printDebugLogs)
            {
                Console.WriteLine(lineToPrint);
            }
        }

        static int GetDay()
        {
            Console.WriteLine("Enter the number of the day of the challenge (ex. 01):");
            var dayStr = @"" + Console.ReadLine();
            int day;
            if (int.TryParse(dayStr, out day))
            {
                return day;
            }
            else
            {
                throw new Exception($"`{dayStr}` was not a number");
            }
        }

        static string GetInputFilePath(int dayNum)
        {
            var inputFilePath = $"C:\\Users\\Shawna\\Documents\\Programming\\Coding Challenges\\AdventOfCode\\AdventOfCode\\2024\\InputFiles\\day{dayNum}.txt";

            if (File.Exists(inputFilePath))
            {
                return inputFilePath;
            }
            else
            {
                throw new Exception($"File not found: {inputFilePath}");
            }
        }

        static List<string> ReadFile(string fileLocation)
        {
            var lineList = new List<string>();

            int counter = 0;
            foreach (string line in File.ReadLines(fileLocation))
            {
                lineList.Add(line);
                counter++;
            }

            Console.WriteLine($"There were {counter} lines.");

            return lineList;
        }
    }

}