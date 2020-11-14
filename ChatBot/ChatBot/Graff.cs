using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class Graff
    {
        private Element now;
        private Stack<Element> history = new Stack<Element>(); 
        private List<Element> starts;

        public Graff(Dictionary<string, string> type_path)
        {
            var dict = new Dictionary<string, List<string>>();
            foreach (var tp in type_path)
            {
                var downDict = Analise(tp.Value);
                dict.Add(tp.Key, downDict.Keys.ToList());
                foreach(var kv in downDict)
                {
                    dict.Add(kv.Key, kv.Value);
                }
            }
            Initialize(dict);
        }

        public Graff(List<Element> starts)
        {
            this.starts = starts;
            now = new Start(starts, null);
        }

        public Graff(Dictionary<string, List<string>> info)
        {
            Initialize(info);
        }

        private void Initialize(Dictionary<string, List<string>> info)
        {
            starts = info
                            .Where(x => SelectOne(info, x.Key))
                            .ToDictionary(x => x.Key, x => x.Value)
                            .Select(x => (Element)new Start(GetElements(info, x.Key), x.Key))
                            .ToList();
            now = new Start(starts, null);
            
        }

        public Element GetNow()
        {
            return now;
        }

        public void BackStep()
        {
            if (history.Count == 0)
                return;
            now = history.Pop();
        }

        public void NextStep(string choise)
        {
            history.Push(now);
            if (now is End) 
                return;
            now = now.Find(choise);
        }

        public List<string> GetVarians()
        {
            if (now is End)
                return null;
            return now.GetNext().Select(x => x.GetValue()).ToList();
        }

        public List<String> GetEnd()
        {
            var ends = new List<End>();
            foreach (var v in starts)
            {
                if (v is End) ends.Add((End)v);
                else AddEnd(ends, v);
            }
            return ends.Select(x => x.GetValue()).ToList();
        }

        private void AddEnd(List<End> ends, Element nowElem)
        {
            foreach(var v in nowElem.GetNext())
            {
                if (v is End) ends.Add((End)v);
                else AddEnd(ends, v);
            }
        }

        private List<Element> GetElements(Dictionary<string, List<string>> info, string key)
        {
            var res = new List<Element>();
            foreach (var e in info[key])
                if (info.ContainsKey(e))
                    res.Add(new Medium(GetElements(info, e), e));
                else
                    res.Add(new End(e));
            return res;
        }

        private static bool SelectOne(Dictionary<string, List<string>> info, string x)
        {
            foreach (var Ls in info.Values)
                if (Ls.Contains(x))
                    return false;
            return true;
        }

        private Dictionary<string, List<string>> Analise(string path)
        {
            var res = new Dictionary<string, List<string>>();
            var lines = File.ReadAllLines(path).Where(x => x != "").ToList();
            var t = new StringBuilder();
            var l = "";
            foreach (var line in lines)
            {
                if (line.StartsWith("*"))
                {
                    if (l != "")
                        res[l] = new List<string> { t.ToString() };
                    l = line;
                    t.Clear();
                }
                else
                {
                    t.Append("\n" + line);
                }
            }
            return res;
        }
    }
}
