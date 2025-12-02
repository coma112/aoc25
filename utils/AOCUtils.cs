using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25.utils {
    internal class AOCUtils {
        public static string[] ReadInput(string filename) {
            string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string fullPath = Path.Combine(projectDir, "txts", filename + ".txt");

            return File.ReadAllLines(fullPath);
        }


        public static void Print(string[] t) {
            foreach (string v in t) {
                Console.WriteLine(v);
            }
        }
    }
}
