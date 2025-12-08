using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class SeventhDay {
        public static long SeventhDayA() {
            string[] lines = AOCUtils.ReadInput("7");

            int rows = lines.Length;
            int cols = lines[0].Length;

            int startCol = -1;
            for (int c = 0; c < cols; c++) {
                if (lines[0][c] == 'S') {
                    startCol = c;
                    break;
                }
            }

            HashSet<int> activeBeams = new HashSet<int>();
            activeBeams.Add(startCol);

            int splits = 0;

            // 0. sor az 's'
            for (int r = 1; r < rows; r++) {
                HashSet<int> newBeams = new HashSet<int>();

                for (int c = 0; c < cols; c++) {
                    if (lines[r][c] == '^' && activeBeams.Contains(c)) {
                        splits++;

                        // b
                        if (c - 1 >= 0) {
                            newBeams.Add(c - 1);
                        }

                        // j
                        if (c + 1 < cols) {
                            newBeams.Add(c + 1);
                        }
                    } else if (activeBeams.Contains(c) && lines[r][c] != '^') {
                        // tovabb halad lefele
                        newBeams.Add(c);
                    }
                }

                activeBeams = newBeams;
            }

            return splits;
        }

        public static long SeventhDayB() {
            string[] lines = AOCUtils.ReadInput("7");

            int rows = lines.Length;
            int cols = lines[0].Length;

            int startCol = -1;
            for (int c = 0; c < cols; c++) {
                if (lines[0][c] == 'S') {
                    startCol = c;
                    break;
                }
            }

            // timelineok szama az oszlopon
            Dictionary<int, long> timelines = new Dictionary<int, long>();
            timelines[startCol] = 1;

            for (int r = 1; r < rows; r++) {
                Dictionary<int, long> nextTimelines = new Dictionary<int, long>();

                foreach (var kvp in timelines) {
                    int c = kvp.Key;
                    long count = kvp.Value;

                    char cell = lines[r][c];

                    if (cell == '.') {
                        if (!nextTimelines.ContainsKey(c)) {
                            nextTimelines[c] = 0;
                        }
                        nextTimelines[c] += count;
                    } else if (cell == '^') {
                        // b
                        if (c - 1 >= 0) {
                            if (!nextTimelines.ContainsKey(c - 1)) {
                                nextTimelines[c - 1] = 0;
                            }
                            nextTimelines[c - 1] += count;
                        }

                        // j
                        if (c + 1 < cols) {
                            if (!nextTimelines.ContainsKey(c + 1)) {
                                nextTimelines[c + 1] = 0;
                            }
                            nextTimelines[c + 1] += count;
                        }
                    } else {
                        if (!nextTimelines.ContainsKey(c)) {
                            nextTimelines[c] = 0;
                        }
                        nextTimelines[c] += count;
                    }
                }

                timelines = nextTimelines;
            }

            long totalTimelines = 0;
            foreach (var count in timelines.Values) {
                totalTimelines += count;
            }

            return totalTimelines;
        }
    }
}
