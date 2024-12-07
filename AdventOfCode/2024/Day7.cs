namespace AdventOfCode._2024
{
    public class Day7 : IDay
    {
        public int Part1(List<string> input)
        {
            var result = Part1Long(input);
            Console.WriteLine(result.ToString());
            return (int)result;
        }

        public long Part1Long(List<string> input)
        {
            long totalCalibrationResult = 0;

            foreach (var line in input)
            {
                var parts = line.Split(": ");
                var testResult = long.Parse(parts[0]);
                var testValues = parts[1].Split(' ').Select(x => long.Parse(x)).ToList();

                if (IsValidEquation(testResult, null, testValues))
                {
                    totalCalibrationResult += testResult;
                }
            }

            return totalCalibrationResult;
        }

        public int Part2(List<string> input)
        {
            var result = Part2Long(input);
            Console.WriteLine(result.ToString());
            return (int)result;
        }

        public long Part2Long(List<string> input)
        {
            long totalCalibrationResult = 0;

            foreach (var line in input)
            {
                var parts = line.Split(": ");
                var testResult = long.Parse(parts[0]);
                var testValues = parts[1].Split(' ').Select(x => long.Parse(x)).ToList();

                if (IsValidEquationWithConcats(testResult, null, testValues))
                {
                    totalCalibrationResult += testResult;
                }
            }

            return totalCalibrationResult;
        }

        private bool IsValidEquation(long goal, long? resultSoFar, List<long> operands)
        {
            if (resultSoFar == null)
            {
                return IsValidEquation(goal, operands[0], operands.GetRange(1, operands.Count - 1));
            }
            else if (operands.Count == 1) {
                return resultSoFar + operands[0] == goal || resultSoFar * operands[0] == goal;
            }
            else
            {
                return IsValidEquation(goal, resultSoFar + operands[0], operands.GetRange(1, operands.Count - 1)) ||
                    IsValidEquation(goal, resultSoFar * operands[0], operands.GetRange(1, operands.Count - 1));
            }
        }

        private bool IsValidEquationWithConcats(long goal, long? resultSoFar, List<long> operands)
        {
            if (resultSoFar == null)
            {
                return IsValidEquationWithConcats(goal, operands[0], operands.GetRange(1, operands.Count - 1));
            }
            else if (operands.Count == 1)
            {
                return resultSoFar + operands[0] == goal ||
                    resultSoFar * operands[0] == goal ||
                    Concat((long)resultSoFar, operands[0]) == goal;
            }
            else
            {
                return IsValidEquationWithConcats(goal, resultSoFar + operands[0], operands.GetRange(1, operands.Count - 1)) ||
                    IsValidEquationWithConcats(goal, resultSoFar * operands[0], operands.GetRange(1, operands.Count - 1)) ||
                    IsValidEquationWithConcats(goal, Concat((long)resultSoFar, operands[0]), operands.GetRange(1, operands.Count - 1));
            }
        }

        private static long Concat(long a, long b)
        {
            return long.Parse(a.ToString() + b.ToString());
        }

    }
}
