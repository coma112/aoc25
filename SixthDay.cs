using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class SixthDay {

        static List<(List<long> numbers, char op)> Parse(string[] lines) {
            int maxLen = 0;

            foreach (string line in lines) {
                if (line.Length > maxLen) maxLen = line.Length;
            }

            string[] nLines = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++) {
                nLines[i] = lines[i].PadRight(maxLen);
            }

            List<(List<long> numbers, char op)> problems = new List<(List<long> numbers, char op)>();

            int col = 0;

            while (col < maxLen) {
                bool allSpaces = true;

                for (int row = 0; row < nLines.Length; row++) {
                    if (nLines[row][col] != ' ') {
                        allSpaces = false;
                        break;
                    }
                }

                if (allSpaces) {
                    col++;
                    continue;
                }

                int problemWidth = 0;
                int checkCol = col;

                while (checkCol < maxLen) {
                    bool hasDigit = false;

                    for (int row = 0; row < nLines.Length; row++) {
                        char c = nLines[row][checkCol];

                        if (char.IsDigit(c) || c == '+' || c == '*') {
                            hasDigit = true;
                            break;
                        }
                    }

                    if (!hasDigit) break;
                    problemWidth++;
                    checkCol++;
                }

                List<long> numbers = new List<long>();
                char op = ' ';

                for (int row = 0; row < nLines.Length; row++) {
                    string segment = nLines[row].Substring(col, problemWidth);
                    string trimmed = segment.Trim();

                    if (trimmed.Length == 0) {
                        continue;
                    } else if (trimmed == "+" || trimmed == "*") {
                        op = trimmed[0];
                    } else if (long.TryParse(trimmed, out long num)) {
                        numbers.Add(num);
                    }
                }

                if (numbers.Count > 0 && op != ' ') {
                    problems.Add((numbers, op));
                }

                col += problemWidth;
            }

            return problems;
        }

        static List<(List<BigInteger> numbers, char op)> ParseRightToLeft(string[] lines) {
            int N = lines.Length;
            int maxLen = lines.Max(l => l.Length);

            string[] nLines = lines.Select(l => l.PadRight(maxLen)).ToArray();

            var problems = new List<(List<BigInteger> numbers, char op)>();

            int col = maxLen - 1;
            while (col >= 0) {
                bool allSpaces = true;
                for (int r = 0; r < N; r++) {
                    if (nLines[r][col] != ' ') {
                        allSpaces = false;
                        break;
                    }
                }
                if (allSpaces) {
                    col--;
                    continue;
                }

                int startCol = col;
                while (col >= 0) {
                    bool hasContent = false;
                    for (int r = 0; r < N; r++) {
                        char ch = nLines[r][col];
                        if (char.IsDigit(ch) || ch == '+' || ch == '*') {
                            hasContent = true;
                            break;
                        }
                    }
                    if (!hasContent) break;
                    col--;
                }
                int leftCol = col + 1;
                int width = startCol - leftCol + 1;

                char[,] block = new char[N, width];
                for (int r = 0; r < N; r++) {
                    for (int c = 0; c < width; c++) {
                        block[r, c] = nLines[r][leftCol + c];
                    }
                }

                int opCol = -1;
                for (int c = 0; c < width; c++) {
                    char ch = block[N - 1, c];
                    if (ch == '+' || ch == '*') {
                        opCol = c;
                        break;
                    }
                }
                if (opCol == -1) {
                    continue;
                }
                char op = block[N - 1, opCol];
                var numbers = new List<BigInteger>();

                for (int c = width - 1; c >= 0; c--) {
                    if (c == opCol) continue;

                    var sb = new StringBuilder();
                    for (int r = 0; r < N - 1; r++) {
                        char ch = block[r, c];
                        if (char.IsDigit(ch)) {
                            sb.Append(ch);
                        }
                    }

                    if (sb.Length == 0) {
                        numbers.Add(BigInteger.Zero);
                    } else {
                        numbers.Add(BigInteger.Parse(sb.ToString()));
                    }
                }

                if (numbers.Count > 0) {
                    problems.Add((numbers, op));
                }
            }

            return problems;
        }

        static long SolveProblem(List<long> numbers, char op) {
            long result = numbers[0];

            for (int i = 1; i < numbers.Count; i++) {
                if (op == '+') result += numbers[i];
                else result *= numbers[i];
            }

            return result;
        }

        static BigInteger SolveProblemBigInt(List<BigInteger> numbers, char op) {
            if (numbers == null || numbers.Count == 0) return BigInteger.Zero;
            BigInteger result = numbers[0];
            for (int i = 1; i < numbers.Count; i++) {
                if (op == '+') result += numbers[i];
                else result *= numbers[i];
            }
            return result;
        }


        public static long SixthDayA() {
            string[] lines = AOCUtils.ReadInput("6");

            var problems = Parse(lines);
            long grandTotal = 0;

            foreach (var problem in problems) {
                long answer = SolveProblem(problem.numbers, problem.op);
                grandTotal += answer;
            }

            return grandTotal;
        }

        public static BigInteger SixthDayB() {
            string[] lines = AOCUtils.ReadInput("6");
            var problems = ParseRightToLeft(lines);
            BigInteger grandTotal = BigInteger.Zero;

            foreach (var problem in problems) {
                BigInteger r = SolveProblemBigInt(problem.numbers, problem.op);
                grandTotal += r;
            }
            return grandTotal;
        }
    }
}
