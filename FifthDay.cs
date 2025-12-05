using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class FifthDay {
        static (long min, long max) GetRange(string rangeStr) {
            string[] parts = rangeStr.Split('-');
            long min = Convert.ToInt64(parts[0]);
            long max = Convert.ToInt64(parts[1]);

            return (min, max);
        }

        static bool IsFresh(long fruit, List<(long min, long max)> ranges) {
            foreach (var range in ranges) {
                if (fruit >= range.min && fruit <= range.max) {
                    return true;
                }
            }
            return false;
        }

        static int GetBlankIndex(string[] lines) {
            for (int i = 0; i < lines.Length; i++) {
                if (string.IsNullOrWhiteSpace(lines[i])) {
                    return i;
                }
            }
            return -1;
        }

        static List<(long min, long max)> MergeRanges(List<(long min, long max)> ranges) {
            if (ranges.Count == 0) return ranges;

            var sortedRanges = ranges.OrderBy(r => r.min).ToList();

            List<(long min, long max)> merged = new List<(long min, long max)>();
            merged.Add(sortedRanges[0]);

            for (int i = 1; i < sortedRanges.Count; i++) {
                var current = sortedRanges[i];
                var lastMerged = merged[merged.Count - 1];

                if (current.min <= lastMerged.max + 1) {
                    merged[merged.Count - 1] = (lastMerged.min, Math.Max(lastMerged.max, current.max));
                } else {
                    merged.Add(current);
                }
            }

            return merged;
        }

        public static long FifthDayA() {
            string[] lines = AOCUtils.ReadInput("5");
            int blankLineIndex = GetBlankIndex(lines);

            List<(long min, long max)> ranges = new List<(long min, long max)>();
            for (int i = 0; i < blankLineIndex; i++) {
                if (!string.IsNullOrWhiteSpace(lines[i])) {
                    ranges.Add(GetRange(lines[i]));
                }
            }

            List<long> ingredientIds = new List<long>();
            for (int i = blankLineIndex + 1; i < lines.Length; i++) {
                if (!string.IsNullOrWhiteSpace(lines[i])) {
                    ingredientIds.Add(Convert.ToInt64(lines[i]));
                }
            }

            long freshCount = 0;
            foreach (long ingredientId in ingredientIds) {
                if (IsFresh(ingredientId, ranges)) {
                    freshCount++;
                }
            }

            return freshCount;
        }

        public static long FifthDayB() {
            string[] lines = AOCUtils.ReadInput("5");
            int blankLineIndex = GetBlankIndex(lines);

            List<(long min, long max)> ranges = new List<(long min, long max)>();
            for (int i = 0; i < blankLineIndex; i++) {
                if (!string.IsNullOrWhiteSpace(lines[i])) {
                    ranges.Add(GetRange(lines[i]));
                }
            }

            var mergedRanges = MergeRanges(ranges);
            long totalFreshIds = 0;

            foreach (var range in mergedRanges) {
                totalFreshIds += range.max - range.min + 1;
            }

            return totalFreshIds;
        }
    }
}