using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class ProcessingString
    {
        public static List<string> ParseString(string line)
        {
            var result = new List<string>();
            var word = new StringBuilder();
            foreach(var ch in line.ToLower())
            {
                if (Char.IsLetter(ch))
                    word.Append(ch);
                else if(word.Length != 0)
                {
                    result.Add(word.ToString());
                    word.Clear();
                }
            }
            return result;
        }
    }
}
