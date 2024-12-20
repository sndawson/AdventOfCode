﻿using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode._2024;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class Day11Tests
    {
        readonly Day11 day = new Day11();

        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput1 = new List<string>
        {
            "0 1 10 99 999",
        };

        private static List<string> sampleInput2 = new List<string>
        {
            "125 17"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput1,
                1,
                7
            },
            new object[]
            {
                sampleInput2,
                6,
                22
            },
            new object[]
            {
                sampleInput2,
                25,
                55312
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput2,
                75,
                65601038650482
            },
        };

        [TestCaseSource("inputTestCasesPart1")]
        public void TestPart1(List<string> input, int blinks, long solution)
        {
            Assert.AreEqual(solution, day.Part1(input, blinks));
        }

        [TestCaseSource("inputTestCasesPart2")]
        public void TestPart2(List<string> input, int blinks, long solution)
        {
            // get a fresh instance of the day class since it has a cache now
            var localDay = new Day11();
            Assert.AreEqual(solution, localDay.Part2(input, blinks));
        }
    }
}