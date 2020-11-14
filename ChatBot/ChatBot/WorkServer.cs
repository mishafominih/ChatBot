using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public static class WorkServer
    {
        static Dictionary<string, string[]> dictSyn = new Dictionary<string, string[]>();
        static Dictionary<string, string[]> dictAnt = new Dictionary<string, string[]>();


        public static void GetDict()
        {
            var lines = File.ReadAllLines("synmaster.txt", Encoding.Default);
            foreach (var line in lines)
            {
                string[] vs = line.Split('|');
                if (dictSyn.ContainsKey(vs[0]))
                {
                    dictSyn[vs[0]] = vs.Skip(1).Count() > dictSyn[vs[0]].Length ? vs.Skip(1).ToArray() : dictSyn[vs[0]];
                }
                else 
                {                 
                    dictSyn[vs[0]] = vs.Skip(1).ToArray();
                }
            }
            dictSyn = dictSyn.Where(x => x.Value.Length > 20 && x.Value.Length < 28).ToDictionary(x => x.Key, x => x.Value);
            lines = File.ReadAllLines("Antonimy.txt", Encoding.UTF8);
            foreach (var line in lines)
            {
                dictAnt[line.Split(new char[] { '-', ' '}, StringSplitOptions.RemoveEmptyEntries)[0]] =
                    line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
            }

        }
        public static double GetData(string current, string comparison)
        {
            var syn = Find(dictSyn, current, comparison);
            var ant = Find(dictAnt, current, comparison);
            if (syn != 0)
                return 0.5 + syn;
            return 0.5 - ant;
        }

        private static double Find(Dictionary<string, string[]> dict, string current, string comparison)
        {
            if (dict.ContainsKey(current))
            {
                var first = dict[current].ToList();
                if (first.Contains(comparison)) return 0.5;
                for (int i = 1; i < 5; i++)
                {
                    var second = first.SelectMany(x =>
                    {
                        if (dict.ContainsKey(x))
                            return dict[x];
                        return new string[] { x };
                    }).ToList();
                    if (second.Contains(comparison)) return 0.5 - 0.1 * i;
                    first = second;
                }
            }
            return 0;
        }
    }
}
