using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class Elements
    {
        public int AtomNum;
        public string Name;
        public string Symbol;
        public float MMass;
        
        public Elements( int num, string nam, string sym, float mass)
        {
            AtomNum = num;
            Name = nam;
            Symbol = sym;
            MMass = mass;
        }

        
    }
}
