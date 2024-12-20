﻿namespace AdventOfCode._2024
{
    public class Day02 : IDay
    {
        public int Part1(List<string> input)
        {
            var safeReports = 0;
            foreach (var report in input)
            {
                var levels = report.Split(' ').ToList();
                if (ProcessReport(levels))
                {
                    safeReports++;
                }
            }

            return safeReports;
        }

        public int Part2(List<string> input)
        {
            var safeReports = 0;
            foreach (var report in input)
            {
                var levels = report.Split(' ').ToList();
                if (ProcessReport(levels))
                {
                    safeReports++;
                }
                else
                {
                    // try rerunning the report with one level missing and see if any of those are safe
                    for (int i = 0; i < levels.Count; i++)
                    {
                        var newLevels = new List<string>(levels);
                        newLevels.RemoveAt(i);
                        if (ProcessReport(newLevels))
                        {
                            safeReports++;
                            break;
                        }
                    }
                }
            }

            return safeReports;
        }

        internal static bool ProcessReport(List<string> levels)
        {
            var previous = -1;
            bool? increasing = null;
            var safe = true;
            foreach (var levelStr in levels)
            {
                var level = int.Parse(levelStr);
                if (previous == -1)
                {
                    previous = level;
                    continue;
                }
                if (increasing == null)
                {
                    if (level > previous)
                    {
                        increasing = true;
                    }
                    else // could be equal, but that wouldn't be valid anyway
                    {
                        increasing = false;
                    }
                }

                var diff = 0;
                if ((bool)increasing)
                {
                    diff = level - previous;
                }
                else
                {
                    diff = previous - level;
                }
                if (diff <= 0 || diff > 3)
                {
                    safe = false;
                    break;
                }
                previous = level;
            }
            return safe;
        }
    }
}
