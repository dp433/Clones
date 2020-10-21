using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "learn 1 42";
            var ss = s.Split(' ');
            var cloneNum = int.Parse(ss[1]) - 1;

            Console.WriteLine(cloneNum);
            Console.ReadKey();
        }
    }
}
