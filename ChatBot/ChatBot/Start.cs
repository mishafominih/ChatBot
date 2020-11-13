using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class Start : Element
    {
        private string value;
        private List<Element> next;
        public Start(List<Element> elements, string val)
        {
            next = elements;
            value = val;
        }

        public Element Find(string str)
        {
            foreach(var e in next)
            {
                if (e.GetValue() == str)
                    return e;
            }
            return null;
        }

        public List<Element> GetNext()
        {
            return next;
        }

        public string GetValue()
        {
            return value;
        }

        public Element GlobalFind(string str)
        {
            throw new NotImplementedException();
        }
    }
}
