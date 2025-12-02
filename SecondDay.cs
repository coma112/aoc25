using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class SecondDay {
        static bool IsInvalid(long number) {
            string numStr = number.ToString();
            int len = numStr.Length;

            if (len % 2 != 0) {
                return false;
            }

            int halfLen = len / 2;
            string firstHalf = numStr.Substring(0, halfLen);
            string secondHalf = numStr.Substring(halfLen);

            return firstHalf == secondHalf;
        }

        static bool IsInvalidPartB(long number) {
            string numStr = number.ToString();
            int len = numStr.Length;

            for (int patternLen = 1; patternLen <= len / 2; patternLen++) {
                if (len % patternLen != 0) {
                    continue;
                }

                string pattern = numStr.Substring(0, patternLen);
                int repeatCount = len / patternLen;

                if (repeatCount >= 2) {
                    bool isValid = true;
                    for (int i = 0; i < repeatCount; i++) {
                        string segment = numStr.Substring(i * patternLen, patternLen);
                        if (segment != pattern) {
                            isValid = false;
                            break;
                        }
                    }
                    if (isValid) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static long SecondDayB() {
            string[] lines = AOCUtils.ReadInput("2");
            string oneLine = string.Join("", lines);
            string[] parts = oneLine.Split(',');
            long totalSum = 0;

            foreach (string part in parts) {
                string[] range = part.Split('-');
                long min = Convert.ToInt64(range[0]);
                long max = Convert.ToInt64(range[1]);

                for (long num = min; num <= max; num++) {
                    if (IsInvalidPartB(num)) {
                        totalSum += num;
                    }
                }
            }

            return totalSum;
        }

        public static long SecondDayA() {
            string[] lines = AOCUtils.ReadInput("2");
            string oneLine = string.Join("", lines);
            string[] parts = oneLine.Split(',');

            long totalSum = 0;

            foreach (string part in parts) {
                string[] range = part.Split('-');
                long min = Convert.ToInt64(range[0]);
                long max = Convert.ToInt64(range[1]);

                for (long num = min; num <= max; num++) {
                    if (IsInvalid(num)) {
                        totalSum += num;
                    }
                }
            }

            return totalSum;
        }
    }
}
