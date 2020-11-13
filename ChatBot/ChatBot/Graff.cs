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
            starts = n(info);
            now = new Start(starts, null);
        }

        private List<Element> n(Dictionary<string, List<string>> info)
        {
            //var res = new List<Element>();
            //var one = info
            //    .Where(x => SelectOne(info, x))
            //    .ToDictionary(x => x.Key, x => x.Value);
            //var other = info
            //    .Where(x => !SelectOne(info, x));
            //if (other == null)
            //{
            //    foreach (var e in one)
            //    {
            //        res.Add(new End(e.Value.FirstOrDefault()));
            //    }
            //    return res;
            //}
            //other = other.ToDictionary(x => x.Key, x => x.Value);
            //foreach (var e in one)
            //{
            //    //foreach (var v in e.Value)
            //    //{
            //        res.Add(new Medium(n(other.ToDictionary(x => x.Key, x => x.Value)), e.Key));
            //    //}
            //}
            //return res;
        } 

        private static bool SelectOne(Dictionary<string, List<string>> info, KeyValuePair<string, List<string>> x)
        {
            foreach (var Ls in info.Values)
            {
                if (Ls.Contains(x.Key))
                    return false;
            }
            return true;
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
    }
}
