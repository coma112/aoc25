using AOC25.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    

    internal class FirstDay {
        public static int FirstDayA() {
            string[] rotations = AOCUtils.ReadInput("1");
            int dialPosition = 50;
            int zeroCount = 0;

            foreach (string rotation in rotations) {
                char direction = rotation[0];
                int distance = Convert.ToInt32(rotation.Substring(1));

                if (direction == 'L') {
                    dialPosition -= distance;
                } else {
                    dialPosition += distance;
                }

                // 0-99 range, körkörös
                dialPosition = ((dialPosition % 100) + 100) % 100;


                if (dialPosition == 0) {
                    zeroCount++;
                }
            }

            return zeroCount;
        }

        public static int FirstDayB() {
            string[] rotations = AOCUtils.ReadInput("1");

            int dialPosition = 50;
            int zerosCount = 0;

            foreach (string rotation in rotations) {
                char direction = rotation[0];
                int distance = Convert.ToInt32(rotation.Substring(1));

                if (direction == 'L') {
                    for (int i = 1; i <= distance; i++) {
                        int newPos = ((dialPosition - i) % 100 + 100) % 100;
                        if (newPos == 0) {
                            zerosCount++;
                        }
                    }
                    dialPosition -= distance;
                } else {
                    for (int i = 1; i <= distance; i++) {
                        int newPos = ((dialPosition + i) % 100 + 100) % 100;
                        if (newPos == 0) {
                            zerosCount++;
                        }
                    }
                    dialPosition += distance;
                }

                dialPosition = ((dialPosition % 100) + 100) % 100;
            }

            return zerosCount;
        }
    }
}
