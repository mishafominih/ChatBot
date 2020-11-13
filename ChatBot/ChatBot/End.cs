using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class End : Element
    {
        private string value;
        public End(string val)
        {
            value = val;
        }

        public Element Find(string str)
        {
            return null;
        }

        public List<Element> GetNext()
        {
            return null;
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
