using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC25 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine();
            Console.WriteLine("--- ELSŐ ---");
            Console.WriteLine(FirstDay.FirstDayA());
            Console.WriteLine(FirstDay.FirstDayB());
            Console.WriteLine("--- ELSŐ ---");
            Console.WriteLine();
            Console.WriteLine("--- MÁSODIK ---");
            Console.WriteLine(SecondDay.SecondDayA());
            Console.WriteLine(SecondDay.SecondDayB());
            Console.WriteLine("--- MÁSODIK ---");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--- HARMADIK ---");
            Console.WriteLine(ThirdDay.ThirdDayA());
            Console.WriteLine(ThirdDay.ThirdDayB());
            Console.WriteLine("--- HARMADIK ---");
            Console.WriteLine();
        }
    }
}
