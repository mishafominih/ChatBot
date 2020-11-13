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
            var result = new List<Element>();
            var node = info
                .Where(x => SelectOne(info, x.Key))
                .ToDictionary(x => x.Key, x => x.Value);

            var endNode = CreateEndNode(info);

            return result;
        }

        private List<Element> CreateEndNode(Dictionary<string, List<string>> info)
        {
            var keys = info.Keys.ToList();
            var endNode = new List<Element>();
            GetNode(keys, info, endNode);
            return endNode;
        }

        private void GetNode(List<string> keys, Dictionary<string, List<string>> info, List<Element> endNode)
        {
            foreach (var key in keys)
            {
                foreach(var value in info.Values)
                {
                    if (value.Contains(key))
                        GetNode(value, info, endNode);
                    else if (!SelectOne(info, key))
                        endNode.Add(new End(key));
                }
            }
        }

        /*
        private List<Element> n(Dictionary<string, List<string>> info)
        {
            var res = new List<Element>();
            var one = info
                .Where(x => SelectOne(info, x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            var other = info
                .Where(x => !SelectOne(info, x.Key));
            if (other is null)
            {
                foreach (var e in one)
                {
                    res.Add(new End(e.Value.FirstOrDefault()));
                }
                return res;
            }
            other = other.ToDictionary(x => x.Key, x => x.Value);
            foreach (var e in one)
            {
                //foreach (var v in e.Value)
                //{
                res.Add(new Medium(n(other.ToDictionary(x => x.Key, x => x.Value)), e.Key));
                //}
            }
            return res;
        } 
        */
        private static bool SelectOne(Dictionary<string, List<string>> info, string x)
        {
            foreach (var Ls in info.Values)
            {
                if (Ls.Contains(x))
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
