using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class Graff
    {
        private Element now;
        private List<Element> starts;

        public Graff(List<Element> starts)
        {
            this.starts = starts;
            now = new Start(starts, null);
        }

        public Graff(Dictionary<string, List<string>> info)
        {
            starts = info
                .Where(x => SelectOne(info, x.Key))
                .ToDictionary(x => x.Key, x => x.Value)
                .Select(x => (Element)new Start(GetElements(info, x.Key), x.Key))
                .ToList();
            now = new Start(starts, null);
        }
        public void NextStep(string choise)
        {
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
    }
}
