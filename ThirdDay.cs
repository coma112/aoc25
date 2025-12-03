using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class ThirdDay {
        static int LongestTwo(string number) {
            int maxJoltage = 0;

            for (int i = 0; i < number.Length - 1; i++) {
                for (int j = i + 1; j < number.Length; j++) {
                    int twoDigit = int.Parse($"{number[i]}{number[j]}");
                    if (twoDigit > maxJoltage) {
                        maxJoltage = twoDigit;
                    }
                }
            }

            return maxJoltage;
        }

        static long LargestKDigits(string number, int k) {
            char[] result = new char[k];
            int startIndex = 0;

            for (int i = 0; i < k; i++) {
                int remaining = k - i;
                int lastPossibleIndex = number.Length - remaining;

                char maxDigit = '0';
                int maxIndex = startIndex;

                for (int j = startIndex; j <= lastPossibleIndex; j++) {
                    if (number[j] > maxDigit) {
                        maxDigit = number[j];
                        maxIndex = j;
                    }
                }

                result[i] = maxDigit;
                startIndex = maxIndex + 1;
            }

            return long.Parse(string.Join("", result));
        }

        public static int ThirdDayA() {
            string[] lines = AOCUtils.ReadInput("3");
            int sum = 0;

            for (int i = 0; i < lines.Length; i++) {
                int longest = LongestTwo(lines[i]);
                sum += longest;
            }

            return sum;
        }

        public static long ThirdDayB() {
            string[] lines = AOCUtils.ReadInput("3");
            long sum = 0;

            for (int i = 0; i < lines.Length; i++) {
                long longest = LargestKDigits(lines[i], 12);
                sum += longest;
            }

            return sum;
        }
    }
}
