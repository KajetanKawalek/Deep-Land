using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Deep_Land
{
    public class Item
    {
        public string name;

        public Item(string _name)
        {
            name = _name;
        }

        public virtual void Use()
        {
            Debug.WriteLine("Use " + name);
        }
    }
}
