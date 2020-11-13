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
                {"a", new List<string>{"k", "n"} },
                {"k", new List<string>{"j"} },
                {"l", new List<string>{"m"} },
                {"m", new List<string>{"r"} }
            };
            var graff = new Graff(new Dictionary<string, string> {
                {"мобильные телефоны", "Mobile.txt" },
                {"Телевидение", "Tv.txt" },
                {"Интернет", "Internet.txt" },
                {"Домашний телефон", "Home.txt" },
                {"Видеонаблюдение", "Video.txt" }
            });
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
