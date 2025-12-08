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

        static long SolveProblem(List<long> numbers, char op) {
            long result = numbers[0];

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

            if (lines.Length == 0) return BigInteger.Zero;

            int width = lines[0].Length;

            for (int i = 1; i < lines.Length; i++) {
                if (lines[i].Length > width) {
                    width = lines[i].Length;
                }
            }

            string[] columns = new string[width];
            for (int i = 0; i < width; i++) {
                columns[i] = "";
            }

            for (int row = 0; row < lines.Length; row++) {
                for (int col = 0; col < lines[row].Length; col++) {
                    columns[col] += lines[row][col];
                }

                for (int col = lines[row].Length; col < width; col++) {
                    columns[col] += ' ';
                }
            }

            BigInteger total = BigInteger.Zero;
            BigInteger currentSum = BigInteger.Zero;
            char currentOp = '+';

            for (int i = 0; i < columns.Length; i++) {
                string col = columns[i];
                string trimmed = col.Trim();

                if (trimmed.Length > 0) {
                    char lastChar = trimmed[trimmed.Length - 1];
                    if (lastChar == '+' || lastChar == '*') {
                        total += currentSum;
                        currentOp = lastChar;

                        if (currentOp == '+') {
                            currentSum = BigInteger.Zero;
                        } else {
                            currentSum = BigInteger.One;
                        }
                    }
                }

                string numStr = "";
                for (int j = 0; j < trimmed.Length; j++) {
                    if (char.IsDigit(trimmed[j])) {
                        numStr += trimmed[j];
                    }
                }

                if (numStr.Length > 0) {
                    BigInteger num = BigInteger.Parse(numStr);

                    if (currentOp == '+') {
                        currentSum += num;
                    } else {
                        currentSum *= num;
                    }
                }
            }

            total += currentSum;

            return total;
        }
    }
}
