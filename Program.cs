using StatisticalReader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var statReader = new StatReader(@"C:\Users\Administrator\Desktop\dalATnyg.csv");
            var stats = statReader.ReadInGame();
            Console.WriteLine(stats);
            var x = 1;
        }
    }
}
