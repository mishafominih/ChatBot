using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            new Test();
            //NewMethod();
        }

        private static void NewMethod()
        {
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

            var x = new Сategories(graff);
            Application.Run(x);
        }
    }
}
