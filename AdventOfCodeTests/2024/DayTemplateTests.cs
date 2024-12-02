using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class DayTemplateTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput = new List<string>
        {
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput,
                2
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput,
                4
            },
        };

        [TestCaseSource("inputTestCasesPart1")]
        public void CalculateTotalDistance(List<string> input, int solution)
        {
            Assert.AreEqual(solution, DayTemplate.Part1(input));
        }

        [TestCaseSource("inputTestCasesPart2")]
        public void CalculateTotalSimilarityScore(List<string> input, int solution)
        {
            Assert.AreEqual(solution, DayTemplate.Part2(input));
        }
    }
}