using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class Layer
    {
        public List<Neuron> neurons = new List<Neuron>();
        Layer previous;
        Layer next;
        public Layer(int count, Layer prev)
        {
            previous = prev;
            for(int i = 0; i < count; i++)
            {
                if(previous.GetCountElem() == 0)
                    neurons.Add(new Neuron(count));
                else
                    neurons.Add(new Neuron(previous.GetCountElem()));
            }
        }

        private Layer(List<Neuron> ns, Layer prev)
        {
            previous = prev;
            neurons = ns;
        }

        public void SetNext(Layer l)
        {
            next = l;
        }

        public void SetPrev(Layer l)
        {
            previous = l;
        }

        public int GetCountElem()
        {
            return neurons == null? 0 : neurons.Count;
        }

        public List<double> ActivateNeurons(List<double> values)
        {
            var res = new List<double>();
            foreach(var n in neurons)
            {
                res.Add(n.Activate(values));
            }
            if(next != null)
                return next.ActivateNeurons(res);
            return res;
        }

        public Layer GetClone(Layer prev) {
            var newNeurons = new List<Neuron>();
            foreach(var n in neurons)
                newNeurons.Add(n.GetClone());
            var res = new Layer(newNeurons, prev);
            if(next != null)
                res.SetNext(next.GetClone(res));
            return res;
        }
    }
}
