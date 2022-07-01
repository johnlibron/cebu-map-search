using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CebuMap
{
    public class Vertex
    {
        public Vertex(string name, int index, int x, int y)
        {
            Name = name;
            Index = index;
            X = x;
            Y = y;
        }

        public string Name { get; set; }
        public Vertex Parent { get; set; }
        public int Index { get; set; }
        public bool Visited { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost
        {
            get
            {
                return GCost + HCost;
            }
        }
    }
}
