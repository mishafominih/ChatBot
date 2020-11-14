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
            WorkServer.GetDict();
            var info = WorkServer.GetData("интернет", "сплетение");
            //new Test();
            //NewMethod();
            //Console.Read();
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

            var x = new Сategories(graff);
            Application.Run(x);
        }
    }
}
