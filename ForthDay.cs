using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class ForthDay {
        static char[] GetAdjacent(char[,] matrix, int currentI, int currentJ) {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            char[] adjs = new char[8];

            int[,] dirs = new int[,] {
                {-1, -1}, {-1, 0}, {-1, 1},
                {0, -1},           {0, 1},
                {1, -1},  {1, 0},  {1, 1}
            };

            for (int k = 0; k < dirs.GetLength(0); k++) {
                int newI = currentI + dirs[k, 0];
                int newJ = currentJ + dirs[k, 1];
                if (newI >= 0 && newI < n && newJ >= 0 && newJ < m) {
                    adjs[k] = matrix[newI, newJ];
                } else {
                    adjs[k] = '.';
                }
            }

            return adjs;
        }

        static int GetPaperRolls(char[,] matrix) {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            int ans = 0;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (matrix[i, j] == '@') {
                        char[] adjs = GetAdjacent(matrix, i, j);
                        int paperCount = 0;

                        for (int k = 0; k < adjs.Length; k++) {
                            if (adjs[k] == '@') {
                                paperCount++;
                            }
                        }

                        if (paperCount < 4) {
                            ans++;
                        }
                    }
                }
            }

            return ans;
        }

        static int GetPaperRollsWithRemove(char[,] matrix) {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            int removed = 0;
            bool changed = true;

            while (changed) {
                changed = false;
                List<(int i, int j)> indexes = new List<(int i, int j)>();

                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m; j++) {
                        if (matrix[i, j] == '@') {
                            char[] adjs = GetAdjacent(matrix, i, j);
                            int paperCount = 0;

                            for (int k = 0; k < adjs.Length; k++) {
                                if (adjs[k] == '@') {
                                    paperCount++;
                                }
                            }

                            if (paperCount < 4) {
                                indexes.Add((i, j));
                            }
                        }
                    }
                }

                if (indexes.Count > 0) {
                    changed = true;
                    removed += indexes.Count;

                    foreach (var pos in indexes) {
                        matrix[pos.i, pos.j] = 'x';
                    }
                }
            }


            return removed;
        }

        public static int ForthDayA() {
            string[] lines = AOCUtils.ReadInput("4");
            int n = lines.Length;
            int m = lines[0].Length;
            char[,] matrix = new char[n, m];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    matrix[i, j] = lines[i][j];
                }
            }

            return GetPaperRolls(matrix);
        }

        public static int ForthDayB() {
            string[] lines = AOCUtils.ReadInput("4");
            int n = lines.Length;
            int m = lines[0].Length;
            char[,] matrix = new char[n, m];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    matrix[i, j] = lines[i][j];
                }
            }

            return GetPaperRollsWithRemove(matrix);
        }
    }
}
