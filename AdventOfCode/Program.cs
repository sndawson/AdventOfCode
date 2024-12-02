using Mono.Options;
using System.Diagnostics;
using System.Reflection;

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
            string day = "";

            // TODO: automatically pick the day class
            var optionSet = new OptionSet {
                { "i|inputFileName=",       "the input file name",          i => inputFileName = i },
                { "d|day=",                 "the day to run",               d => day = d },
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

            if (string.IsNullOrEmpty(inputFilePath))
            {
                inputFilePath = GetInputFilePath();
            }
            Console.WriteLine($"Reading file: {inputFilePath}");
            var fileLines = ReadFile(inputFilePath);

            Console.WriteLine($"Solution: {Day2.Part2(fileLines)}");
        }

        public static void PrintDebugLog(string lineToPrint)
        {
            if (printDebugLogs)
            {
                Console.WriteLine(lineToPrint);
            }
        }

        static string GetInputFilePath()
        {
            Console.WriteLine("Enter input file name:");
            var inputFileName = @"" + Console.ReadLine();
            var inputFilePath = $"C:\\Users\\Shawna\\Documents\\Programming\\Coding Challenges\\AdventOfCode\\AdventOfCode\\2024\\InputFiles\\{inputFileName}.txt";

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