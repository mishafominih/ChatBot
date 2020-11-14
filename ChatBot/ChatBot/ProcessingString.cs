using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class ProcessingString
    {
        const int count = 20;
        public static List<string> ParseString(string line)
        {
            var result = new List<string>();
            var word = new StringBuilder();
            foreach (var ch in line.ToLower())
            {
                if (Char.IsLetter(ch))
                    word.Append(ch);
                else if (word.Length != 0)
                {
                    result.Add(word.ToString());
                    word.Clear();
                }
            }
            return result;
        }

        public static List<double> StringEquals(List<string> current, List<string> val2)
        {
            var result = new List<double>();
            foreach (var e1 in current)
            {
                Tuple<string, string> tuple;
                var max = 0.0;
                var endWord = "";
                foreach (var e2 in val2)
                {
                    var x = WorkServer.GetData(e1, e2);
                    if (x > max)
                    {
                        max = x;
                        tuple = new Tuple<string, string>(e1, e2);
                        endWord = e2;
                    }
                }
                val2.Remove(endWord);
                result.Add(max);
            }
            result = result.OrderBy(x => x).ToList();
            if (result.Count < 20)
            {
                for (int i = 0; i < count; i++)
                {
                    result.Add(0.5);
                }
            }
            return result.Take(count).ToList();
        }
    }
}
