using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput = new List<string>
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput,
                11
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput,
                31
            },
        };

        [TestCaseSource("inputTestCasesPart1")]
        public void CalculateTotalDistance(List<string> input, int solution)
        {
            Assert.AreEqual(solution, Day1.CalculateTotalDistance(input));
        }

        [TestCaseSource("inputTestCasesPart2")]
        public void CalculateTotalSimilarityScore(List<string> input, int solution)
        {
            Assert.AreEqual(solution, Day1.CalculateTotalSimilarityScore(input));
        }
    }
}