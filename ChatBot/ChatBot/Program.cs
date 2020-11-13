using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, List<string>>
            {
                {"p", new List<string>{"a", "l"} },
                {"a", new List<string>{"k"} },
                {"k", new List<string>{"n"} },
                {"l", new List<string>{"m"} },
                {"m", new List<string>{"r"} }
            };
            var graff = new Graff(dict);
            while (graff.GetVarians() != null)
            {
                foreach (var e in graff.GetVarians())
                {
                    Console.WriteLine(e);
                }
                graff.NextStep(Console.ReadLine());
            }
        }
    }
}
