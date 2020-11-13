using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public interface Element
    {
        //string Value { get; }
        //List<Element> Next { get; }
        List<Element> GetNext();

        Element Find(string str);

        Element GlobalFind(string str);

        string GetValue();
    }
}
