using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Deep_Land.Actions
{
    class Action
    {
        public virtual void Act()
        {
            Debug.WriteLine("Act");
        }
    }
}
